using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    PlayerCombat player;

    #region ��ų ������ ����
    [SerializeField]
    SkillData skillData;

    [SerializeField]
    AttackSO skill;
    public AttackSO Skill { get { return skill; } set { skill = value; } }
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
        if (player.pbs.UnitBS != UnitBattleState.Idle)
        {
            return;
        }
        // ��ų�� �������� ����
        if (skill == null)
        {
            Debug.Log("�����");
            return;
        }

        if (coolTime > 0)
            return;

        skill = skillData.Skill;


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
        player.attack = skill;
        player.Attack();
    }
    #endregion

    #region ��ų ��Ÿ�� �۵�
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

    #region ���� ����
    void StartSetting()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCombat>();
        coolTimeIcon.fillAmount = 0;
        coolTime = 0f;
    }
    #endregion
}