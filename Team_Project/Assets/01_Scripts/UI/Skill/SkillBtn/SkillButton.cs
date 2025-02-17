using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SkillButton : MonoBehaviour
{
    // 태그로 자동 참조
    [SerializeField] PlayerCombat player;
    SkillBtnManager chkDup;
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region 스킬 저장용 변수

    // 현재 버튼 이름
    public int buttonIdx;
    public int btnSkillIdx;
    [Space(10)]
    // 스킬 가져올 디렉토리
    [SerializeField] SkillDirectory skillDirectory;

    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region 스킬 데이터 관련
    [Space(10)]
    // 자식오브젝트 SkillData 를 참조 할 것
    [SerializeField] SkillData skillData;

    // 자동으로 참조됨
    [SerializeField] Attack btnSkill;
    public Attack Skill { get { return btnSkill; } set { btnSkill = value; } }

    [SerializeField] Image skillIcon;
    public Image SkillIcon { get { return skillIcon; } set { skillIcon = value; } }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region 스킬 쿨타임 관련
    // 쿨타임 코루틴 실행 확인용 변수
    bool isCoolTime;

    float coolTime;
    public float CoolTime { get { return coolTime; } set { coolTime = value; } }

    // 자식오브젝트 의 쿨타임 아이콘 넣기
    [SerializeField] public Image coolTimeIcon;
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    private void Awake()
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
        if (Skill == null)
        {
            Debug.Log("비었음");
            return;
        }

        // 스킬 쿨타임 이 0 이하거나 콤보 중 이면 실행
        if (coolTime <= 0 || btnSkill.isComboing)
        {
            if (btnSkill.useMana > player.CurrentMp && btnSkill.isComboing == false)
            {
                Debug.Log("마나가 부족합니다.");
                return;
            }

            AttackProcess();
        }
    }

    /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // 공격

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
        player.attack = btnSkill;
        player.Attack();
    }
    #endregion

    #region 스킬 쿨타임 작동
    IEnumerator SkillCoolTime()
    {
        isCoolTime = true;

        float t = 0;
        float tick = 1f / btnSkill.coolTime;
        coolTime = btnSkill.coolTime;

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

    /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // 스킬 저장, 불러오기

    /*    #region SaveSkillData 스킬을 데이터베이스에 저장
        public void SaveSkillData()
        {
            StartCoroutine(SaveSkill());
        }

        IEnumerator SaveSkill()
        {
            #region 데이터 베이스에 보낼 정보

                #region 저장할 스킬 인덱스 확인
                int idx;
                if (btnSkill == null)
                    idx = 0;
                else
                    idx = btnSkill.skillIdx;
            #endregion

            // 0000000013
            string cuid = PlayerPrefs.GetString("characteruid");
            string skill = "skill_" + buttonIdx;
            string url = GameManager.gm.path + "saveskill.php";

            WWWForm form = new WWWForm();
            form.AddField("cuid", "0000000013");
            form.AddField("skill", skill);
            form.AddField("idx", idx);
            #endregion

            #region 데이터 베이스에 값 전달
            using (UnityWebRequest www = UnityWebRequest.Post(url, form))
            {
                yield return www.SendWebRequest();
                if (www.error == null)
                {
                    Debug.Log(skill + " : " + www.downloadHandler.text + "저장 성공");
                }
                else
                {
                    Debug.Log(www.error);
                }
            }
            #endregion
        }
        #endregion*/

    public void BtnSkillSet()
    {
        // 0 이상이면 불러오기
        if (btnSkillIdx > 0)
        {
            btnSkill = skillDirectory.skillAtks[btnSkillIdx];
            Debug.Log(skillDirectory.skillAtks[btnSkillIdx]);
            Debug.Log(SkillIcon);
            SkillIcon.sprite = Skill.sprite;
            SetSkillData();
        }
    }

    #region SetSkillData 스킬 버튼에 할당된 스킬을 자식오브젝트인 스킬 데이터에 할당
    /// <summary>
    /// 스킬 버튼에 할당된 스킬을 자식오브젝트인 스킬 데이터에 할당
    /// </summary>
    public void SetSkillData()
    {
        skillData.Skill = Skill;
        skillData.SkillIcon.sprite = SkillIcon.sprite;
    }
    #endregion

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region 시작 세팅
    void StartSetting()
    {
        skillIcon = GetComponent<Image>();
        skillData = GetComponentInChildren<SkillData>();

        chkDup = GetComponentInParent<SkillBtnManager>();

        coolTimeIcon.fillAmount = 0;
        coolTime = 0f;
    }
    #endregion
}