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

    public override void Idle()
    {
        bossAnim.SetTrigger("idle");

        if (TimeCount())
            RandomPattern();
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

        if (TimeCount())
            RandomPattern();
    }

    public override void CalcBehaveDelay()
    {
        behaveDelay = Random.Range(1f, 3f);
    }

    public override bool TimeCount()
    {
        time += Time.deltaTime;
        if (time >= behaveDelay)
            return true;
        else
            return false;
    }

    public override void RandomPattern()
    {
        var randomState = System.Enum.GetValues(enumType:typeof(BossState));
        BState = (BossState)randomState.GetValue(Random.Range(1, 4));
        Debug.Log(BState);
    }

    public override void Attack()
    {
        LookAtPlayer();
        bossAnim.SetTrigger("biteAttack");

        if (TimeCount())
            RandomPattern();
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
