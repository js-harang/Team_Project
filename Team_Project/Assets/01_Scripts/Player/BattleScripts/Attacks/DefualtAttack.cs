using UnityEngine;

public class DefualtAttack : PlayerAttack
{
    public override void InitSetting()
    {
        // 공격타입
        aST.atkType = 0;
        // 공격력
        aST.atkPower = 1;
        // 공격 딜레이
        aST.atkDelay = 1f;
        // 공격 범위
        aST.atkLenght = new Vector3(2, 2.5f, 2.5f);

        // 소모 마나
        aST.useMana = 0;
    }
    public override void Attack(Transform atkPosition, int atkPower, LayerMask enemyLayer)
    {
        base.Attack(atkPosition, atkPower, enemyLayer);
    }
}