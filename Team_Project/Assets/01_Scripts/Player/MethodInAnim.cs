using UnityEngine;

public class MethodInAnim : MonoBehaviour
{
    public PlayerCombatTest player;

    /*
    private void PlayerAttack()
    {
        player.AttackEnemy();
    }
    */

    private void PlayerAttack()
    {
        player.AttackEnemy();
    }

    private void AttactTrue()
    {
        player.AttackStateTrue();
    }
    private void AttackFalse()
    {
        player.AttackStateFalse();
    }

    /*  public PlayerBattleController player;

      // �ִϸ��̼ǿ��� �̺�Ʈ�� �ҷ����� ���� ����
      private void PlayerAttack()
      {
          player.AttackEnemy();
      }

      // ��Ÿ�� ��ư���� ó��
  *//*    private void AttackCoolTime()
      {
          player.AttackCoolTime();
      }*//*

      private void AttactTrue()
      {
          player.AttackStateTrue();
      }
      private void AttackFalse()
      {
          player.AttackStateFalse();
      }*/
}