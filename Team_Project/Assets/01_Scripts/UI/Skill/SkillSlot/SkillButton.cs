using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    PlayerCombat player;

    #region 스킬 데이터 관련
    [SerializeField]
    SkillData skillData;

    [SerializeField]
    AttackSO skill;
    public AttackSO Skill { get { return skill; } set { skill = value; } }
    #endregion

    #region 스킬 쿨타임 관련
    float coolTime;
    public float CoolTime { get { return coolTime; } set { coolTime = value; } }
    public Image coolTimeIcon;
    #endregion


    private void Start()
    {
        StartSetting();
    }

    public void OnClicked()
    {
        if (player.pbs.UnitBS != UnitBattleState.Idle)
        {
            return;
        }
        // 스킬이 있을때만 실행
        if (skill == null)
        {
            Debug.Log("비었음");
            return;
        }

        if (coolTime > 0)
            return;

        skill = skillData.Skill;


        AttackProcess();
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region 공격 실행 및 쿨타임
    void AttackProcess()
    {
        // 공격 실행
        SkillSetting();
        // 쿨타임 실행
        StartCoroutine(SkillCoolTime());
    }
    #endregion

    #region 변경 및 추가된 스킬로 설정후 공격
    void SkillSetting()
    {
        player.attack = skill;
        player.Attack();
    }
    #endregion

    #region 스킬 쿨타임 작동
    IEnumerator SkillCoolTime()
    {
        float t = 0;
        float tick = 1f / skill.coolTime;
        coolTime = skill.coolTime;

        coolTimeIcon.fillAmount = 1;

        while (coolTimeIcon.fillAmount > 0)
        {
            coolTimeIcon.fillAmount = Mathf.Lerp(1, 0, t);
            t += (Time.deltaTime * tick);

            coolTime -= Time.deltaTime;

            yield return null;
        }
        skill.canAttack = true;
    }
    #endregion

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region 시작 세팅
    void StartSetting()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCombat>();
        coolTimeIcon.fillAmount = 0;
        coolTime = 0f;
    }
    #endregion
}