using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SkillBtnManager : MonoBehaviour
{
    public SkillButton[] btns;
    string skillSet;
    [SerializeField] string[] skillBtnData;
    StringBuilder sb;
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Start()
    {
        sb = new StringBuilder();
        LoadSkillData();
    }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// ��� ��ư�� ����� ��ų��ȣ �� �ִ��� ��ȸ
    /// </summary>
    /// <param name="btnIdx"></param>
    /// <param name="idx"></param>
    /// <returns></returns>
    public bool CheckSkillDuplication(int idx)
    {
        bool isDuplication = false;

        for (int i = 0; i < btns.Length; i++)
        {
            if (btns[i].Skill != null)
            {
                // ��ų�� ��ȣ�� ���� ��ư�� ��ų��ȣ�� ������
                if (btns[i].Skill.skillIdx == idx)
                {
                    isDuplication = true;
                    return isDuplication;
                }
            }
        }
        return isDuplication;
    }
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region ��ų ����
    public void SaveSkillData()
    {
        // ��Ʈ������ �� ��ģ�� �� �����ϱ�
        for (int i = 0; i < btns.Length; i++)
            sb.Append(string.Format("{0:00}", btns[i].btnSkillIdx));

        GameManager.gm.Skill = sb.ToString();

        sb.Clear();
    }

    #endregion

    #region ��ų �ҷ�����
    void LoadSkillData()
    {
        skillBtnData = new string[skillSet.Length / 2];

        int idx = 0;
        for (int i = 0; i < GameManager.gm.Skill.Length; i += 2)
        {
            skillBtnData[idx] = GameManager.gm.Skill.Substring(i, 2);
            btns[idx].btnSkillIdx = System.Convert.ToInt32(skillBtnData[idx]);
            btns[idx].BtnSkillSet();

            idx++;
        }

        for (int i = 0; i < skillBtnData.Length; i++)
        {
            btns[i].btnSkillIdx = System.Convert.ToInt32(skillBtnData[i]);
            btns[i].BtnSkillSet();
        }
    }
    #endregion
}