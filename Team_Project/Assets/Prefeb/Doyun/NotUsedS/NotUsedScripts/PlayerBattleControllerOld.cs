using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleControllerOld : BattleStatus
{
    // (X 키 일반공격), (C 점프), (A, S, D 스킬)
    #region 플레이어 수치 관련(체력,공격력 등 은 BattleStatus 에서 상속 받음)
    // 플레이어 현재 마나
    [SerializeField]
    float currentMp;

    // 플레이어 최대 마나
    [SerializeField]
    int maxMp;
    #endregion

    [Space(10)]
    #region 공격 관련

    public PlayerAttackOld defaultAtk;
    public PlayerAttackOld pAttack;

    // 공격할 배열 번호
    public int atkIndex;

    // 공격 판정 위치 기준
    public Transform atkPos;
    public Transform[] atkPositions;

    // 공격 범위
    public Vector3 atkLenght;
    #endregion

    [Space(10)]
    #region 플레이어 슬라이더 바
    public Slider hpSld;
    public Slider mpSld;
    #endregion

    [Space(10)]
    #region 키입력
    public Button skillABtn;
    public Button skillSBtn;
    #endregion

    [SerializeField]
    LayerMask enemyLayer;

    // 플레이어 애니메이터
    Animator pAnim;

    // 플레이어 상태
    PlayerState pState;

    private void Start()
    {
        StartSetting();
    }

    void Update()
    {
        // 죽은 상태면 키입력 금지
        if (pState.UnitBS == UnitBattleState.Die)
            return;

        BattleKeyInput();       // 공격키 입력
    }
////////////////////////////////////////////////////////////////////////////

    #region 키입력
    private void BattleKeyInput()
    {
        if (pState.UnitBS == UnitBattleState.Attack)
            return;

        if (Input.GetKeyDown(KeyCode.Z))        // 일반 공격 입력
        {
            atkIndex = 0;
            pAttack = defaultAtk;
            ChangeAttack(atkIndex);
            AttackStart();
        }
        else if (Input.GetKeyDown(KeyCode.A))   // A 키 스킬 입력
            skillABtn.onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.S))   // S 키 스킬 입력
            skillSBtn.onClick.Invoke();
    }
    #endregion

////////////////////////////////////////////////////////////////////////////

    #region 공격 시작
    public void AttackStart()
    {
        if (currentMp > pAttack.aST.useMana)
        {
            Debug.Log("공격 타입 : " + pAttack.aST.atkType);
            currentMp -= pAttack.aST.useMana;
            AttackAnim(pAttack);
            SetMpSlider();
        }
        else
            Debug.Log("마나가 부족합니다.");
    }
    #endregion

    #region 공격 애니메이션 재생
    public void AttackAnim(PlayerAttackOld pAttack)
    {
        pAnim.SetTrigger("IsAttack");
        pAnim.SetFloat("Attack", pAttack.aST.atkType);
    }
    #endregion

    #region 공격 판정(애니메이션에서 이벤트로 불러짐)
    public void AttackEnemy()
    {
        pAttack.Attack(atkPos, atkPower, enemyLayer);
    }
    #endregion

    #region 공격 교체 (스킬버튼 에서 호출됨)
    public void ChangeAttack(int atkIndex)      // 키입력 받을시 해당하는 공격으로 스크립트 전환
    {
        atkPos = atkPositions[atkIndex];
        pAttack.InitSetting();          // 해당하는 공격 스탯으로 변환
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

////////////////////////////////////////////////////////////////////////////

    #region 플레이어 데미지 입을때
    public void Hurt(float damage)
    {
        if (pState.UnitBS == UnitBattleState.Die)
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

            pState.UnitBS = UnitBattleState.Die;
            pAnim.SetTrigger("PlayerDie");

            UIController uiCon = GameObject.FindAnyObjectByType<UIController>();
            uiCon.GameOverUI();
        }
    }
    #endregion

////////////////////////////////////////////////////////////////////////////

    #region 시작 세팅
    private void StartSetting()
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
    private void SetHpSlider()
    {
        hpSld.value = currentHp / maxHp;
    }
    private void SetMpSlider()
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