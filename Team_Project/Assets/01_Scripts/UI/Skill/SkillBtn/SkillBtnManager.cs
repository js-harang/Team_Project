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

    private void Start()
    {
        sb = new StringBuilder();
        LoadSkillData();
        // SaveSkillData();
    }
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
        }
        return isDuplication;
    }
    void SaveSkillData()
    {
        // ��Ʈ������ �� ��ģ�� �� �����ϱ�
        for (int i = 0; i < btns.Length; i++)
        {
            sb.Append(string.Format("{0:00}", btns[i].btnSkillIdx));
        }
        Debug.Log(sb);

        // ������ ���̽��� �� �����ϱ�

    }
    void LoadSkillData()
    {
        // ������ ���̽� ���� �� �ҷ�����
        StartCoroutine(LoadSkill());
    }

    /// <summary>
    /// �����ͺ��̽� ���� ��ų ���� �ҷ�����
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadSkill()
    {
        #region �����ͺ��̽� �� ���� ������
        // 0000000018
        string cuid = PlayerPrefs.GetString("characteruid");
        string url = GameManager.gm.path + "loadskill.php";

        WWWForm form = new WWWForm();
        form.AddField("cuid", 0000000018);
        #endregion

        #region �����ͺ��̽� �� �� ����
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                skillSet = www.downloadHandler.text;
                Debug.Log(skillSet);

                skillBtnData = new string[skillSet.Length / 2];
                Debug.Log(skillBtnData.Length);

                int idx = 0;
                for (int i = 0; i < skillSet.Length - 1; i += 2)
                {
                    skillBtnData[idx] = skillSet.Substring(i, 2);
                    btns[idx].btnSkillIdx = System.Convert.ToInt32(skillBtnData[idx]);

                    Debug.Log(btns[idx].btnSkillIdx);
                    btns[idx].BtnSkillSet();

                    idx++;
                }
            }
            else
                Debug.Log(www.error);
        }
        #endregion
    }
}