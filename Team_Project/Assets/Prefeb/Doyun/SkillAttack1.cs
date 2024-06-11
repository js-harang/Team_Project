using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack1 : PlayerAttack
{
    public override void InitSetting()
    {
        // 공격타입
        aST.atkType = 1;

        // 공격력
        aST.atkPower = 3;
        // 공격 딜레이
        aST.atkDelay = 10f;
        // 공격 범위
        aST.atkLenght = new Vector3(2, 2.5f, 2.5f);

        // 소모 마나
        aST.useMana = 10;
    }
    public override void Attack(Transform atkPosition)
    {
        base.Attack(atkPosition);
    }
}
