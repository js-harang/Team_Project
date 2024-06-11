using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack1 : PlayerAttack
{
    public override void InitSetting()
    {
        // ����Ÿ��
        aST.atkType = 1;

        // ���ݷ�
        aST.atkPower = 3;
        // ���� ������
        aST.atkDelay = 10f;
        // ���� ����
        aST.atkLenght = new Vector3(2, 2.5f, 2.5f);

        // �Ҹ� ����
        aST.useMana = 10;
    }
    public override void Attack(Transform atkPosition)
    {
        base.Attack(atkPosition);
    }
}
