using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : BattleStatus
{
    public AttackSO attack;
    public AttackSO previousAttack;

    public float delayComboTime;
    float lastClickedTime;
    int comboCounter;
    float atkDamage;

    public LayerMask enemyLayer;

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
        if (attack == null)
            return;

        if (previousAttack != attack)
        {
            comboCounter = 0;
        }

        // ���� ���� �� �ƴϸ� �޺� ȸ���� ���� �迭�� ���� �۰ų� ������ ����
        if (pbs.UnitBS == UnitBattleState.Idle && comboCounter <= attack.animOCs.Length)
        {
            // �������� �żҵ� ���� ���
            CancelInvoke("ResetCombo");

            if (Time.time - lastClickedTime >= 0.2f)
            {
                anim.runtimeAnimatorController = attack.animOCs[comboCounter];

                atkDamage = attack.damage[comboCounter];
                anim.Play("Attack", 0, 0);

                previousAttack = attack;

                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter + 1 > attack.animOCs.Length)
                {
                    ResetCombo();
                }
            }
        }
    }

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

    void ResetCombo()
    {
        comboCounter = 0;
    }


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

    public void AttackStateTrue()
    {
        pbs.UnitBS = UnitBattleState.Attack;
    }

    public void AttackStateFalse()
    {
        pbs.UnitBS = UnitBattleState.Idle;
    }

    #region �÷��̾� ������ ������
    public void Hurt(float damage)
    {
        if (pbs.UnitBS == UnitBattleState.Die)
            return;

        pbs.UnitState = UnitState.Hurt;
        // Debug.Log("Player Damaged :" + damage);

        currentHp -= damage;

/*        SetPlayerSlider();*/

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
}
