using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleControllerOld : BattleStatus
{
    // (X Ű �Ϲݰ���), (C ����), (A, S, D ��ų)
    #region �÷��̾� ��ġ ����(ü��,���ݷ� �� �� BattleStatus ���� ��� ����)
    // �÷��̾� ���� ����
    [SerializeField]
    float currentMp;

    // �÷��̾� �ִ� ����
    [SerializeField]
    int maxMp;
    #endregion

    [Space(10)]
    #region ���� ����

    public PlayerAttackOld defaultAtk;
    public PlayerAttackOld pAttack;

    // ������ �迭 ��ȣ
    public int atkIndex;

    // ���� ���� ��ġ ����
    public Transform atkPos;
    public Transform[] atkPositions;

    // ���� ����
    public Vector3 atkLenght;
    #endregion

    [Space(10)]
    #region �÷��̾� �����̴� ��
    public Slider hpSld;
    public Slider mpSld;
    #endregion

    [Space(10)]
    #region Ű�Է�
    public Button skillABtn;
    public Button skillSBtn;
    #endregion

    [SerializeField]
    LayerMask enemyLayer;

    // �÷��̾� �ִϸ�����
    Animator pAnim;

    // �÷��̾� ����
    PlayerState pState;

    private void Start()
    {
        StartSetting();
    }

    void Update()
    {
        // ���� ���¸� Ű�Է� ����
        if (pState.UnitBS == UnitBattleState.Die)
            return;

        BattleKeyInput();       // ����Ű �Է�
    }
////////////////////////////////////////////////////////////////////////////

    #region Ű�Է�
    private void BattleKeyInput()
    {
        if (pState.UnitBS == UnitBattleState.Attack)
            return;

        if (Input.GetKeyDown(KeyCode.Z))        // �Ϲ� ���� �Է�
        {
            atkIndex = 0;
            pAttack = defaultAtk;
            ChangeAttack(atkIndex);
            AttackStart();
        }
        else if (Input.GetKeyDown(KeyCode.A))   // A Ű ��ų �Է�
            skillABtn.onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.S))   // S Ű ��ų �Է�
            skillSBtn.onClick.Invoke();
    }
    #endregion

////////////////////////////////////////////////////////////////////////////

    #region ���� ����
    public void AttackStart()
    {
        if (currentMp > pAttack.aST.useMana)
        {
            Debug.Log("���� Ÿ�� : " + pAttack.aST.atkType);
            currentMp -= pAttack.aST.useMana;
            AttackAnim(pAttack);
            SetMpSlider();
        }
        else
            Debug.Log("������ �����մϴ�.");
    }
    #endregion

    #region ���� �ִϸ��̼� ���
    public void AttackAnim(PlayerAttackOld pAttack)
    {
        pAnim.SetTrigger("IsAttack");
        pAnim.SetFloat("Attack", pAttack.aST.atkType);
    }
    #endregion

    #region ���� ����(�ִϸ��̼ǿ��� �̺�Ʈ�� �ҷ���)
    public void AttackEnemy()
    {
        pAttack.Attack(atkPos, atkPower, enemyLayer);
    }
    #endregion

    #region ���� ��ü (��ų��ư ���� ȣ���)
    public void ChangeAttack(int atkIndex)      // Ű�Է� ������ �ش��ϴ� �������� ��ũ��Ʈ ��ȯ
    {
        atkPos = atkPositions[atkIndex];
        pAttack.InitSetting();          // �ش��ϴ� ���� �������� ��ȯ
    }
    #endregion

    #region ���ݻ��� ����,��
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

    #region �÷��̾� ������ ������
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
            // �ǰ�
        }
        else
        {
            Debug.Log("�÷��̾� ���");

            pState.UnitBS = UnitBattleState.Die;
            pAnim.SetTrigger("PlayerDie");

            UIController uiCon = GameObject.FindAnyObjectByType<UIController>();
            uiCon.GameOverUI();
        }
    }
    #endregion

////////////////////////////////////////////////////////////////////////////

    #region ���� ����
    private void StartSetting()
    {
        SetPlayerSlider();
        pState = GetComponent<PlayerState>();
        pAnim = GetComponentInChildren<Animator>();
    }
    #endregion

    #region �����̴� ����
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

    // ���� ���� ǥ��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        // Gizmos.DrawWireCube(atkPos.position, pAttack.aST.atkLenght);
    }
}