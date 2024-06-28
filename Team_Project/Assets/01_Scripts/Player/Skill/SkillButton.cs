using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    PlayerBattleController player;

    #region 스킬 데이터 관련
    [SerializeField]
    SkillData skillData;

    [SerializeField]
    PlayerAttack skill;
    public PlayerAttack Skill { get { return skill; } set { skill = value; } }
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
        // 스킬이 있을때만 실행
        if (skillData.Skill == null)
            return;

        skill = skillData.Skill;

        if (coolTime > 0)
        {
            Debug.Log("쿨타임");
            return;
        }

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
        player.pAttack = skill;
        player.ChangeAttack(skill.aST.atkType);
        player.AttackStart();
    }
    #endregion

    #region 스킬 쿨타임 작동
    IEnumerator SkillCoolTime()
    {
        float tick = 1f / skill.aST.atkDelay;
        float t = 0;
        coolTime = skill.aST.atkDelay;

        coolTimeIcon.fillAmount = 1;

        while (coolTimeIcon.fillAmount > 0)
        {
            coolTimeIcon.fillAmount = Mathf.Lerp(1, 0, t);
            t += (Time.deltaTime * tick);

            coolTime -= Time.deltaTime;

            yield return null;
        }
    }
    #endregion

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region 시작 세팅
    void StartSetting()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBattleController>();
        coolTimeIcon.fillAmount = 0;
        coolTime = 0f;
    }
    #endregion
}