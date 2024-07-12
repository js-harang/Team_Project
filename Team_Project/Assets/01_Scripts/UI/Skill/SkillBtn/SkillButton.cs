using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    // 태그로 자동 참조
    PlayerCombat player;
    CheckDuplication chkDup;

    #region 스킬 저장용 변수

    // 현재 버튼 이름
    public int buttonIdx;
    
    [Space(10)]
    // 스킬 가져올 디렉토리
    [SerializeField] SkillDirectory skillDirectory;

    #endregion

    #region 스킬 데이터 관련
    [Space(10)]
    // 자식오브젝트 SkillData 를 참조 할 것
    [SerializeField] SkillData skillData;

    // 자동으로 참조됨
    // AttackSO skill;
    Attack skill;

    // public AttackSO Skill { get { return skill; } set { skill = value; } }
    public Attack Skill { get { return skill; } set { skill = value; } }
    #endregion

    #region 스킬 쿨타임 관련
    // 쿨타임 코루틴 실행 확인용 변수
    bool isCoolTime;

    float coolTime;
    public float CoolTime { get { return coolTime; } set { coolTime = value; } }

    // 자식오브젝트 의 쿨타임 아이콘 넣기
    [SerializeField] public Image coolTimeIcon;
    #endregion


    private void Start()
    {
        // PlayerPrefs.DeleteAll();
        StartSetting();
    }

    public void OnClicked()
    {
        if (player.pbs.UnitBS != UnitBattleState.Idle)
        {
            return;
        }

        // 스킬이 있을때만 실행
        if (skillData.Skill == null)
        {
            Debug.Log("비었음");
            return;
        }

        skill = skillData.Skill;

        // 스킬 쿨타임 이 0 이하거나 콤보 중 이면 실행
        if (coolTime <= 0 || skill.isComboing)
        {
            if (skill.useMana > player.CurrentMp && skill.isComboing == false)
            {
                Debug.Log("마나가 부족합니다.");
                return;
            }

            AttackProcess();
        }
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region 공격 실행 및 쿨타임
    void AttackProcess()
    {
        // 공격 실행
        SkillSetting();
        // 쿨타임 실행
        if (!isCoolTime)
        {
            Debug.Log("쿨타임 실행");
            StartCoroutine(SkillCoolTime());
        }
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
        isCoolTime = true;

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
        isCoolTime = false;
    }
    #endregion

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void LoadSkillData()
    {
        int idx = PlayerPrefs.GetInt("SkillButton" + buttonIdx, 0);
        Debug.Log("SkillButton" + buttonIdx + " 인덱스 번호 : " + idx);
        // 0 이상이면 불러오기
        if (idx > 0)
        {
            skill = skillDirectory.skillAtks[idx];
            skillData.Skill = skill;
            skillData.SkillIcon.sprite = skillDirectory.skillSprites[idx];
        }

        if (skill == null)
        {
            Debug.Log(this.name + "스킬이 비었음");
            return;
        }

        if (chkDup.CheckSkillDuplication(buttonIdx, idx))
        {
            Debug.Log("중복 발견 " + this.name);
        }
        // 0 이면 기본적으로 null 값임
    }

    public void SaveSkillData()
    {
        int idx;
        if (skillData.Skill == null)
            idx = 0;
        else
           idx = skillData.Skill.skillIdx;

        PlayerPrefs.SetInt("SkillButton" + buttonIdx, idx);
        PlayerPrefs.Save();
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region 시작 세팅
    void StartSetting()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCombat>();
        coolTimeIcon.fillAmount = 0;
        coolTime = 0f;
        chkDup = GetComponentInParent<CheckDuplication>();
        LoadSkillData();
    }
    #endregion
}