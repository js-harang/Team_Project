using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

// ���ʹ� ���� ���
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
    // ���ʹ� ���� ����
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

    // ���ʹ̿��� �ʿ��� ������
    public Animator enemyAnim;
    public GameObject enemyStateUI;
    public Slider enemyHpBar;
    public GameObject player;
    // DamagedAction �� KnockBack ���࿡ �ʿ��� ����
    public Rigidbody rb;

    [Space(10)]
    // ���� ü��
    public float currentHp;
    // �ִ�ü��
    public int maxHp;
    // ���ݷ�
    public int atkPower;
    // ���ʹ̷κ��� �÷��̾���� ����
    public Vector3 dir;
    // �̵��ӵ�
    public float moveSpeed;
    // �ൿ���� ��� �ð�
    public float behaveDelay;
    // ��� �ð� ��� �ϴ� ����
    public float time;
    // ������ �ð��� �������� Ȯ���ϴ� ����
    public bool delayDone;
    // ���� ���� ������ Ȯ���ϴ� ����
    public bool nowAttack;
    // �װ� ���� �ܰ�� �Ѿư��� Ȯ���ϴ� ����
    public bool imDying { set { if(value) bCon.EnemyCount--; } }

    private void Start()
    {
        // ���� �� ��Ʋ ��Ʈ�ѷ��� ���ʹ� ��ü�� ������Ŵ
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

    // ���ʹ� Ȱ��ȭ �� Start ���� ����
    public abstract void EnemyStart();
    // ���ʹ� ���� ���� ����
    public abstract void Appear();
    // ���ʹ̰� Update���� �����ϴ� ����
    public abstract void EnemyMovement();
    // ���ʹ� ��� ���� ���� ����
    public abstract void Idle();
    // ���ʹ̰� �÷��̾ �Ĵٺ�����
    public abstract void LookAtPlayer();
    // ���ʹ� �̵� ���� ����
    public abstract void Move();
    // ���ʹ��� �ൿ �� ���� �������� ����
    public abstract void CalcBehaveDelay();
    // �ð��� �帧�� ���ؼ� ������ �ð��� �����ߴ��� ���
    public abstract void DelayTimeCount();
    // �������� ������ ������
    public abstract void StartRandomPattern();
    // ���ʹ� ���� ���� ����
    public abstract void Attack();
    // ���ʹ̰� ���� ������ Ȯ���ϴ� �ִϸ��̼� �̺�Ʈ�� �޼ҵ�
    public abstract void NowAttackCheck();
    // ���ʹ� ���� UI �����Ͽ� ǥ��
    public abstract void EnemyStateUpdate();
    // ���ʹ� ��� ���� ����
    public abstract void Die();
}