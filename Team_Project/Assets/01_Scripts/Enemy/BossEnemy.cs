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
            CalcBehaveDelay();
            time = 0;
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

    [Space(10)]
    // ������ �̸�
    public string bossName;
    // ���� ü��
    public float currentHp;
    // �ִ�ü��
    public int maxHp;
    // ���ݷ�
    public int atkPower;
    // �����κ��� �÷��̾���� ����
    public Vector3 dir;
    // �̵��ӵ�
    public float moveSpeed;
    // �ൿ���� ��� �ð�
    public float behaveDelay;
    // ��� �ð� ��� �ϴ� ����
    public float time;
    // ������ �ð��� �������� Ȯ���ϴ� ����
    public bool delayDone;
    // ���� ���� ������ Ȯ���ϴ� ���� 0 : �⺻ ����, 1 : ���� ��, 2 : ���� ����
    public int nowAttack;

    void Start() 
    {
        BossStart();
        Appear();
    }

    void Update() 
    {
        BossMovement();

        if (nowAttack == 1 || bState == BossState.Appear)
            return;

        DelayTimeCount();
    }

    // ���� Ȱ��ȭ �� Start ���� ����
    public abstract void BossStart();
    // ������ Update���� �����ϴ� ����
    public abstract void BossMovement();
    // ���� ���� ���� ����
    public abstract void Appear();
    // ���� ��� ���� ���� ����
    public abstract void Idle();
    // ������ �÷��̾ �Ĵٺ�����
    public abstract void LookAtPlayer();
    // ���� �̵� ���� ����
    public abstract void Move();
    // ������ �ൿ �� ���� �������� ����
    public abstract void CalcBehaveDelay();
    // �ð��� �帧�� ���ؼ� ������ �ð��� �����ߴ��� ���
    public abstract void DelayTimeCount();
    // �������� ������ ������
    public abstract void RandomPattern();
    // ���� ���� ���� ����
    public abstract void Attack();
    // ������ ���� ������ Ȯ���ϴ� �ִϸ��̼� �̺�Ʈ�� �޼ҵ�
    public abstract void NowAttackCheck();
    // ������ �ǰ� ���� ����
    public abstract void Hit();
    // ���� ���� UI �����Ͽ� ǥ��
    public abstract void BossStateUpdate();
    // ���� ��� ���� ����
    public abstract void Die();
}
