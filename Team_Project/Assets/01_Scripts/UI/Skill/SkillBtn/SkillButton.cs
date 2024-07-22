using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SkillButton : MonoBehaviour
{
    // �±׷� �ڵ� ����
    PlayerCombat player;
    SkillBtnManager chkDup;

    #region ��ų ����� ����

    // ���� ��ư �̸�
    public int buttonIdx;
    public int btnSkillIdx;
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

    // ����

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

    // ��ų ����, �ҷ�����

    #region SaveSkillData ��ų�� �����ͺ��̽��� ����
    public void SaveSkillData()
    {
        StartCoroutine(SaveSkill());
    }

    IEnumerator SaveSkill()
    {
        #region ������ ���̽��� ���� ����

            #region ������ ��ų �ε��� Ȯ��
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

        #region ������ ���̽��� �� ����
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                Debug.Log(skill + " : " + www.downloadHandler.text + "���� ����");
            }
            else
            {
                Debug.Log(www.error);
            }
        }
        #endregion
    }
    #endregion
/*
    #region LoadSkillData ��ų�� �����ͺ��̽� ���� �ҷ�����
    void LoadSkillData()
    {
        StartCoroutine(LoadSkill());
    }

    /// <summary>
    /// �����ͺ��̽� ���� ��ų ������ �ҷ��� ��ų ��ư�� �Ҵ�
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadSkill()
    {
        #region �����ͺ��̽� �� ���� ������
        // 0000000013
        string cuid = PlayerPrefs.GetString("characteruid");
        string skill = "skill_" + buttonIdx;
        string url = GameManager.gm.path + "loadskill.php";

        WWWForm form = new WWWForm();
        form.AddField("cuid", 0000000013);
        form.AddField("skill", skill);
        #endregion

        #region �����ͺ��̽� �� �� ����
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                int idx = int.Parse(www.downloadHandler.text);
                Debug.Log(idx);
            }
            else
                Debug.Log(www.error);
        }
        #endregion
    }
    #endregion*/

    public void BtnSkillSet()
    {
        // 0 �̻��̸� �ҷ�����
        if (btnSkillIdx > 0)
        {
            btnSkill = skillDirectory.skillAtks[btnSkillIdx];
            SkillIcon.sprite = Skill.sprite;
            SetSkillData();
        }
    }

    #region SetSkillData ��ų ��ư�� �Ҵ�� ��ų�� �ڽĿ�����Ʈ�� ��ų �����Ϳ� �Ҵ�
    /// <summary>
    /// ��ų ��ư�� �Ҵ�� ��ų�� �ڽĿ�����Ʈ�� ��ų �����Ϳ� �Ҵ�
    /// </summary>
    public void SetSkillData()
    {
        skillData.Skill = Skill;
        skillData.SkillIcon.sprite = SkillIcon.sprite;
    }
    #endregion

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region ���� ����
    void StartSetting()
    {
        skillIcon = GetComponent<Image>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCombat>();
        skillData = GetComponentInChildren<SkillData>();

        coolTimeIcon.fillAmount = 0;
        coolTime = 0f;

        chkDup = GetComponentInParent<SkillBtnManager>();

        // LoadSkillData();
    }
    #endregion
}