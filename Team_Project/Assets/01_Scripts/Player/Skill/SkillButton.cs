using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    PlayerBattleController player;

    #region ��ų ������ ����
    [SerializeField]
    SkillData skillData;

    [SerializeField]
    PlayerAttack skill;
    public PlayerAttack Skill { get { return skill; } set { skill = value; } }
    #endregion

    #region ��ų ��Ÿ�� ����
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
        // ��ų�� �������� ����
        if (skillData.Skill == null)
            return;

        skill = skillData.Skill;

        if (coolTime > 0)
        {
            Debug.Log("��Ÿ��");
            return;
        }

        AttackProcess();
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region ���� ���� �� ��Ÿ��
    void AttackProcess()
    {
        // ���� ����
        SkillSetting();
        // ��Ÿ�� ����
        StartCoroutine(SkillCoolTime());
    }
    #endregion

    #region ���� �� �߰��� ��ų�� ������ ����
    void SkillSetting()
    {
        player.pAttack = skill;
        player.ChangeAttack(skill.aST.atkType);
        player.AttackStart();
    }
    #endregion

    #region ��ų ��Ÿ�� �۵�
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

    #region ���� ����
    void StartSetting()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBattleController>();
        coolTimeIcon.fillAmount = 0;
        coolTime = 0f;
    }
    #endregion
}