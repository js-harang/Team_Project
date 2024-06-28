using UnityEngine;

public class SkillAttack1 : PlayerAttack
{
    public override void InitSetting()
    {
        // ���� ��ȣ
        aST.atkNum = 1;

        // ����Ÿ�� (�Ƶ����Ѹ���)
        aST.atkType = 1;

        // ���� ����
        aST.atkPower = 1.2f;
        // ���� ������
        aST.atkDelay = 10f;
        // ���� ����
        aST.atkLenght = new Vector3(4, 2.5f, 4);

        // �Ҹ� ����
        aST.useMana = 10;
    }

    public override void Attack(Transform atkPosition, int atkPower)
    {
        base.Attack(atkPosition, atkPower);
    }
}