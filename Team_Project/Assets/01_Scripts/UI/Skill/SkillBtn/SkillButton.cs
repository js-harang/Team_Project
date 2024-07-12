using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // AttackSO skill;
    Attack skill;

    // public AttackSO Skill { get { return skill; } set { skill = value; } }
    public Attack Skill { get { return skill; } set { skill = value; } }
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
        if (skillData.Skill == null)
        {
            Debug.Log("�����");
            return;
        }

        skill = skillData.Skill;

        // ��ų ��Ÿ�� �� 0 ���ϰų� �޺� �� �̸� ����
        if (coolTime <= 0 || skill.isComboing)
        {
            if (skill.useMana > player.CurrentMp && skill.isComboing == false)
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
        player.attack = skill;
        player.Attack();
    }
    #endregion

    #region ��ų ��Ÿ�� �۵�
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
        Debug.Log("SkillButton" + buttonIdx + " �ε��� ��ȣ : " + idx);
        // 0 �̻��̸� �ҷ�����
        if (idx > 0)
        {
            skill = skillDirectory.skillAtks[idx];
            skillData.Skill = skill;
            skillData.SkillIcon.sprite = skillDirectory.skillSprites[idx];
        }

        if (skill == null)
        {
            Debug.Log(this.name + "��ų�� �����");
            return;
        }

        if (chkDup.CheckSkillDuplication(buttonIdx, idx))
        {
            Debug.Log("�ߺ� �߰� " + this.name);
        }
        // 0 �̸� �⺻������ null ����
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

    #region ���� ����
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