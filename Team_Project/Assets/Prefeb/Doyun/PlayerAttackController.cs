using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    // 공격들 배열
    public PlayerAttack pAttack;

    // 공격 판정 위치 기준
    public Transform atkPos;


    void Start()
    {
        pAttack.InitSetting();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pAttack.Attack(atkPos);
        }
    }
}
