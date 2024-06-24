using System.Collections;
using UnityEngine;

public struct AttackStatus
{
    //���� Ÿ��
    public int atkType;

    // ��ų ��Ÿ�� ����
    public bool isCoolTime;

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
    [SerializeField]
    LayerMask enemyLayer;

    public abstract void InitSetting();

    public virtual void Attack(Transform atkPosition, int atkPower)
    {

        Vector3 position = atkPosition.transform.position;
        enemys = Physics.OverlapBox(position, aST.atkLenght, Quaternion.identity, enemyLayer);

        foreach (Collider enemy in enemys)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("�� ���� ������ :" + aST.atkPower);

                EnemyFSM enemyFsm = enemy.GetComponent<EnemyFSM>();
                enemyFsm.HitEnemy(aST.atkPower);
            }
        }
    }

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
}