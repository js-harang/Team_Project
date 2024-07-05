using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : BossEnemy
{
    // 근거리 공격 범위
    [SerializeField, Space(10)]
    float meleeAttackDistance;
    [SerializeField]
    // 원거리 공격 범위
    float rangeAttackDistance;
    // 공격 범위 한계
    [SerializeField]
    float limitAttackRange;

    public override void BossStart()
    {
        // 등장 시 배틀 컨트롤러의 보스 개체수 증가시킴
        bCon = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bCon.BattleState = BattleState.BossAppear;
        bCon.BossCount++;

        //필요한 참조들 가져옴
        bossAnim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");
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
            case BossState.Attack:
                StartNextPattern();
                break;
            default:
                break;
        }
    }

    public override void Idle()
    {
        bossAnim.SetTrigger("idle");

        StartNextPattern();
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
        Vector3 dir = player.transform.position - transform.position;
        LookAtPlayer();
        bossAnim.SetTrigger("move");
        transform.position += dir * moveSpeed * Time.deltaTime;

        StartNextPattern();
    }

    public override void CalcBehaveDelay()
    {
        behaveDelay = Random.Range(1f, 2f);
        Debug.Log(behaveDelay);
    }

    public override void DelayTimeCount()
    {
        time += Time.deltaTime;
        Debug.Log(time);
        if (time >= behaveDelay)
            delayDone = true;
        else
            delayDone = false;
    }

    public override void RandomPattern()
    {
        /*var randomState = System.Enum.GetValues(enumType:typeof(BossState));
        BState = (BossState)randomState.GetValue(Random.Range(1, 4));*/
        delayDone = false;
        nowAttack = 0;

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

        Debug.Log(BState);
    }

    // 딜레이가 끝났을 경우 다음 행동을 개시
    public void StartNextPattern()
    {
        if (delayDone)
            RandomPattern();
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
        MyAttackPattern();
    }

    // 플레이어와의 거리에 따라 공격패턴이 다름
    void MyAttackPattern()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= meleeAttackDistance)
            bossAnim.SetTrigger("biteAttack");
        else if (distance >= rangeAttackDistance && distance <= limitAttackRange)
            bossAnim.SetTrigger("fireAttack");
    }

    public override void NowAttackCheck()
    {
        switch (nowAttack)
        {
            case 0:
                nowAttack = 1; time = 0;
                break;
            case 1:
                nowAttack = 2;
                break;
            default:
                break;
        }
    }

    public override void Hit()
    {
        BossStateUpdate();
    }

    public override void BossStateUpdate()
    {
        bossStateUI.SetActive(true);
        bossName_text.text = bossName;
        bossHpBar.value = currentHp / maxHp;
    }

    public override void Die()
    {

    }
}
