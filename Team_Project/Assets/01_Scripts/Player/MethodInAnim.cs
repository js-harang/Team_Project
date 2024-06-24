using UnityEngine;

public class MethodInAnim : MonoBehaviour
{
    public PlayerBattleController player;

    // 애니메이션에서 이벤트로 불러오니 삭제 금지
    private void PlayerAttack()
    {
        player.AttackEnemy();
    }

    // 쿨타임 버튼에서 처리
/*    private void AttackCoolTime()
    {
        player.AttackCoolTime();
    }*/

    private void AttactTrue()
    {
        player.AttackStateTrue();
    }
    private void AttackFalse()
    {
        player.AttackStateFalse();
    }
}