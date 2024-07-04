using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : BattleStatus
{
    public AttackSO attack;
    public AttackSO previousAttack;

    public float delayComboTime;
    float lastClickedTime;
    int comboCounter;
    float atkDamage;


    public LayerMask enemyLayer;

    #region �÷��̾� ��ġ ����(ü��,���ݷ� �� �� BattleStatus ���� ��� ����)
    // �÷��̾� ���� ����
    [SerializeField]
    float currentMp;
    public float CurrentMp { get { return currentMp; } set { currentMp = value; } }

    // �÷��̾� �ִ� ����
    [SerializeField]
    int maxMp;
    #endregion

    [Space(10)]
    #region �÷��̾� �����̴� ��
    public Slider hpSld;
    public Slider mpSld;
    #endregion

    public Animator anim;
    public PlayerState pbs;

    public Transform atkPos;
    public Transform[] atkPositions;

    Collider[] enemys;

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
            // �������� �żҵ� ���� ���
            CancelInvoke("ResetCombo");


            anim.runtimeAnimatorController = attack.animOCs[comboCounter];

            atkDamage = attack.damage[comboCounter];

            // �޺����� �ƴϸ� ���� �Ҹ�
            if (!attack.isComboing)
            {
                currentMp -= attack.useMana;
            }
            SetPlayerSlider();

            anim.Play("Attack", 0, 0);

            previousAttack = attack;
            previousAttack.isComboing = true;

            comboCounter++;
            lastClickedTime = Time.time;

            if (comboCounter + 1 > attack.animOCs.Length)
            {
                Debug.Log("�޺��ʱ�ȭ");
                ResetCombo();
            }

        }
    }

    #region ���� ����(�κ�ũ �ð����� ���� ������ �����)
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

    #region �޺� ����
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
                EnemyFSM enemyFsm = enemy.GetComponent<EnemyFSM>();
                enemyFsm.HitEnemy(sumDamage);
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
    public void Hurt(float damage)
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
