using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckDuplication : MonoBehaviour
{
    public SkillButton[] btns;

    public bool CheckSkillDuplication(int idx)
    {
        bool isDuplication = false;

        for (int i = 0; i < btns.Length; i++)
        {
            // ��ϵ� ��ư�� ��ų�� ��ȣ�� ���� ��ư�� ��ų��ȣ�� ������
            if (btns[i].Skill.skillIdx == idx)
            {
                isDuplication = true;
                break;
            }
        }
        return isDuplication;
    }
}
