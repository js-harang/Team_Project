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
    }
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

    #region ��ų ����
    public void SaveSkillData()
    {
        // ��Ʈ������ �� ��ģ�� �� �����ϱ�
        for (int i = 0; i < btns.Length; i++)
            sb.Append(string.Format("{0:00}", btns[i].btnSkillIdx));

        Debug.Log(sb.ToString());

        // ������ ���̽��� �� �����ϱ�
        StartCoroutine(SaveSkill());
        
        sb.Clear();
    }

    IEnumerator SaveSkill()
    {
        string cuid = PlayerPrefs.GetString("characteruid");
        string url = GameManager.gm.path + "saveskill.php";
        string num = sb.ToString();
        Debug.Log(num);
        
        WWWForm form = new WWWForm();
        form.AddField("cuid", GameManager.gm.useCuid);
        form.AddField("num", num);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
                Debug.Log(www.downloadHandler.text + "�� ���� �Ϸ�");
            else
                Debug.Log(www.error);
        }
    }
    #endregion

    #region ��ų �ҷ�����
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
        form.AddField("cuid", GameManager.gm.useCuid);
        #endregion

        #region �����ͺ��̽� �� �� ����
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                skillSet = www.downloadHandler.text;

                skillBtnData = new string[skillSet.Length / 2];

                int idx = 0;
                for (int i = 0; i < skillSet.Length - 1; i += 2)
                {
                    skillBtnData[idx] = skillSet.Substring(i, 2);
                    btns[idx].btnSkillIdx = System.Convert.ToInt32(skillBtnData[idx]);
                    btns[idx].BtnSkillSet();

                    idx++;
                }
            }
            else
                Debug.Log(www.error);
        }
        #endregion
    }
    #endregion
}