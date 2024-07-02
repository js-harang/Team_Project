using UnityEngine;

public class MethodInAnimTest : MonoBehaviour
{
    public PlayerCombatTest player;


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
}