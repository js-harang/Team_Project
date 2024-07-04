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
    public Animator bossAnim;
    public GameObject bossStateUI;
    public TMP_Text bossName_text;
    public Slider bossHpBar;
    public GameObject player;
    public Vector3 playerPosition;

    // ������ �̸�
    public string bossName;
    // ���� ü��
    public float currentHp;
    // �ִ�ü��
    public int maxHp;
    // ���ݷ�
    public int atkPower;
    // �̵��ӵ�
    public int moveSpped;
    // ���� ���� ����
    public float attackDistance;
    // ���� �� ��� �ð�
    public float attackDelay;


    void Start() 
    {
        BossStart();
        Appear();
    }

    void Update() 
    {
        BossMovement();
    }

    // ���� Ȱ��ȭ �� Start ���� ����
    public abstract void BossStart();
    // ���� ���� ���� ����
    public abstract void Appear();
    // ���� ��� ���� ���� ����
    public abstract void Idle();
    // ������ �÷��̾ �Ĵٺ�����
    public abstract void LookAtPlayer();
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
    // Update���� ����� ������ �ൿ��
    public abstract void BossMovement();
}
