using UnityEngine;

public class MethodInAnim : MonoBehaviour
{
    public PlayerCombat player;

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

    private void EndHurt()
    {
        player.EndHurt();
    }
}