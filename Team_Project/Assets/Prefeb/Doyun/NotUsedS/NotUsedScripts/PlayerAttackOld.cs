using System.Collections;
using UnityEngine;

public struct AttackStatus
{
    //공격 번호
    public int atkNum;

    //공격 타입
    public int atkType;

    // 스킬 쿨타임 여부
    public bool isCoolTime;

    // 공격력
    public float atkPower;
    // 공격 딜레이
    public float atkDelay;
    // 공격 범위
    public Vector3 atkLenght;

    // 소모 자원
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
                Debug.Log("적 공격 데미지 :" + atkDamage);

                // float 로 변환 필요
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
        Debug.Log("쿨타임 시작");
        aST.isCoolTime = true;

        yield return new WaitForSeconds(aST.atkDelay);

        Debug.Log("쿨타임 종료");
        aST.isCoolTime = false;
    }
*/
