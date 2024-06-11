using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackStatus
{
    //���� Ÿ��
    public int atkType;

    // ���ݷ�
    public int atkPower;
    // ���� ������
    public float atkDelay;
    // ���� ����
    public Vector3 atkLenght;

    // �Ҹ� �ڿ�
    public int useMana;
}

public abstract class PlayerAttack : MonoBehaviour
{
    public AttackStatus aST;

    [SerializeField]
    Collider[] enemys;

    public abstract void InitSetting();

    public virtual void Attack(Transform atkPosition)
    {
        Vector3 position = atkPosition.transform.position;
        enemys = Physics.OverlapBox(position, aST.atkLenght, Quaternion.identity);

        foreach (Collider enemy in enemys)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                Debug.Log("���� Ÿ�� :" + aST.atkType);
                Debug.Log("�� ���� ������ :" + aST.atkPower);
                Debug.Log("�� ���� ������ :" + aST.atkPower);
                Debug.Log("�Ҹ� ���� :" + aST.useMana);
            }
        }
    }
}