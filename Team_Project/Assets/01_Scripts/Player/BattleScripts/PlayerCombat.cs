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

        // 공격 상태 가 아니며 콤보 회수가 공격 배열들 보다 작거나 같을때 실행
        if (pbs.UnitBS == UnitBattleState.Idle && comboCounter <= attack.animOCs.Length)
        {
            // 공격중지 매소드 실행 취소
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
        // GetCurrentAnimatorStateInfo(0) : 현재 애니메이션의 정보
        // IsTag : 애니메이션 의 현재 스테이트 의 태그 비교
        // normalizeTime : 애니메이션 진행사항(0이면 시작안함, 1이면 애니매이션 완료)
        if (pbs.UnitBS == UnitBattleState.Idle && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            // 설정된 시간 후에 콤보 리셋
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

    #region 플레이어 데미지 입을때
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
            // 피격
        }
        else
        {
            Debug.Log("플레이어 사망");

            pbs.UnitBS = UnitBattleState.Die;
            anim.SetTrigger("PlayerDie");

            UIController uiCon = GameObject.FindAnyObjectByType<UIController>();
            uiCon.GameOverUI();
        }
    }
    #endregion
}
