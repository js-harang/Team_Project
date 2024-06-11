using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackStatus
{
    //공격 타입
    public int atkType;

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

    public abstract void InitSetting();

    public virtual void Attack(Transform atkPosition)
    {
        Vector3 position = atkPosition.transform.position;
        enemys = Physics.OverlapBox(position, aST.atkLenght, Quaternion.identity);

        foreach (Collider enemy in enemys)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                Debug.Log("공격 타입 :" + aST.atkType);
                Debug.Log("적 공격 데미지 :" + aST.atkPower);
                Debug.Log("적 공격 데미지 :" + aST.atkPower);
                Debug.Log("소모 마나 :" + aST.useMana);
            }
        }
    }
}