using System.Collections;
using UnityEngine;

public struct AttackStatus
{
    //���� ��ȣ
    public int atkNum;

    //���� Ÿ��
    public int atkType;

    // ��ų ��Ÿ�� ����
    public bool isCoolTime;

    // ���ݷ�
    public float atkPower;
    // ���� ������
    public float atkDelay;
    // ���� ����
    public Vector3 atkLenght;

    // �Ҹ� �ڿ�
    public int useMana;
}

public abstract class PlayerAttackOld : MonoBehaviour
{
    public AttackStatus aST;

    [SerializeField]
    Collider[] enemys;


    public abstract void InitSetting();

    public virtual void Attack(Transform atkPosition, int atkPower, LayerMask enemyLayer)
    {

        Vector3 position = atkPosition.transform.position;
        enemys = Physics.OverlapBox(position, aST.atkLenght, Quaternion.identity, enemyLayer);

        foreach (Collider enemy in enemys)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                float atkDamage = atkPower * aST.atkPower;
                Debug.Log("�� ���� ������ :" + atkDamage);

                // float �� ��ȯ �ʿ�
                EnemyFSM enemyFsm = enemy.GetComponent<EnemyFSM>();
                enemyFsm.Damaged(atkDamage);
            }
        }
    }
}
/*
    public virtual void AttackCoolTime()
    {
        StartCoroutine(CoolTime());
    }

    IEnumerator CoolTime()
    {
        Debug.Log("��Ÿ�� ����");
        aST.isCoolTime = true;

        yield return new WaitForSeconds(aST.atkDelay);

        Debug.Log("��Ÿ�� ����");
        aST.isCoolTime = false;
    }
*/