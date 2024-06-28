using UnityEngine;

public class DefualtAttack : PlayerAttack
{
    public override void InitSetting()
    {
        // ����Ÿ��
        aST.atkType = 0;
        // ���ݷ�
        aST.atkPower = 1;
        // ���� ������
        aST.atkDelay = 1f;
        // ���� ����
        aST.atkLenght = new Vector3(2, 2.5f, 2.5f);

        // �Ҹ� ����
        aST.useMana = 0;
    }
    public override void Attack(Transform atkPosition, int atkPower, LayerMask enemyLayer)
    {
        base.Attack(atkPosition, atkPower, enemyLayer);
    }
}