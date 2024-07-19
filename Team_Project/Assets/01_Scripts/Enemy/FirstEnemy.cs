using System.Collections;
using UnityEngine;

public class FirstEnemy : EnemyFSM
{
    // 공격 범위 한계
    [SerializeField]
    float limitAttackRange;

    [SerializeField] int exp;

    public override void EnemyStart()
    {
        //필요한 참조들 가져옴
        enemyAnim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        myColl = GetComponent<Collider>();

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
        dir.Normalize();
        transform.position += dir * moveSpeed * Time.deltaTime;
        enemyAnim.SetTrigger("move");
    }

    public override void CalcBehaveDelay()
    {
        time = 0;
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

    // DamagedAction 에서 상속받은 메서드를 선언
    public override void Damaged(float hitPow)
    {
        if (EState == EnemyState.Die)
            return;
        currentHp -= hitPow;

        if (currentHp > 0)
        {
            EState = EnemyState.Damaged;
            enemyAnim.SetTrigger("damaged");
            StartCoroutine(DamagedEffect());
            EnemyStateUpdate();
        }
        else
        {
            EnemyStateUpdate();

            GameManager.gm.SumEXP(exp);
            EState = EnemyState.Die;
        }
    }

    IEnumerator DamagedEffect()
    {
        enemySkin.material.color = Color.gray;
        yield return new WaitForSeconds(0.1f);
        enemySkin.material.color = Color.white;
    }

    // DamagedAction 에서 상속받은 메서드를 선언
    public override void KnockBack(Vector3 atkPos, float knockBackForce)
    {
        rb.velocity = Vector3.zero;

        if (EState == EnemyState.Die)
            return;

        float dis = Vector3.Distance(transform.position, atkPos);

        Vector3 dir = transform.position - atkPos;
        dir.Normalize();

        rb.AddForce(dir * (knockBackForce / dis), ForceMode.Impulse);
    }

    public override void EnemyStateUpdate()
    {
        enemyStateUI.SetActive(true);
        enemyHpBar.value = currentHp / maxHp;
    }

    public override void Die()
    {
        rb.useGravity = false;
        myColl.enabled = false;
        enemyAnim.SetTrigger("die");
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        imDying = true;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
