using UnityEngine;
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

public abstract class BossFSM : DamagedAction
{   
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
            time = 0;
            CalcBehaveDelay();
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

    public BattleController bCon;

    // �������� �ʿ��� ������
    public Animator bossAnim;
    public GameObject bossStateUI;
    public TMP_Text bossName_text;
    public Slider bossHpBar;
    public GameObject player;
    public Collider myColl;
    // ������ ������ ����
    public SkinnedMeshRenderer bossSkin;

    // �ڽ��� ID
    public EnemyID enemyID;

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
    // ���� ���� ������ Ȯ���ϴ� ����
    public bool nowAttack;
    // �װ� ���� �ܰ�� �Ѿư��� Ȯ���ϴ� ����
    public bool imDying { set { if (value) bCon.BossCount--; } }

    void Start() 
    {
        // ���� �� ��Ʋ ��Ʈ�ѷ��� ���� ��ü�� ������Ŵ
        bCon = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bCon.BattleState = BattleState.BossAppear;
        bCon.BossCount++;
        
        BossStart();
    }

    void Update() 
    {
        if (bState != BossState.Die)
        {
            BossMovement();

            if (nowAttack == true || bState == BossState.Appear)
                return;

            DelayTimeCount();
        }
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
    public abstract void StartRandomPattern();
    // ���� ���� ���� ����
    public abstract void Attack();
    // ������ ���� ������ Ȯ���ϴ� �ִϸ��̼� �̺�Ʈ�� �޼ҵ�
    public abstract void NowAttackCheck();
    // ���� ���� UI �����Ͽ� ǥ��
    public abstract void BossStateUpdate();
    // ���� ��� ���� ����
    public abstract void Die();
}
