using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : DamagedAction
{
    #region �÷��̾� ��ġ ����
    // ���� ü��
    [SerializeField]
    float currentHp;
    // �ִ�ü��
    [SerializeField]
    int maxHp;
    // ���ݷ�
    [SerializeField]
    int atkPower;

    // �÷��̾� ���� ����
    [SerializeField]
    float currentMp;
    public float CurrentMp { get { return currentMp; } set { currentMp = value; } }

    // �÷��̾� �ִ� ����
    [SerializeField]
    int maxMp;
    #endregion

    #region ���� ����
    [Space(10)]
    // public AttackSO attack;
    public Attack attack;
    // public AttackSO previousAttack;
    public Attack previousAttack;

    [Space(10)]
    public float delayComboTime;
    int comboCounter;
    float atkDamage;

    [Space(10)]
    Collider[] enemys;
    public LayerMask enemyLayer;

    [Space(10)]
    public Transform atkPos;
    public Transform[] atkPositions;

    [Space(10)]
    public bool canMoveAtk;

    [Space(10)]
    public GameObject effectPrefebs;
    #endregion

    [Space(10)]
    #region �÷��̾� �����̴� ��
    public Slider hpSld;
    public Slider mpSld;
    #endregion

    public Animator anim;
    public PlayerState pbs;

    bool isReset;
    IEnumerator co;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        co = ResetCom();
    }

    private void Update()
    {
        ExitAttack();
    }

    public void Attack()
    {
        // ������ ������ ����
        if (attack == null)
            return;

        #region ���� ���ݰ� ���� ������ �ٸ��� �������� �޺� �ʱ�ȭ
        if (previousAttack != attack)
        {
            ResetCombo();
        }
        #endregion

        // ���� ���� �� �ƴϸ� �޺� ȸ���� ���� �迭�� ���� �۰ų� ������ ����
        if (pbs.UnitBS == UnitBattleState.Idle && comboCounter <= attack.animOCs.Length)
        {
            // �������� �ڷ�ƾ�� ����
            StopCoroutine(co);

            anim.runtimeAnimatorController = attack.animOCs[comboCounter];

            atkDamage = attack.damage[comboCounter];
            canMoveAtk = attack.canMoveAtk;
            delayComboTime = attack.delayComboTime;

            // �޺����� �ƴϸ� ���� �Ҹ�
            if (!attack.isComboing)
                currentMp -= attack.useMana;

            SetPlayerSlider();

            anim.Play("Attack", 0, 0);

            previousAttack = attack;
            previousAttack.isComboing = true;

            comboCounter++;

            if (comboCounter + 1 > attack.animOCs.Length)
            {
                ResetCombo();
                StopCoroutine(co);
            }
        }
    }

    #region ���� ����(���� ������ Ÿ�� �ȿ� ���� ������)
    void ExitAttack()
    {
        // GetCurrentAnimatorStateInfo(0) : ���� �ִϸ��̼��� ����
        // IsTag : �ִϸ��̼� �� ���� ������Ʈ �� �±� ��
        // normalizeTime : �ִϸ��̼� �������(0�̸� ���۾���, 1�̸� �ִϸ��̼� �Ϸ�)
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            if (!isReset)
                StartCoroutine(co);
        }
    }
    #endregion

    #region �޺� ����

    void ResetCombo()
    {
        comboCounter = 0;

        // ���� ������ ������
        if (previousAttack != null)
            previousAttack.isComboing = false;
    }

    IEnumerator ResetCom()
    {
        isReset = true;
        yield return new WaitForSeconds(delayComboTime);

        Debug.Log("�ڷ�ƾ �޺��ʱ�ȭ");
        ResetCombo();

        isReset = false;
    }
    #endregion

    #region ���� ����(�÷��̾� ���ݷ� * ��ų���)
    public void AttackEnemy()
    {
        atkPos = atkPositions[attack.atkPosIdx];
        Vector3 position = atkPos.transform.position;

        enemys = Physics.OverlapBox(position, attack.atkLength, Quaternion.identity, enemyLayer);

        foreach (Collider enemy in enemys)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                float sumDamage = atkPower * atkDamage;
                DamagedAction damageAct = enemy.GetComponent<DamagedAction>();

                damageAct.Damaged(sumDamage);
                damageAct.KnockBack(transform.position, attack.knockBackForce);

                Vector3 epos = enemy.transform.position;
                Ray ray = new (transform.position, epos - transform.position);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    AudioSource audio = effectPrefebs.GetComponent<AudioSource>();
                    audio.clip = attack.hitSound;
                    Instantiate(effectPrefebs, hit.point, Quaternion.identity);
                }
            }
        }
    }
    #endregion

    #region ���������� Ȯ��
    public void AttackStateTrue()
    {
        pbs.UnitBS = UnitBattleState.Attack;
    }

    public void AttackStateFalse()
    {
        pbs.UnitBS = UnitBattleState.Idle;
    }
    #endregion

    #region �÷��̾� ������ ������
    public override void Damaged(float damage)
    {
        if (pbs.UnitBS == UnitBattleState.Die)
            return;

        pbs.UnitState = UnitState.Hurt;
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

            pbs.UnitBS = UnitBattleState.Die;
            anim.SetTrigger("PlayerDie");

            UIController uiCon = GameObject.FindAnyObjectByType<UIController>();
            uiCon.GameOverUI();
        }
    }
    #endregion

    public override void KnockBack(Vector3 atkPos, float knockBackForce)
    {
        rb.velocity = Vector3.zero;
        
        float dis = Vector3.Distance(transform.position, atkPos);

        Vector3 dir = transform.position - atkPos;
        dir.Normalize();

        rb.AddForce(dir * (knockBackForce / dis), ForceMode.Impulse);
    }

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
}