using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleControllerTest : BattleStatus
{
    // (X Ű �Ϲݰ���), (C ����), (A, S, D ��ų)
    #region �÷��̾� ��ġ ���� (ü��, ���ݷ� ���� BattleStatus ���� ��ӹ���)
////////////////////////////////////////////////////////////////////////////////////

    // �÷��̾� ���� ����
    [SerializeField]
    float currentMp;

    // �÷��̾� �ִ� ����
    [SerializeField]
    int maxMp;

    // �Ҹ�Ǵ� ����
    [SerializeField]
    int useMana;

    [SerializeField]
    int skillDamage;
    ////////////////////////////////////////////////////////////////////////////////////
    #endregion

    [Space(10)]
    public int index;
    
    [Space(10)]
    #region ���� ����
////////////////////////////////////////////////////////////////////////////////////

    public Transform atkPos;
    public Transform[] atkPositions;

    // ���� Ÿ��
    public int atkType;

    // ���� ����
    public Vector3 atkLenght;

////////////////////////////////////////////////////////////////////////////////////
    #endregion

    [Space(10)]
    #region �� ���� ����
////////////////////////////////////////////////////////////////////////////////////
 
    Collider[] enemys;

    public LayerMask enemyLayer;

////////////////////////////////////////////////////////////////////////////////////
    #endregion

    [Space(10)]
    #region �÷��̾� �����̴� ��
    // �÷��̾� �����̴���
    public Slider hpSld;
    public Slider mpSld;
    #endregion

    // �÷��̾� �ִϸ�����
    Animator pAnim;

    // �÷��̾� ����
    PlayerState pState;


    private void Start()
    {
        StartSetting();
        atkPos = atkPositions[index];
    }

    void Update()
    {// ���� ���¸� Ű�Է� ����
        if (pState.UnitState == UnitState.Die)
            return;

        KeyInput();       // ����Ű �Է�
    }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    #region Ű�Է�
    void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.X))        // �Ϲ� ���� �Է�
        {
            // AttackStart();
        }
        else if (Input.GetKeyDown(KeyCode.Z))   // A Ű ��ų �Է�
        {
            // AttackStart();
        }
    }
    #endregion

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region ���� ����
    public void AttackSetting(int _atkType, int _damage, int _atkPositionIndex, Vector3 _atkLenght, int _useMana)
    {
        useMana = _useMana;
        atkType = _atkType;
        index = _atkPositionIndex;
        atkLenght = _atkLenght;
        skillDamage = _damage;
/*
        Debug.Log("������" + atkPower);
        Debug.Log("�Ҹ𸶳�" + useMana);
        Debug.Log("���� Ÿ��" + atkType);
        Debug.Log("�ε���" + index);
        Debug.Log("���� ����" + atkLenght);*/
    }
    #endregion

    #region ���� ����
    public void AttackStart()
    {
        if (currentMp > useMana)
        {
           // Debug.Log("���� Ÿ�� : " + atkType);
            currentMp -= useMana;
            AttackAnim(atkType);
            SetMpSlider();
        }
        else
            Debug.Log("������ �����մϴ�.");
    }
    #endregion

    #region ���� �ִϸ��̼� ���
    public void AttackAnim(int atkType)
    {
        pAnim.SetTrigger("IsAttack");
        pAnim.SetFloat("Attack", atkType);
    }
    #endregion

    #region �������� (�ִϸ��̼� ���� �ҷ���)
    public void AttackEnemy()
    {
        Attack(atkPos, skillDamage);
    }
    #endregion

    #region ���� ����
    public void Attack(Transform _atkPosition, int _skillDamage)
    {
        Vector3 position = _atkPosition.transform.position;
        enemys = Physics.OverlapBox(position, atkLenght, Quaternion.identity, enemyLayer);

        // ������ �� ������Ʈ �鿡 ������ �ֱ�
        foreach (Collider enemy in enemys)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                Debug.Log("�� ���� ������ :" + _skillDamage);

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

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region �÷��̾� ������ ������
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
    #endregion

    #region ���� ����
    void StartSetting()
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
    void SetHpSlider()
    {
        hpSld.value = currentHp / maxHp;
    }
    void SetMpSlider()
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