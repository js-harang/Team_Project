using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckDuplication : MonoBehaviour
{
    public SkillButton[] btns;
    /// <summary>
    /// ��� ��ư�� ����� ��ų��ȣ �� �ִ��� ��ȸ
    /// </summary>
    /// <param name="btnIdx"></param>
    /// <param name="idx"></param>
    /// <returns></returns>
    public bool CheckSkillDuplication(int idx)
    {
        Debug.Log("�˻���");
        bool isDuplication = false;

        for (int i = 0; i < btns.Length; i++)
        {
            if (btns[i].Skill != null)
            {
                // ��ų�� ��ȣ�� ���� ��ư�� ��ų��ȣ�� ������
                if (btns[i].Skill.skillIdx == idx)
                {
                    Debug.Log("�ߺ� �߰�");
                    isDuplication = true;
                    return isDuplication;
                }
            }
            else
                Debug.Log("�ߺ� ����");

        }
        return isDuplication;
    }
}