using UnityEngine;

public class SkillAttack1 : PlayerAttack
{
    public override void InitSetting()
    {
        // ����Ÿ�� (�Ƶ����Ѹ���)
        aST.atkType = 1;

        // ���ݷ�
        aST.atkPower = 3;
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