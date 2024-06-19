using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleController : BattleStatus
{
    // (X Ű �Ϲݰ���), (C ����), (A, S, D ��ų)
    Animator pAnim;

    // ���ݵ� �迭
    public PlayerAttack[] pAttacks;
    PlayerAttack pAttack;

    // ������ �迭 ��ȣ
    public int atkIndex;

    // ���� ���� ��ġ ����
    public Transform atkPos;
    public Transform[] atkPositions;
    // ���� ����
    public Vector3 atkLenght;

    // �÷��̾� ����
    PlayerState pState;

    // �÷��̾� ����
    [SerializeField]
    float currentMp;
    [SerializeField]
    int maxMp;


    // �÷��̾� �����̴���
    public Slider hpSld;
    public Slider mpSld;

    private void Start()
    {
        StartSetting();
        ChangeAttack(atkIndex);
        Debug.Log(pAttack.aST.isCoolTime);
    }

    void Update()
    {// ���� ���¸� Ű�Է� ����
        if (pState.UnitState == UnitState.Die)
            return;

        AttackKeyInput();       // ����Ű �Է�
    }

    // ���� �ִϸ��̼� ���
    public void AttackAnim(PlayerAttack pAttack)
    {
        pAnim.SetTrigger("IsAttack");
        pAnim.SetFloat("Attack", pAttack.aST.atkType);
    }

    void AttackKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.X))        // �Ϲ� ���� �Է�
        {
            atkIndex = 0;

            ChangeAttack(atkIndex);
            AttackStart();
        }
        else if (Input.GetKeyDown(KeyCode.Z))   // A Ű ��ų �Է�
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
                Debug.Log("����");
                currentMp -= pAttack.aST.useMana;
                AttackAnim(pAttack);
                SetMpSlider();
            }
            else
                Debug.Log("������ �����մϴ�.");
        }
        else
            Debug.Log("���� ��Ÿ��");
    }

    public void AttackStateTrue()
    {
        pState.UnitBS = UnitBattleState.Attack;
    }
    public void AttackStateFalse()
    {
        pState.UnitBS = UnitBattleState.Idle;
    }

    // ���� ��ü
    void ChangeAttack(int atkIndex)      // Ű�Է� ������ �ش��ϴ� �������� ��ũ��Ʈ ��ȯ
    {
        atkPos = atkPositions[atkIndex];
        pAttack = pAttacks[atkIndex];
        pAttack.InitSetting();          // �ش��ϴ� ���� �������� ��ȯ
    }
    // ���� ����
    public void AttackEnemy()
    {
        pAttack.Attack(atkPos, atkPower);
    }
    // ���� ��Ÿ��
    public void AttackCoolTime()
    {
        pAttack.AttackCoolTime();
    }

    // ������ ������
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
            // �ǰ�
        }
        else
        {
            Debug.Log("�÷��̾� ���");

            pState.UnitState = UnitState.Die;
            pAnim.SetTrigger("PlayerDie");

            UIController uiCon = GameObject.FindAnyObjectByType<UIController>();
            uiCon.GameOverUI();
        }
    }

    // ���� ����
    void StartSetting()
    {
        SetPlayerSlider();
        pState = GetComponent<PlayerState>();
        pAnim = GetComponentInChildren<Animator>();
    }

    // �����̴� ����
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

    // ���� ���� ǥ��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        // Gizmos.DrawWireCube(atkPos.position, pAttack.aST.atkLenght);
    }
}