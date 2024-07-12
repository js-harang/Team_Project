using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckDuplication : MonoBehaviour
{
    public SkillButton[] btns;

    public bool CheckSkillDuplication(int btnIdx,int idx)
    {
        bool isDuplication = false;

        for (int i = 0; i < btns.Length; i++)
        {
            if (btnIdx != i)
            {
                if (btns[i].Skill != null)
                {
                    // 등록된 버튼의 스킬의 번호와 현재 버튼의 스킬번호와 같으면
                    if (btns[i].Skill.skillIdx == idx)
                    {
                        isDuplication = true;
                        return isDuplication;
                    }
                }
            }
        }
        return isDuplication;
    }
}
