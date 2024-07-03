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

    // �������� �ʿ��� ������
    public NavMeshAgent bossNav;
    public Animator bossAnim;
    public GameObject bossStateUI;
    public TMP_Text bossName_text;
    public Slider bossHpBar;
    public GameObject player;

    // ������ �̸�
    public string bossName;
    // ���� ü��
    public float currentHp;
    // �ִ�ü��
    public int maxHp;
    // ���ݷ�
    public int atkPower;
    // ���� ���� ����
    public float attackDistance;
    // ���� �� ��� �ð�
    public float attackDelay;


    void Start() 
    {
        // ���� �� ��Ʋ ��Ʈ�ѷ��� ���� ��ü�� ������Ŵ
        bCon = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bCon.BattleState = BattleState.BossAppear;
        bCon.BossCount++;

        //�ʿ��� ������ ������
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

    // ���� ���� ���� ����
    public abstract void Appear();
    // ���� ��� ���� ���� ����
    public abstract void Idle();
    // ���� ������̼� ���� �� ��� �ʱ�ȭ
    public abstract void StopMove();
    // ���� �̵� ���� ����
    public abstract void Move();
    // ���� ���� ���� ����
    public abstract void Attack();
    // ������ �ǰ� ���� ����
    public abstract void Hit();
    // ���� ���� UI �����Ͽ� ǥ��
    public abstract void BossStateUpdate();
    // ���� ��� ���� ����
    public abstract void Die();
}
