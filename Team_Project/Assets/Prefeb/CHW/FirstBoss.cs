using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : BossEnemy
{
    // 등장 애니메이션 재생 시간
    float appearAnimTime = 2.33f;

    public override void Appear()
    {
        StartCoroutine(AppearAction(appearAnimTime));
    }

    IEnumerator AppearAction(float appearAnimTime)
    {
        //bossAnim.SetTrigger("appear");
        yield return new WaitForSeconds(appearAnimTime);
        BState = BossState.Idle;

        yield return new WaitForSeconds(1f);
        bCon.BattleState = BattleState.NowBattle;
    }

    public override void Idle()
    {
        StopMove();
        bossAnim.SetTrigger("idle");
    }

    public override void StopMove()
    {
        bossNav.isStopped = true;
        bossNav.ResetPath();
    }

    public override void Move()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= attackDistance)
        {
            bossNav.destination = player.transform.position;
            return;
        }
        else
            StopMove();
            //BState = BossState.Attack;
            
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
