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

    #region 스킬 저장
    public void SaveSkillData()
    {
        // 스트링빌더 로 합친뒤 값 전달하기
        for (int i = 0; i < btns.Length; i++)
            sb.Append(string.Format("{0:00}", btns[i].btnSkillIdx));

        Debug.Log(sb.ToString());

        // 데이터 베이스에 값 전달하기
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
                Debug.Log(www.downloadHandler.text + "로 저장 완료");
            else
                Debug.Log(www.error);
        }
    }
    #endregion

    #region 스킬 불러오기
    void LoadSkillData()
    {
        // 데이터 베이스 에서 값 불러오기
        StartCoroutine(LoadSkill());
    }

    /// <summary>
    /// 데이터베이스 에서 스킬 정보 불러오기
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadSkill()
    {
        #region 데이터베이스 에 보낼 데이터
        // 0000000018
        string cuid = PlayerPrefs.GetString("characteruid");
        string url = GameManager.gm.path + "loadskill.php";

        WWWForm form = new WWWForm();
        form.AddField("cuid", GameManager.gm.useCuid);
        #endregion

        #region 데이터베이스 에 값 전달
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