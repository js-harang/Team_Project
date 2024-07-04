using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : BossEnemy
{
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
        StartCoroutine(AppearAction());
    }

    IEnumerator AppearAction()
    {
        yield return new WaitForSeconds(2.6f);
        BState = BossState.Idle;
        yield return new WaitForSeconds(1f);
        BState = BossState.Move;
    }

    public override void Idle()
    {
        bossAnim.SetTrigger("idle");
    }

    public override void LookAtPlayer()
    {
        if (transform.position.x > player.transform.position.x)
            transform.forward = Vector3.left;
        else
            transform.forward = Vector3.right;
    }

    public override void Move()
    {
        transform.Translate(player.transform.position * moveSpped * Time.deltaTime);
        LookAtPlayer();
        bossAnim.SetTrigger("move");
    }

    public override void BehaviorDelay()
    {
        
    }

    public override void Attack()
    {
        LookAtPlayer();
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
}
