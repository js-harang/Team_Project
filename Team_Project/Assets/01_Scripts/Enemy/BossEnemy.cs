using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public enum BossState
{
    Idle,
    Move,
    Attack,
    Damaged,
    Die,
}

public abstract class BossEnemy : MonoBehaviour
{
    public BattleController bCon;

    BossState bState;
    public BossState BState 
    { 
        get 
        { 
            return bState; 
        } 
        set 
        {
            bState = value;
            switch (bState)
            {
                case BossState.Idle:
                    Idle();
                    break;
                case BossState.Attack:
                    Attack();
                    break;
                case BossState.Damaged:
                    break;
                case BossState.Die:
                    Die();
                    break;
                default:
                    break;
            }
        } 
    }

    // 보스에게 필요한 변수들
    public NavMeshAgent bossNav;
    public Animator bossAnim;
    public GameObject bossStateUI;
    public TMP_Text bossName_text;
    public Slider bossHpBar;
    public GameObject player;

    // 보스의 이름
    public string bossName;
    // 현재 체력
    public float currentHp;
    // 최대체력
    public int maxHp;
    // 공격력
    public int atkPower;
    // 공격 가능 범위
    public float attackDistance;
    // 공격 후 대기 시간
    public float attackDelay;


    void Start() 
    {
        // 등장 시 배틀 컨트롤러의 보스 개체수 증가시킴
        bCon = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bCon.BattleState = BattleState.BossAppear;
        bCon.BossCount++;

        //필요한 참조들 가져옴
        bossNav = GetComponent<NavMeshAgent>();
        bossAnim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");

        Appear();
    }

    void Update() 
    {
        if (bCon.BattleState != BattleState.NowBattle)
            return;

        Move();
    }

    // 보스 등장 시의 동작
    public abstract void Appear();
    // 보스 대기 상태 시의 동작
    public abstract void Idle();
    // 보스 내비게이션 정지 및 경로 초기화
    public abstract void StopMove();
    // 보스 이동 시의 동작
    public abstract void Move();
    // 보스 공격 시의 동작
    public abstract void Attack();
    // 보스가 피격 시의 동작
    public abstract void Hit();
    // 보스 상태 UI 갱신하여 표시
    public abstract void BossStateUpdate();
    // 보스 사망 시의 동작
    public abstract void Die();
}
