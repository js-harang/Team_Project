using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SkillButton : MonoBehaviour
{
    // �±׷� �ڵ� ����
    PlayerCombat player;
    CheckDuplication chkDup;

    #region ��ų ����� ����

    // ���� ��ư �̸�
    public int buttonIdx;
    
    [Space(10)]
    // ��ų ������ ���丮
    [SerializeField] SkillDirectory skillDirectory;

    #endregion

    #region ��ų ������ ����
    [Space(10)]
    // �ڽĿ�����Ʈ SkillData �� ���� �� ��
    [SerializeField] SkillData skillData;

    // �ڵ����� ������
    [SerializeField] Attack btnSkill;
    public Attack Skill { get { return btnSkill; } set { btnSkill = value; } }
    
    [SerializeField] Image skillIcon;
    public Image SkillIcon { get { return skillIcon; } set { skillIcon = value; } }
    #endregion

    #region ��ų ��Ÿ�� ����
    // ��Ÿ�� �ڷ�ƾ ���� Ȯ�ο� ����
    bool isCoolTime;

    float coolTime;
    public float CoolTime { get { return coolTime; } set { coolTime = value; } }

    // �ڽĿ�����Ʈ �� ��Ÿ�� ������ �ֱ�
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

        // ��ų�� �������� ����
        if (Skill == null)
        {
            Debug.Log("�����");
            return;
        }

        // ��ų ��Ÿ�� �� 0 ���ϰų� �޺� �� �̸� ����
        if (coolTime <= 0 || btnSkill.isComboing)
        {
            if (btnSkill.useMana > player.CurrentMp && btnSkill.isComboing == false)
            {
                Debug.Log("������ �����մϴ�.");
                return;
            }

            AttackProcess();
        }
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region ���� ���� �� ��Ÿ��
    void AttackProcess()
    {
        // ���� ����
        SkillSetting();
        // ��Ÿ�� ����
        if (!isCoolTime)
        {
            Debug.Log("��Ÿ�� ����");
            StartCoroutine(SkillCoolTime());
        }
    }
    #endregion

    #region ���� �� �߰��� ��ų�� ������ ����
    void SkillSetting()
    {
        player.attack = btnSkill;
        player.Attack();
    }
    #endregion

    #region ��ų ��Ÿ�� �۵�
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

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    IEnumerator LoadSkill()
    {
        // 0000000013
        string cuid = PlayerPrefs.GetString("characteruid");
        string skill = "skill_" + buttonIdx;
        string url = GameManager.gm.path + "loadskill.php";
        Debug.Log(skill);

        WWWForm form = new WWWForm();
        form.AddField("cuid", cuid);
        form.AddField("skill", skill);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                int idx = int.Parse(www.downloadHandler.text);
                Debug.Log(idx);

                // 0 �̻��̸� �ҷ�����
                if (idx > 0)
                {
                    btnSkill = skillDirectory.skillAtks[idx];
                    SkillIcon.sprite = Skill.sprite;
                    SetSkillData();
                }

                if (btnSkill != null)
                {
                    if (chkDup.CheckSkillDuplication(idx))
                    {
                        Debug.Log("�ߺ� �߰� " + this.name);
                    }
                    // 0 �̸� �⺻������ null ����
                }
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }

    void LoadSkillData()
    {
        StartCoroutine(LoadSkill());
    }

    public void SaveSkillData()
    {
        int idx;
        if (btnSkill == null)
            idx = 0;
        else
           idx = btnSkill.skillIdx;

        PlayerPrefs.SetInt("skill_" + buttonIdx, idx);
        PlayerPrefs.Save();
    }

    public void SetSkillData()
    {
        skillData.Skill = Skill;
        skillData.SkillIcon.sprite = SkillIcon.sprite;
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region ���� ����
    void StartSetting()
    {
        skillIcon = GetComponent<Image>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCombat>();
        skillData = GetComponentInChildren<SkillData>();

        coolTimeIcon.fillAmount = 0;
        coolTime = 0f;

        chkDup = GetComponentInParent<CheckDuplication>();

        LoadSkillData();
    }
    #endregion
}