using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : EnemyFSM
{
    // ���� ���� �Ѱ�
    [SerializeField]
    float limitAttackRange;

    public override void EnemyStart()
    {
        //�ʿ��� ������ ������
        enemyAnim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();

        Appear();
    }

    public override void Appear()
    {
        LookAtPlayer();
        CalcBehaveDelay();
        StartCoroutine(AppearAction());
    }

    IEnumerator AppearAction()
    {
        yield return new WaitForSeconds(1f);
        EState = EnemyState.Move;
    }

    public override void EnemyMovement()
    {
        switch (EState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            default:
                break;
        }
    }

    public override void Idle()
    {
        enemyAnim.SetTrigger("idle");
    }

    public override void LookAtPlayer()
    {
        if (player.transform.position.x < transform.position.x)
            transform.forward = Vector3.left;
        else
            transform.forward = Vector3.right;
    }

    public override void Move()
    {
        LookAtPlayer();
        Vector3 dir = player.transform.position - transform.position;
        transform.position += dir * moveSpeed * Time.deltaTime;
        enemyAnim.SetTrigger("move");
    }

    public override void CalcBehaveDelay()
    {
        behaveDelay = Random.Range(1f, 2f);
    }

    public override void DelayTimeCount()
    {
        time += Time.deltaTime;
        if (time >= behaveDelay)
        {
            time = 0;
            StartRandomPattern();
        }
    }

    public override void StartRandomPattern()
    {
        int randomState;

        if (EState == EnemyState.Idle)
            randomState = Random.Range(2, 4);
        else
            randomState = Random.Range(1, 4);

        switch (randomState)
        {
            case 1:
                EState = EnemyState.Idle;
                break;
            case 2:
                EState = EnemyState.Move;
                break;
            case 3:
                EState = EnemyState.Attack;
                break;
            default:
                break;
        }
    }

    public override void Attack()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance >= limitAttackRange)
        {
            EState = EnemyState.Move;
            return;
        }

        LookAtPlayer();
        enemyAnim.SetTrigger("attack");
    }

    public override void NowAttackCheck()
    {
        if (nowAttack)
        {
            nowAttack = false;
            return;
        }
        nowAttack = true;
        time = 0;
    }

    public override void Damaged(float hitPow)
    {
        if (EState == EnemyState.Die)
            return;
        currentHp -= hitPow;

        if (currentHp > 0)
        {
            StartCoroutine(DamagedEffect());
            EnemyStateUpdate();
        }
        else
        {
            EnemyStateUpdate();
            EState = EnemyState.Die;
        }
    }

    IEnumerator DamagedEffect()
    {
        
        yield return new WaitForSeconds(0.1f);
        
    }

    public override void EnemyStateUpdate()
    {
        enemyStateUI.SetActive(true);
        enemyHpBar.value = currentHp / maxHp;
    }

    public override void Die()
    {
        enemyAnim.SetTrigger("die");
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        imDying = true;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    public override void KnockBack(Vector3 atkPos, float knockBackForce)
    {
        rb.velocity = Vector3.zero;

        float dis = Vector3.Distance(transform.position, atkPos);

        Vector3 dir = atkPos - transform.position;
        dir.Normalize();

        rb.AddForce(dir * (knockBackForce / dis),ForceMode.Impulse);
    }
}