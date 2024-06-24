using System.Collections;
using UnityEngine;

public struct AttackStatus
{
    //공격 타입
    public int atkType;

    // 스킬 쿨타임 여부
    public bool isCoolTime;

    // 공격력
    public int atkPower;
    // 공격 딜레이
    public float atkDelay;
    // 공격 범위
    public Vector3 atkLenght;

    // 소모 자원
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
                Debug.Log("적 공격 데미지 :" + aST.atkPower);

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
        Debug.Log("쿨타임 시작");
        aST.isCoolTime = true;

        yield return new WaitForSeconds(aST.atkDelay);

        Debug.Log("쿨타임 종료");
        aST.isCoolTime = false;
    }
}