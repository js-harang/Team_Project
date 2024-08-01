using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : DamagedAction
{

    #region 플레이어 수치 관련

    #region 공격력
    [SerializeField] int atkPower;
    public int AtkPower { get { return atkPower; } set { atkPower = value; } }
    #endregion

    #region 현재 체력
    [SerializeField] float currentHp;
    public float CurrentHp { get { return currentHp; } set { currentHp = value; } }
    #endregion

    #region 최대체력
    [SerializeField] int maxHp;
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    #endregion

    #region 플레이어 현재 마나
    [SerializeField]
    float currentMp;
    public float CurrentMp { get { return currentMp; } set { currentMp = value; } }
    #endregion

    #region 플레이어 최대 마나 
    [SerializeField] int maxMp;
    public int MaxMp { get { return maxMp; } set { maxMp = value; } }
    #endregion

    UIController ui;

    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region 공격 관련

    #region 공격 스킬 정보
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

    #region 적 오브젝트 판정
    [Space(10)]
    public LayerMask enemyLayer;
    private Collider[] enemys;
    #endregion

    #region 공격 위치
    [Space(10)]
    public Transform atkPos;
    public Transform[] atkPositions;
    #endregion

    #region 공격 이펙트
    [Space(10)]
    public GameObject effectPrefebs;
    #endregion
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    public Animator anim;
    public PlayerState pbs;
    BattleController bCon;

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // 메소드
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
    // 공격 에니메이션 실행
    public void Attack()
    {
        // 공격이 없으면 리턴
        if (attack == null)
            return;

        #region 이전 공격과 지금 공격이 다르면 이전공격 콤보 초기화
        if (previousAttack != null && previousAttack != attack)
            ResetCombo();
        #endregion

        // 공격 상태 가 아니며 콤보 회수가 공격 배열들 보다 작거나 같을때 실행
        if (pbs.UnitBS == UnitBattleState.Idle && comboCounter <= attack.animOCs.Length)
        {
            // 공격중지 매소드 실행 취소
            CancelInvoke("ResetCombo");

            #region 공격 설정
            anim.runtimeAnimatorController = attack.animOCs[comboCounter];

            atkDamage = attack.damage[comboCounter];
            canMoveAtk = attack.canMoveAtk;

            // 콤보중이 아니면 마나 소모
            if (!attack.isComboing)
                currentMp -= attack.useMana;

            // mp슬라이더 설정
            GameManager.gm.UI.SetMpSlider(CurrentMp, MaxMp);
            #endregion

            anim.Play("Attack", 0, 0);

            previousAttack = attack;
            previousAttack.isComboing = true;

            comboCounter++;

            #region 최대 콤보 도달시 초기화
            if (comboCounter + 1 > attack.animOCs.Length)
            {
                Debug.Log("콤보초기화");
                ResetCombo();
            }
            #endregion
        }
    }
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region ExitAttack() 공격 중지(인보크 시간내에 공격 없으면 실행됨)
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

    #region ResetCombo() 콤보 리셋
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

    #region AttackState True, False 공격중인지 확인
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

    #region AttackEnemy() 공격 판정(플레이어 공격력 * 스킬계수)
    public void AttackEnemy()
    {
        atkPos = atkPositions[attack.atkPosIdx];
        Vector3 position = atkPos.transform.position;

        enemys = Physics.OverlapBox(position, attack.atkLength, Quaternion.identity, enemyLayer);

        foreach (Collider enemy in enemys)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                #region 데미지 주기
                float sumDamage = atkPower * atkDamage;
                DamagedAction damageAct = enemy.GetComponent<DamagedAction>();

                damageAct.Damaged(sumDamage);
                damageAct.KnockBack(transform.position, attack.knockBackForce);
                #endregion

                #region 이펙트
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

    // 플레이어 피격
    #region Damaged(float damage) 플레이어 데미지 입을때
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
            #region 플레이어 피격
            // 피격
            anim.SetTrigger("IsHurt");
            #endregion
        }
        else
        {
            #region 플레이어 사망
            Debug.Log("플레이어 사망");

            pbs.UnitBS = UnitBattleState.Die;
            anim.SetTrigger("PlayerDie");

            UIController uiCon = GameObject.FindAnyObjectByType<UIController>();
            uiCon.GameOverUI();
            #endregion
        }
    }
    #endregion

    // 피격 판정 종료
    public void EndHurt()
    {
        pbs.UnitBS = UnitBattleState.Idle;
    }

    // 넉백
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