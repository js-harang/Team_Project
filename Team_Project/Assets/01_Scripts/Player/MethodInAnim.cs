using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodInAnim : MonoBehaviour
{
    public PlayerBattleController player;

    // 애니메이션에서 이벤트로 불러오니 삭제 금지
    void PlayerAttack()
    {
        player.AttackEnemy();
    }
    void AttackCoolTime()
    {
        player.AttackCoolTime();
    }

    void AttactTrue()
    {
        player.AttackStateTrue();
    }
    void AttackFalse()
    {
        player.AttackStateFalse();
    }
}