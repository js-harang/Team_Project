using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodInAnim1 : MonoBehaviour
{
    public PlayerBattleControllerTest player;

    // 애니메이션에서 이벤트로 불러오니 삭제 금지
    void PlayerAttack()
    {
        player.AttackEnemy();
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