using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

// 에너미 상태 상수
public enum EnemyState
{
    Appear,
    Idle,
    Move,
    Attack,
    Damaged,
    Die,
}

public abstract class EnemyFSM : DamagedAction
{
    // 에너미 상태 변수
    EnemyState eState;
    public EnemyState EState
    {
        get
        {
            return eState;
        }
        set
        {
            eState = value;
            CalcBehaveDelay();
            switch (eState)
            {
                case EnemyState.Attack:
                    Attack();
                    break;
                case EnemyState.Damaged:
                    break;
                case EnemyState.Die:
                    Die();
                    break;
                default:
                    break;
            }
        }
    }

    public BattleController bCon;

    // 에너미에게 필요한 변수들
    public Animator enemyAnim;
    public GameObject enemyStateUI;
    public Slider enemyHpBar;
    public GameObject player;
    // DamagedAction 의 KnockBack 실행에 필요한 변수
    public Rigidbody rb;

    [Space(10)]
    // 현재 체력
    public float currentHp;
    // 최대체력
    public int maxHp;
    // 공격력
    public int atkPower;
    // 에너미로부터 플레이어로의 벡터
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
    public bool imDying { set { if(value) bCon.EnemyCount--; } }

    private void Start()
    {
        // 등장 시 배틀 컨트롤러의 에너미 개체수 증가시킴
        bCon = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bCon.EnemyCount++;

        EnemyStart();
    }

    private void Update()
    {
        if (eState != EnemyState.Die)
        {
            EnemyMovement();

            if (nowAttack == true || eState == EnemyState.Appear)
                return;

            DelayTimeCount();
        }
    }

    // 에너미 활성화 시 Start 실행 동작
    public abstract void EnemyStart();
    // 에너미 등장 시의 동작
    public abstract void Appear();
    // 에너미가 Update에서 실핼하는 동작
    public abstract void EnemyMovement();
    // 에너미 대기 상태 시의 동작
    public abstract void Idle();
    // 에너미가 플레이어를 쳐다보게함
    public abstract void LookAtPlayer();
    // 에너미 이동 시의 동작
    public abstract void Move();
    // 에너미의 행동 간 텀을 랜덤으로 구함
    public abstract void CalcBehaveDelay();
    // 시간의 흐름을 구해서 딜레이 시간에 도달했는지 계산
    public abstract void DelayTimeCount();
    // 랜덤으로 패턴을 실행함
    public abstract void StartRandomPattern();
    // 에너미 공격 시의 동작
    public abstract void Attack();
    // 에너미가 공격 중인지 확인하는 애니메이션 이벤트용 메소드
    public abstract void NowAttackCheck();
    // 에너미 상태 UI 갱신하여 표시
    public abstract void EnemyStateUpdate();
    // 에너미 사망 시의 동작
    public abstract void Die();
}