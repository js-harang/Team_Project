using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleController : BattleStatus
{
    // (X 키 일반공격), (C 점프), (A, S, D 스킬)
    Animator pAnim;

    // 공격들 배열
    public PlayerAttack[] pAttacks;
    PlayerAttack pAttack;

    // 공격할 배열 번호
    public int atkIndex;

    // 공격 판정 위치 기준
    public Transform atkPos;
    public Transform[] atkPositions;
    // 공격 범위
    public Vector3 atkLenght;

    // 플레이어 상태
    PlayerState pState;

    // 플레이어 마나
    [SerializeField]
    float currentMp;
    [SerializeField]
    int maxMp;


    // 플레이어 슬라이더바
    public Slider hpSld;
    public Slider mpSld;

    private void Start()
    {
        StartSetting();
        ChangeAttack(atkIndex);
        Debug.Log(pAttack.aST.isCoolTime);
    }

    void Update()
    {// 죽은 상태면 키입력 금지
        if (pState.UnitState == UnitState.Die)
            return;

        AttackKeyInput();       // 공격키 입력
    }

    // 공격 애니메이션 재생
    public void AttackAnim(PlayerAttack pAttack)
    {
        pAnim.SetTrigger("IsAttack");
        pAnim.SetFloat("Attack", pAttack.aST.atkType);
    }

    void AttackKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.X))        // 일반 공격 입력
        {
            atkIndex = 0;

            ChangeAttack(atkIndex);
            AttackStart();
        }
        else if (Input.GetKeyDown(KeyCode.Z))   // A 키 스킬 입력
        {
            atkIndex = 1;

            ChangeAttack(atkIndex);
            AttackStart();
        }
    }

    void AttackStart()
    {
        if (!pAttack.aST.isCoolTime)
        {
            if (currentMp > pAttack.aST.useMana)
            {
                Debug.Log("공격");
                currentMp -= pAttack.aST.useMana;
                AttackAnim(pAttack);
                SetMpSlider();
            }
            else
                Debug.Log("마나가 부족합니다.");
        }
        else
            Debug.Log("공격 쿨타임");
    }

    public void AttackStateTrue()
    {
        pState.UnitBS = UnitBattleState.Attack;
    }
    public void AttackStateFalse()
    {
        pState.UnitBS = UnitBattleState.Idle;
    }

    // 공격 교체
    void ChangeAttack(int atkIndex)      // 키입력 받을시 해당하는 공격으로 스크립트 전환
    {
        atkPos = atkPositions[atkIndex];
        pAttack = pAttacks[atkIndex];
        pAttack.InitSetting();          // 해당하는 공격 스탯으로 변환
    }
    // 공격 판정
    public void AttackEnemy()
    {
        pAttack.Attack(atkPos, atkPower);
    }
    // 공격 쿨타임
    public void AttackCoolTime()
    {
        pAttack.AttackCoolTime();
    }

    // 데미지 입을때
    public void Hurt(float damage)
    {
        if (pState.UnitState == UnitState.Die)
            return;

        pState.UnitState = UnitState.Hurt;
        // Debug.Log("Player Damaged :" + damage);

        currentHp -= damage;

        SetPlayerSlider();

        if (currentHp > 0)
        {
            // 피격
        }
        else
        {
            Debug.Log("플레이어 사망");

            pState.UnitState = UnitState.Die;
            pAnim.SetTrigger("PlayerDie");

            UIController uiCon = GameObject.FindAnyObjectByType<UIController>();
            uiCon.GameOverUI();
        }
    }

    // 시작 세팅
    void StartSetting()
    {
        SetPlayerSlider();
        pState = GetComponent<PlayerState>();
        pAnim = GetComponentInChildren<Animator>();
    }

    // 슬라이더 세팅
    public void SetPlayerSlider()
    {
        SetHpSlider();
        SetMpSlider();
    }
    void SetHpSlider()
    {
        hpSld.value = currentHp / maxHp;
    }
    void SetMpSlider()
    {
        mpSld.value = currentMp / maxMp;
    }

    // 공격 범위 표시
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        // Gizmos.DrawWireCube(atkPos.position, pAttack.aST.atkLenght);
    }
}