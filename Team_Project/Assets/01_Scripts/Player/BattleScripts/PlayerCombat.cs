using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : DamagedAction
{
    #region 플레이어 수치 관련
    // 현재 체력
    [SerializeField]
    float currentHp;
    // 최대체력
    [SerializeField]
    int maxHp;
    // 공격력
    [SerializeField]
    int atkPower;

    // 플레이어 현재 마나
    [SerializeField]
    float currentMp;
    public float CurrentMp { get { return currentMp; } set { currentMp = value; } }

    // 플레이어 최대 마나
    [SerializeField]
    int maxMp;
    #endregion

    #region 공격 관련
    [Space(10)]
    public AttackSO attack;
    public AttackSO previousAttack;

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
    #region 플레이어 슬라이더 바
    public Slider hpSld;
    public Slider mpSld;
    #endregion

    public Animator anim;
    public PlayerState pbs;


    private void Update()
    {
        ExitAttack();
    }

    public void Attack()
    {
        // 공격이 없으면 리턴
        if (attack == null)
            return;

        #region 이전 공격과 지금 공격이 다르면 이전공격 콤보 초기화
        if (previousAttack != attack)
        {
            ResetCombo();
        }
        #endregion

        // 공격 상태 가 아니며 콤보 회수가 공격 배열들 보다 작거나 같을때 실행
        if (pbs.UnitBS == UnitBattleState.Idle && comboCounter <= attack.animOCs.Length)
        {
            // 공격중지 매소드 실행 취소
            CancelInvoke("ResetCombo");


            anim.runtimeAnimatorController = attack.animOCs[comboCounter];

            atkDamage = attack.damage[comboCounter];
            canMoveAtk = attack.canMoveAtk;

            // 콤보중이 아니면 마나 소모
            if (!attack.isComboing)
            {
                currentMp -= attack.useMana;
            }
            SetPlayerSlider();

            anim.Play("Attack", 0, 0);

            previousAttack = attack;
            previousAttack.isComboing = true;

            comboCounter++;

            if (comboCounter + 1 > attack.animOCs.Length)
            {
                Debug.Log("콤보초기화");
                ResetCombo();
            }

        }
    }

    #region 공격 중지(인보크 시간내에 공격 없으면 실행됨)
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
    #endregion

    #region 콤보 리셋
    void ResetCombo()
    {
        comboCounter = 0;
        // 이전 공격이 있을때
        if (previousAttack != null)
        {
            previousAttack.isComboing = false;
        }
    }
    #endregion

    #region 공격 판정(플레이어 공격력 * 스킬계수)
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

                Vector3 epos = enemy.transform.position;
                Ray ray = new (transform.position, epos - transform.position);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Instantiate(effectPrefebs, hit.point, Quaternion.identity);
                }
            }
        }
    }
    #endregion

    #region 공격중인지 확인
    public void AttackStateTrue()
    {
        pbs.UnitBS = UnitBattleState.Attack;
    }

    public void AttackStateFalse()
    {
        pbs.UnitBS = UnitBattleState.Idle;
    }
    #endregion

    #region 플레이어 데미지 입을때
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

    #region 슬라이더 세팅
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