using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    // (X 키 일반공격), (C 점프), (A, S, D 스킬)

    // 공격들 배열
    public PlayerAttack[] pAttacks;
    PlayerAttack pAttack;

    // 공격할 배열 번호
    public int atkIndex;

    // 공격 판정 위치 기준
    public Transform atkPos;

    private void Start()
    {
        pAttacks[0].InitSetting();
    }

    void Update()
    {
        AttackKeyInput();       // 공격키 입력
    }

    public void AttackEnemy()
    {
        pAttack.Attack(atkPos);
    }

    void ChangeAttck(int atkIndex)      // 키입력 받을시 해당하는 공격으로 스크립트 전환
    {
        pAttack = pAttacks[atkIndex];
        pAttack.InitSetting();          // 해당하는 공격 스탯으로 변환
    }

    void AttackKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.X))        // 일반 공격 입력
        {
            Debug.Log("일반공격 전환");
            atkIndex = 0;
            ChangeAttck(atkIndex);
        }
        else if (Input.GetKeyDown(KeyCode.A))   // A 키 스킬 입력
        {
            Debug.Log("A스킬 전환");
            atkIndex = 1;
            ChangeAttck(atkIndex);
        }
    }
}