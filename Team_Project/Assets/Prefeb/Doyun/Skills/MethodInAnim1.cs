using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodInAnim1 : MonoBehaviour
{
    public PlayerBattleControllerTest player;

    // �ִϸ��̼ǿ��� �̺�Ʈ�� �ҷ����� ���� ����
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