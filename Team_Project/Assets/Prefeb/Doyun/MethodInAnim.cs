using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodInAnim : MonoBehaviour
{
    public PlayerBattleController player;

    // �ִϸ��̼ǿ��� �̺�Ʈ�� �ҷ����� ���� ����
    void PlayerAttack()
    {
        player.AttackEnemy();
    }
}