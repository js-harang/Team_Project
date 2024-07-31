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
    /// 모든 버튼에 등록할 스킬번호 가 있는지 조회
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
                // 스킬의 번호와 현재 버튼의 스킬번호와 같으면
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

    #region 스킬 저장
    public void SaveSkillData()
    {
        // 스트링빌더 로 합친뒤 값 전달하기
        for (int i = 0; i < btns.Length; i++)
            sb.Append(string.Format("{0:00}", btns[i].btnSkillIdx));

        GameManager.gm.Skill = sb.ToString();

        sb.Clear();
    }

    #endregion

    #region 스킬 불러오기
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