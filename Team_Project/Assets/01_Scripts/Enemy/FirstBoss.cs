using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : BossFSM
{
    // 근거리 공격 범위
    [Space(10)]
    public float meleeAttackDistance;
    // 원거리 공격 범위
    public float rangeAttackDistance;
    // 공격 범위 한계
    [SerializeField]
    float limitAttackRange;
    // 보스의 렌더러 변수
    [SerializeField]
    SkinnedMeshRenderer bossSkin;

    public override void BossStart()
    {
        //필요한 참조들 가져옴
        bossAnim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");

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
        yield return new WaitForSeconds(2.6f);
        bossAnim.SetTrigger("idle");
        yield return new WaitForSeconds(1f);
        BState = BossState.Move;
    }

    public override void BossMovement()
    {
        switch (BState)
        {
            case BossState.Idle:
                Idle();
                break;
            case BossState.Move:
                Move();
                break;
            default:
                break;
        }
    }

    public override void Idle()
    {
        bossAnim.SetTrigger("idle");
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
        bossAnim.SetTrigger("move");
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

        if (BState == BossState.Idle)
            randomState = Random.Range(2, 4);
        else
            randomState = Random.Range(1, 4);

        switch (randomState)
        {
            case 1:
                BState = BossState.Idle;
                break;
            case 2:
                BState = BossState.Move;
                break;
            case 3:
                BState = BossState.Attack;
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
            BState = BossState.Move;
            return;
        }

        LookAtPlayer();
        MyAttackPattern(distance);
    }

    // 플레이어와의 거리에 따라 공격패턴이 다름
    void MyAttackPattern(float distance)
    {
        if (distance <= meleeAttackDistance)
            bossAnim.SetTrigger("biteAttack");
        else if (distance >= rangeAttackDistance && distance <= limitAttackRange)
            bossAnim.SetTrigger("fireAttack");
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

    // 보스 피격시의 동작
    public override void Damaged(float hitPow)
    {
        if (BState == BossState.Die)
            return;
        currentHp -= hitPow;

        if (currentHp > 0)
        {
            StartCoroutine(DamagedEffect());
            BossStateUpdate();
        }
        else
        {
            BossStateUpdate();
            BState = BossState.Die;
        }
    }

    IEnumerator DamagedEffect()
    {
        bossSkin.material.color = Color.gray;
        yield return new WaitForSeconds(0.1f);
        bossSkin.material.color = Color.white;
    }

    public override void BossStateUpdate()
    {
        bossStateUI.SetActive(true);
        bossName_text.text = bossName;
        bossHpBar.value = currentHp / maxHp;
    }

    public override void Die()
    {
        bossAnim.SetTrigger("die");
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
        yield return new WaitForSeconds(3f);
        imDying = true;
        gameObject.SetActive(false);
    }

    public override void KnockBack(Vector3 atkPos, float knockBackForce) {}
}
