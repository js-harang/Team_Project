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
                    // ��ϵ� ��ư�� ��ų�� ��ȣ�� ���� ��ư�� ��ų��ȣ�� ������
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
