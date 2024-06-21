using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleControllerTest : BattleStatus
{
    // (X 키 일반공격), (C 점프), (A, S, D 스킬)
    #region 플레이어 수치 관련 (체력, 공격력 등은 BattleStatus 에서 상속받음)
////////////////////////////////////////////////////////////////////////////////////

    // 플레이어 현재 마나
    [SerializeField]
    float currentMp;

    // 플레이어 최대 마나
    [SerializeField]
    int maxMp;

    // 소모되는 마나
    [SerializeField]
    int useMana;

    [SerializeField]
    int skillDamage;
    ////////////////////////////////////////////////////////////////////////////////////
    #endregion

    [Space(10)]
    public int index;
    
    [Space(10)]
    #region 공격 관련
////////////////////////////////////////////////////////////////////////////////////

    public Transform atkPos;
    public Transform[] atkPositions;

    // 공격 타입
    public int atkType;

    // 공격 범위
    public Vector3 atkLenght;

////////////////////////////////////////////////////////////////////////////////////
    #endregion

    [Space(10)]
    #region 적 감지 관련
////////////////////////////////////////////////////////////////////////////////////
 
    Collider[] enemys;

    public LayerMask enemyLayer;

////////////////////////////////////////////////////////////////////////////////////
    #endregion

    [Space(10)]
    #region 플레이어 슬라이더 바
    // 플레이어 슬라이더바
    public Slider hpSld;
    public Slider mpSld;
    #endregion

    // 플레이어 애니메이터
    Animator pAnim;

    // 플레이어 상태
    PlayerState pState;


    private void Start()
    {
        StartSetting();
        atkPos = atkPositions[index];
    }

    void Update()
    {// 죽은 상태면 키입력 금지
        if (pState.UnitState == UnitState.Die)
            return;

        KeyInput();       // 공격키 입력
    }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    #region 키입력
    void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.X))        // 일반 공격 입력
        {
            // AttackStart();
        }
        else if (Input.GetKeyDown(KeyCode.Z))   // A 키 스킬 입력
        {
            // AttackStart();
        }
    }
    #endregion

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region 공격 세팅
    public void AttackSetting(int _atkType, int _damage, int _atkPositionIndex, Vector3 _atkLenght, int _useMana)
    {
        useMana = _useMana;
        atkType = _atkType;
        index = _atkPositionIndex;
        atkLenght = _atkLenght;
        skillDamage = _damage;
/*
        Debug.Log("데미지" + atkPower);
        Debug.Log("소모마나" + useMana);
        Debug.Log("공격 타입" + atkType);
        Debug.Log("인덱스" + index);
        Debug.Log("공격 벡터" + atkLenght);*/
    }
    #endregion

    #region 공격 시작
    public void AttackStart()
    {
        if (currentMp > useMana)
        {
           // Debug.Log("공격 타입 : " + atkType);
            currentMp -= useMana;
            AttackAnim(atkType);
            SetMpSlider();
        }
        else
            Debug.Log("마나가 부족합니다.");
    }
    #endregion

    #region 공격 애니메이션 재생
    public void AttackAnim(int atkType)
    {
        pAnim.SetTrigger("IsAttack");
        pAnim.SetFloat("Attack", atkType);
    }
    #endregion

    #region 공격판정 (애니메이션 에서 불러짐)
    public void AttackEnemy()
    {
        Attack(atkPos, skillDamage);
    }
    #endregion

    #region 공격 판정
    public void Attack(Transform _atkPosition, int _skillDamage)
    {
        Vector3 position = _atkPosition.transform.position;
        enemys = Physics.OverlapBox(position, atkLenght, Quaternion.identity, enemyLayer);

        // 감지한 적 오브젝트 들에 데미지 넣기
        foreach (Collider enemy in enemys)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                Debug.Log("적 공격 데미지 :" + _skillDamage);

                EnemyFSM enemyFsm = enemy.GetComponent<EnemyFSM>();
                enemyFsm.HitEnemy(_skillDamage);
            }
            else if (enemy.gameObject.tag == "InteractObj")
            {
                TestObj testObj = enemy.GetComponent<TestObj>();
                testObj.TestDebug(_skillDamage);
            }
        }
    }
    #endregion

    #region 공격상태 시작,끝
    public void AttackStateTrue()
    {
        pState.UnitBS = UnitBattleState.Attack;
    }
    public void AttackStateFalse()
    {
        pState.UnitBS = UnitBattleState.Idle;
    }
    #endregion

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region 플레이어 데미지 입을때
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
    #endregion

    #region 시작 세팅
    void StartSetting()
    {
        SetPlayerSlider();
        pState = GetComponent<PlayerState>();
        pAnim = GetComponentInChildren<Animator>();
    }
    #endregion

    #region 슬라이더 세팅
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
    #endregion

    // 공격 범위 표시
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        // Gizmos.DrawWireCube(atkPos.position, pAttack.aST.atkLenght);
    }
}