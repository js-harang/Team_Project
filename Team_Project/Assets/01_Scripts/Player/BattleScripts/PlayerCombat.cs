using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : DamagedAction
{

    #region �÷��̾� ��ġ ����

    #region ���ݷ�
    [SerializeField] int atkPower;
    public int AtkPower { get { return atkPower; } set { atkPower = value; } }
    #endregion

    #region ���� ü��
    [SerializeField] float currentHp;
    public float CurrentHp { get { return currentHp; } set { currentHp = value; } }
    #endregion

    #region �ִ�ü��
    [SerializeField] int maxHp;
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    #endregion

    #region �÷��̾� ���� ����
    [SerializeField]
    float currentMp;
    public float CurrentMp { get { return currentMp; } set { currentMp = value; } }
    #endregion

    #region �÷��̾� �ִ� ���� 
    [SerializeField] int maxMp;
    public int MaxMp { get { return maxMp; } set { maxMp = value; } }
    #endregion

    UIController ui;

    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region ���� ����

    #region ���� ��ų ����
    [Space(10)]
    public Attack attack;
    public Attack previousAttack;

    [Space(10)]
    private float atkDamage;
    public float delayComboTime;
    [SerializeField] int comboCounter;

    [Space(10)]
    public bool canMoveAtk;
    #endregion

    #region �� ������Ʈ ����
    [Space(10)]
    public LayerMask enemyLayer;
    private Collider[] enemys;
    #endregion

    #region ���� ��ġ
    [Space(10)]
    public Transform atkPos;
    public Transform[] atkPositions;
    #endregion

    #region ���� ����Ʈ
    [Space(10)]
    public GameObject effectPrefebs;
    #endregion
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    public Animator anim;
    public PlayerState pbs;
    BattleController bCon;

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // �޼ҵ�
    private void Start()
    {
        GameManager.gm.Player = GetComponent<PlayerCombat>();
        bCon = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        ui = GameManager.gm.UI;
        SetPlayerState();

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ExitAttack();
    }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void SetPlayerState()
    {
        atkPower = GameManager.gm.AtkPower;
        maxHp = GameManager.gm.MaxHp;
        maxMp = GameManager.gm.MaxMp;

        currentHp = maxHp;
        currentMp = maxMp;
    }
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    // ���� ���ϸ��̼� ����
    public void Attack()
    {
        // ������ ������ ����
        if (attack == null)
            return;

        #region ���� ���ݰ� ���� ������ �ٸ��� �������� �޺� �ʱ�ȭ
        if (previousAttack != null && previousAttack != attack)
            ResetCombo();
        #endregion

        // ���� ���� �� �ƴϸ� �޺� ȸ���� ���� �迭�� ���� �۰ų� ������ ����
        if (pbs.UnitBS == UnitBattleState.Idle && comboCounter <= attack.animOCs.Length)
        {
            // �������� �żҵ� ���� ���
            CancelInvoke("ResetCombo");

            #region ���� ����
            anim.runtimeAnimatorController = attack.animOCs[comboCounter];

            atkDamage = attack.damage[comboCounter];
            canMoveAtk = attack.canMoveAtk;

            // �޺����� �ƴϸ� ���� �Ҹ�
            if (!attack.isComboing)
                currentMp -= attack.useMana;

            // mp�����̴� ����
            GameManager.gm.UI.SetMpSlider(CurrentMp, MaxMp);
            #endregion

            anim.Play("Attack", 0, 0);

            previousAttack = attack;
            previousAttack.isComboing = true;

            comboCounter++;

            #region �ִ� �޺� ���޽� �ʱ�ȭ
            if (comboCounter + 1 > attack.animOCs.Length)
            {
                Debug.Log("�޺��ʱ�ȭ");
                ResetCombo();
            }
            #endregion
        }
    }
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region ExitAttack() ���� ����(�κ�ũ �ð����� ���� ������ �����)
    void ExitAttack()
    {
        // GetCurrentAnimatorStateInfo(0) : ���� �ִϸ��̼��� ����
        // IsTag : �ִϸ��̼� �� ���� ������Ʈ �� �±� ��
        // normalizeTime : �ִϸ��̼� �������(0�̸� ���۾���, 1�̸� �ִϸ��̼� �Ϸ�)
        if (pbs.UnitBS == UnitBattleState.Idle && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            // ������ �ð� �Ŀ� �޺� ����
            Invoke("ResetCombo", delayComboTime);
        }
    }
    #endregion

    #region ResetCombo() �޺� ����
    void ResetCombo()
    {
        comboCounter = 0;
        // ���� ������ ������
        if (previousAttack != null)
        {
            previousAttack.isComboing = false;
        }
    }
    #endregion

    #region AttackState True, False ���������� Ȯ��
    public void AttackStateTrue()
    {
        pbs.UnitBS = UnitBattleState.Attack;
    }

    public void AttackStateFalse()
    {
        pbs.UnitBS = UnitBattleState.Idle;
    }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region AttackEnemy() ���� ����(�÷��̾� ���ݷ� * ��ų���)
    public void AttackEnemy()
    {
        atkPos = atkPositions[attack.atkPosIdx];
        Vector3 position = atkPos.transform.position;

        enemys = Physics.OverlapBox(position, attack.atkLength, Quaternion.identity, enemyLayer);

        foreach (Collider enemy in enemys)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                #region ������ �ֱ�
                float sumDamage = atkPower * atkDamage;
                DamagedAction damageAct = enemy.GetComponent<DamagedAction>();

                damageAct.Damaged(sumDamage);
                damageAct.KnockBack(transform.position, attack.knockBackForce);
                #endregion

                #region ����Ʈ
                Vector3 epos = enemy.transform.position;
                Ray ray = new(transform.position, epos - transform.position);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    AudioSource audio = effectPrefebs.GetComponent<AudioSource>();
                    audio.clip = attack.hitSound;
                    Instantiate(effectPrefebs, hit.point, Quaternion.identity);
                }
                #endregion
            }
        }
    }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // �÷��̾� �ǰ�
    #region Damaged(float damage) �÷��̾� ������ ������
    public override void Damaged(float damage)
    {
        if (pbs.UnitBS == UnitBattleState.Die)
            return;

        pbs.UnitBS = UnitBattleState.Hurt;

        currentHp -= damage;

        bCon.playerHitCount++;

        ui.SetHpSlider(CurrentHp, MaxHp);

        if (currentHp > 0)
        {
            #region �÷��̾� �ǰ�
            // �ǰ�
            anim.SetTrigger("IsHurt");
            #endregion
        }
        else
        {
            #region �÷��̾� ���
            Debug.Log("�÷��̾� ���");

            pbs.UnitBS = UnitBattleState.Die;
            anim.SetTrigger("PlayerDie");

            UIController uiCon = GameObject.FindAnyObjectByType<UIController>();
            uiCon.GameOverUI();
            #endregion
        }
    }
    #endregion

    // �ǰ� ���� ����
    public void EndHurt()
    {
        pbs.UnitBS = UnitBattleState.Idle;
    }

    // �˹�
    public override void KnockBack(Vector3 atkPos, float knockBackForce)
    {
        rb.velocity = Vector3.zero;

        if (pbs.UnitBS == UnitBattleState.Die)
            return;

        float dis = Vector3.Distance(transform.position, atkPos);

        Vector3 dir = transform.position - atkPos;
        dir.Normalize();

        rb.AddForce(dir * (knockBackForce / dis), ForceMode.Impulse);
    }
}