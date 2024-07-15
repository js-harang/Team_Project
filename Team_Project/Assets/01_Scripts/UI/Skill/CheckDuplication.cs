using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckDuplication : MonoBehaviour
{
    public SkillButton[] btns;
    /// <summary>
    /// 모든 버튼에 등록할 스킬번호 가 있는지 조회
    /// </summary>
    /// <param name="btnIdx"></param>
    /// <param name="idx"></param>
    /// <returns></returns>
    public bool CheckSkillDuplication(int idx)
    {
        Debug.Log("검사중");
        bool isDuplication = false;

        for (int i = 0; i < btns.Length; i++)
        {
            if (btns[i].Skill != null)
            {
                // 스킬의 번호와 현재 버튼의 스킬번호와 같으면
                if (btns[i].Skill.skillIdx == idx)
                {
                    Debug.Log("중복 발견");
                    isDuplication = true;
                    return isDuplication;
                }
            }
            else
                Debug.Log("중복 없음");

        }
        return isDuplication;
    }
}