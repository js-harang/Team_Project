using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : BossEnemy
{
    // 등장 애니메이션 재생 시간
    float appearAnimTime = 2.33f;

    public override void Appear()
    {
        LookAtPlayer();
        StartCoroutine(AppearAction(appearAnimTime));
    }

    IEnumerator AppearAction(float appearAnimTime)
    {
        yield return new WaitForSeconds(appearAnimTime);
        BState = BossState.Idle;

        yield return new WaitForSeconds(1f);
        bCon.BattleState = BattleState.NowBattle;
        BState = BossState.Move;
        bossAnim.SetTrigger("move");
    }

    public override void Idle()
    {
        StopMove();
        bossAnim.SetTrigger("idle");
    }

    public override void StopMove()
    {
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
        if (Vector3.Distance(transform.position, player.transform.position) >= attackDistance)
        {
            LookAtPlayer();
            return;
        }
        /*else
            StopMove();
            BState = BossState.Attack;*/
            
    }

    public override void Attack()
    {
        
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
