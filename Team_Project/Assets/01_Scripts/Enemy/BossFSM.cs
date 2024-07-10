using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public enum BossState
{
    Appear,
    Idle,
    Move,
    Attack,
    Damaged,
    Die,
}

public abstract class BossFSM : DamagedAction
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
            time = 0;
            CalcBehaveDelay();
            switch (bState)
            {
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
    public Animator bossAnim;
    public GameObject bossStateUI;
    public TMP_Text bossName_text;
    public Slider bossHpBar;
    public GameObject player;

    [Space(10)]
    // 보스의 이름
    public string bossName;
    // 현재 체력
    public float currentHp;
    // 최대체력
    public int maxHp;
    // 공격력
    public int atkPower;
    // 보스로부터 플레이어로의 벡터
    public Vector3 dir;
    // 이동속도
    public float moveSpeed;
    // 행동간의 대기 시간
    public float behaveDelay;
    // 대기 시간 계산 하는 변수
    public float time;
    // 딜레이 시간이 끝났는지 확인하는 변수
    public bool delayDone;
    // 지금 공격 중인지 확인하는 변수
    public bool nowAttack;
    // 죽고 다음 단계로 넘아갈지 확인하는 변수
    public bool imDying;

    void Start() 
    {
        // 등장 시 배틀 컨트롤러의 보스 개체수 증가시킴
        bCon = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bCon.BattleState = BattleState.BossAppear;
        bCon.BossCount++;

        BossStart();
    }

    void Update() 
    {
        if (bState != BossState.Die)
        {
            BossMovement();

            if (nowAttack == true || bState == BossState.Appear)
                return;

            DelayTimeCount();
        }
        else if (imDying)
            bCon.BattleState = BattleState.Clear;
    }

    // 보스 활성화 시 Start 실행 동작
    public abstract void BossStart();
    // 보스가 Update에서 실핼하는 동작
    public abstract void BossMovement();
    // 보스 등장 시의 동작
    public abstract void Appear();
    // 보스 대기 상태 시의 동작
    public abstract void Idle();
    // 보스가 플레이어를 쳐다보게함
    public abstract void LookAtPlayer();
    // 보스 이동 시의 동작
    public abstract void Move();
    // 보스의 행동 간 텀을 랜덤으로 구함
    public abstract void CalcBehaveDelay();
    // 시간의 흐름을 구해서 딜레이 시간에 도달했는지 계산
    public abstract void DelayTimeCount();
    // 랜덤으로 패턴을 실행함
    public abstract void StartRandomPattern();
    // 보스 공격 시의 동작
    public abstract void Attack();
    // 보스가 공격 중인지 확인하는 애니메이션 이벤트용 메소드
    public abstract void NowAttackCheck();
    // 보스 상태 UI 갱신하여 표시
    public abstract void BossStateUpdate();
    // 보스 사망 시의 동작
    public abstract void Die();
}
