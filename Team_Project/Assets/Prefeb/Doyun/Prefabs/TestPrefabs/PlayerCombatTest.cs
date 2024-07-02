using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatTest : BattleStatus
{
    public AttackSO attack;
    public float delayComboTime;
    float lastClickedTime;
    int comboCounter;
    public LayerMask enemyLayer;

    public Animator anim;
    public PlayerState pbs;

    public Transform atkPos;
    public Transform[] atkPositions;

    Collider[] enemys;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        ExitAttack();
    }

    void Attack()
    {
        if (attack == null)
            return;

        // ���� ���� �� �ƴϸ� �޺� ȸ���� ���� �迭�� ���� �۰ų� ������ ����
        if (pbs.UnitBS == UnitBattleState.Idle && comboCounter <= attack.animOCs.Length)
        {
            // �������� �żҵ� ���� ���
            CancelInvoke("ResetCombo");

            if (Time.time - lastClickedTime >= 0.2f)
            {
                anim.runtimeAnimatorController = attack.animOCs[comboCounter];
                anim.Play("Attack", 0, 0);

                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter + 1 > attack.animOCs.Length)
                    comboCounter = 0;
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
            Debug.Log("asdasd");
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
        Debug.Log(attack.damage[comboCounter]);
        
        atkPos = atkPositions[attack.atkPosIdx];
        Vector3 position = atkPos.transform.position;

        enemys = Physics.OverlapBox(position, attack.atkLength, Quaternion.identity, enemyLayer);

        foreach (Collider enemy in enemys)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                float atkDamage = atkPower * attack.damage[comboCounter];
                Debug.Log("�� ���� ������ :" + atkDamage);

                // float �� ��ȯ �ʿ�
                EnemyFSM enemyFsm = enemy.GetComponent<EnemyFSM>();
                enemyFsm.HitEnemy(atkDamage);
            }
        }

        // ���� ���ݷ�,����Ʈ ���
    }

    public void AttackStateTrue()
    {
        pbs.UnitBS = UnitBattleState.Attack;
    }

    public void AttackStateFalse()
    {
        pbs.UnitBS = UnitBattleState.Idle;
    }
}
