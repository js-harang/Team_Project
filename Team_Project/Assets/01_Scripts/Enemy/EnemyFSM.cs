using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyFSM : BattleStatus
{
    // ���ʹ� ���� ���
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die,
    }
    
    // ���ʹ� ���� ����
    EnemyState m_State;

    // ĳ���� ��Ʈ�ѷ� ������Ʈ
    CharacterController cc;

    // �÷��̾� �߰� ����
    public float findDistance = 8f;
    
    // ���� ���� ����
    public float attackDistance = 2f;

    // �÷��̾� Ʈ������
    public Transform player;

    // �̵� �ӵ�
    public float moveSpeed = 5f;

    /*    // ���ʹ� ���ݷ�
        public int atkPower = 3;*/

    // ���� �ð�
    float currentTime = 0;
    
    // ���� ������ �ð�
    float attackDelay = 2f;

    // �ʱ� ��ġ ����� ����
    Vector3 originPos;
    Quaternion originRot;

    // �̵� ���� ����
    public float moveDistance = 20f;

    /*    // ���ʹ��� ü��
        public float currentHp = 15;*/

    /*    // ���ʹ��� �ִ� ü��
        int maxHp = 15;*/

    // ���ʹ� hp �����̴� ����
    public Slider hpSlider;

    // �ִϸ����� ����
    Animator anim;

    // ������̼� ������Ʈ ����
    NavMeshAgent enemyNav;

    BattleController bCon;

    private void Start()
    {
        // ������ ���ʹ� ���¸� ���
        m_State = EnemyState.Idle;

        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();

        // �ڽ��� �ʱ� ��ġ�� ȸ�� ���� �����ϱ�
        originPos = transform.position;
        originRot = transform.rotation;

        // �ڽ� ������Ʈ�κ��� �ִϸ����� ���� �޾ƿ���
        anim = GetComponentInChildren<Animator>();

        // ������̼� ������Ʈ ������Ʈ �޾ƿ���
        enemyNav = GetComponent<NavMeshAgent>();

        // �����ϸ鼭 ��Ʋ ��Ʈ�ѷ��� ���� ���ʹ� ī���͸� �ϳ� �ø���. 
        bCon = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bCon.EnemyCount++;
    }

    private void Update()
    {
        // ���� ���¸� üũ�� �ش� ���º��� ������ ����� ����
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }

        // ���� hp(%)�� hp �����̴��� value�� �ݿ�
        hpSlider.value = (float)currentHp / maxHp;
    }

    void Idle()
    {
        // ����, �÷��̾�� �Ÿ��� �׼� ���� �̳���� Move ���·� ��ȯ
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            Debug.Log("IdleToMove");

            m_State = EnemyState.Move;
            print("���� ��ȯ: Idle -> Move");

            // �̵� �ִϸ��̼����� ��ȯ
            anim.SetTrigger("IdleToMove");
        }
    }

    void Move()
    {
        // ����, ���� ��ġ�� �ʱ� ��ġ���� �̵� ���� ������ �Ѿ�ٸ�
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            // ���� ���¸� ����(Return)�� ��ȯ
            m_State = EnemyState.Return;
            // print("���� ��ȯ : Move -> Return");
        }
        // ����, �÷��̾���� �Ÿ��� ���� ���� ���̶�� �÷��̾ ���� �̵�
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // ������̼����� �����ϴ� �ּ� �Ÿ��� ���� ���� �Ÿ��� ����
            enemyNav.stoppingDistance = attackDistance;

            // ������̼��� �������� �÷��̾��� ��ġ�� ����
            enemyNav.destination = player.position;

            /* // �̵� ���� ����
             Vector3 dir = (player.position - transform.position).normalized;

             // �÷��̾ ���� ������ ��ȯ�Ѵ�.
             transform.forward = dir; */
        }
        // �׷��� �ʴٸ�, ���� ���¸� ����(Attack)���� ��ȯ
        else
        {
            // ������̼� ������Ʈ�� �̵��� ���߰� ��θ� �ʱ�ȭ
            /*Enemy.isStopped = true;
            Enemy.ResetPath(); */

            m_State = EnemyState.Attack;
            print("���� ��ȯ: Move -> Attack");

            // ���� �ð��� ���� ������ �ð���ŭ �̸� ������� ����
            currentTime = attackDelay;

            // ���� ��� �ִϸ��̼� �÷���
            anim.SetTrigger("MoveToAttackDelay");
        }
    }

    void Attack()
    {
        // ����, �÷��̾ ���� ���� �̳��� �ִٸ� �÷��̾ ����
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // ������ �ð����� �÷��̾ ����
            currentTime += Time.deltaTime;

            if (currentTime > attackDelay)
            {
                //player.GetComponent<PlayerMove>().DamageAction(attackPower);
                //print("����");
                currentTime = 0;

                /* AttackAction(); */
                // ���� �ִϸ��̼� �÷���
                anim.SetTrigger("StartAttack");
            }
        }
        // �׷��� �ʴٸ�, ������¸� �̵����� ��ȯ(���߰� �ǽ�)
        else
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ : Attack -> Move");
            currentTime = 0;

            // �̵� �ִϸ��̼� �÷���
            anim.SetTrigger("AttackToMove");
        }
    }

    // �÷��̾��� ��ũ��Ʈ�� ������ ó�� �Լ��� ����
    public void AttackAction()
    {
        PlayerCombat pbc = player.gameObject.GetComponent<PlayerCombat>();
        pbc.Hurt(atkPower);
    }

    void Return()
    {
        // ����, �ʱ� ��ġ���� �Ÿ��� 0.1f �̻��̶�� �ʱ� ��ġ ������ �̵�
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            // ������̼��� �������� �ʱ� ����� ��ġ�� ����
            enemyNav.destination = originPos;

            // ������̼����� �����ϴ� �ּ� �Ÿ��� 0���� ����
            enemyNav.stoppingDistance = 0;
        }
        // �׷��� �ʴٸ�, �ڽ��� ��ġ�� �ʱ� ��ġ�� �����ϰ� ���� ���¸� ���� ��ȯ
        else
        {
            // ������̼� ������Ʈ�� �̵��� ���߰� ��θ� �ʱ�ȭ
            enemyNav.isStopped = true;
            enemyNav.ResetPath();

            // ��ġ ���� ȸ�� ���� �ʱ� ���·� ��ȯ
            transform.position = originPos;
            transform.rotation = originRot;

            // hp�� �ٽ� ȸ��
            currentHp = maxHp;

            m_State = EnemyState.Idle;
            print("���� ��ȯ : Return -> Idle");

            // ��� �ִϸ��̼����� ��ȯ�ϴ� Ʈ�������� ȣ��
            anim.SetTrigger("MoveToIdle");
        }
    }

    //////////// �ǰ��� /////////////
    void Damaged()
    {
        // �ǰ� ���¸� ó���ϱ� ���� �ڷ�ƾ ����
        StartCoroutine(DamageProcess());
    }
    // ������ ó���� �ڷ�ƾ �Լ�
    IEnumerator DamageProcess()
    {
        // �ǰ� ��� �ð���ŭ ��ٸ�
        yield return new WaitForSeconds(1f);

        // ���� ���¸� �̵� ���·� ��ȯ
        m_State = EnemyState.Move;
        print("���� ��ȯ : Damaged => Move");
    }
    // ������ ���� �Լ�
    public void HitEnemy(float hitPower)
    {
        Debug.Log("���ʹ� �ǰ� ȣ��");
        // ����, �̹� �ǰ� �����̰ų� ��� ���� �Ǵ� ���� ���¶��
        // �ƹ��� ó���� ���� �ʰ� �Լ��� ����
        if (m_State == EnemyState.Die || m_State == EnemyState.Return
            )
        {
            return;
        }

        // ������̼� ������Ʈ�� �̵��� ���߰� ��θ� �ʱ�ȭ
        enemyNav.isStopped = true;
        enemyNav.ResetPath();

        // �÷��̾��� ���ݷ¸�ŭ ���ʹ��� ü���� ���ҽ�Ŵ
        currentHp -= hitPower;

        // ���ʹ��� ü���� 0���� ũ�� �ǰ� ���·� ��ȯ
        if (currentHp > 0)
        {
            m_State = EnemyState.Damaged;
            print("���� ��ȯ : Any state -> Damaged");

            // �ǰ� �ִϸ��̼��� �÷���
            anim.SetTrigger("Damaged");

            Damaged();
        }
        // �׷��� �ʴٸ� ���� ���·� ��ȯ
        else
        {
            m_State = EnemyState.Die;
            print("���� ��ȯ : Any state -> Die");

            // ���� �ִϸ��̼��� �÷���
            anim.SetTrigger("Die");
            Die();
        }
    }
    // ���� ���� �Լ�
    void Die()
    {
        // �������� �ǰ� �ڷ�ƾ�� ����
        StopAllCoroutines();

        // ���� ���¸� ó���ϱ� ���� �ڷ�ƾ�� ����
        StartCoroutine(DieProcess());
    }
    IEnumerator DieProcess()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ�� ��Ȱ��ȭ
        cc.enabled = false;
        bCon.EnemyCount--;

        // 2�� ���� ��ٸ� �Ŀ� �ڱ� �ڽ��� ����
        yield return new WaitForSeconds(2f);
        print("�Ҹ�!");
        gameObject.SetActive(false);
    }
}