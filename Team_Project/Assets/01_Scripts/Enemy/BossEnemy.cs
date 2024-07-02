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
    public Vector3 playerPosition;

    // ������ �̸�
    public string bossName;
    // ���� ü��
    public float currentHp;
    // �ִ�ü��
    public int maxHp;
    // ���ݷ�
    public int atkPower;


    void Start() 
    {
        // ���� �� ��Ʋ ��Ʈ�ѷ��� ���� ��ü�� ������Ŵ
        bCon = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bCon.BattleState = BattleState.BossAppear;
        bCon.BossCount++;

        //�ʿ��� ������Ʈ�� ������
        bossNav = GetComponent<NavMeshAgent>();
        bossAnim = GetComponentInChildren<Animator>();
        playerPosition = GameObject.FindWithTag("Player").transform.position;

        Appear();
    }

    void Update() 
    {
        Move();
    }

    public abstract void Appear();
    public abstract void Idle();
    public abstract void Move();
    public abstract void Attack();
    public abstract void Hit();
    public abstract void Die();
}
