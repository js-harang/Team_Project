using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : BossEnemy
{
    public float appearAnimTime;

    public override void Appear()
    {
        StartCoroutine(AppearAction(appearAnimTime));
    }

    IEnumerator AppearAction(float appearAnimTime)
    {
        yield return new WaitForSeconds(appearAnimTime);
        BState = BossState.Idle;

        yield return new WaitForSeconds(1f);
        bCon.BattleState = BattleState.NowBattle;
    }

    public override void Idle()
    {
        
    }

    public override void Move()
    {
        
    }

    public override void Attack()
    {
        
    }

    public override void Hit()
    {
        bossStateUI.SetActive(true);
        bossName_text.text = bossName;
        bossHpBar.value = currentHp / maxHp;
    }

    public override void Die()
    {
        
    }
}
