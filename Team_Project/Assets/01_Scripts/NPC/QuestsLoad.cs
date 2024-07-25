using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuestsLoad : MonoBehaviour
{
    // 각 NPC 의 아이디를 키로 퀘스트 리스트를 담을 딕셔너리
    public Dictionary<int, List<QuestData>> questDic = new Dictionary<int, List<QuestData>>();

    private void Start()
    {
        StartCoroutine(LoadQuestDatas());
    }

    // 딕셔너리에 각각의 리스트를 NPC 아이디를 키로 저장하고
    // 각각의 리스트에 각 NPC 별로 할당받을 퀘스트 정보를 리스트에 담음
    IEnumerator LoadQuestDatas()
    {
        string url = GameManager.gm.path + "load_questdata.php";
        string cuid = PlayerPrefs.GetString("characteruid");
        WWWForm form = new WWWForm();
        form.AddField("cuid", 0000000004);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                string jsonStr = "{\"Items\":" + www.downloadHandler.text + "}";
                QuestJsons quests = JsonUtility.FromJson<QuestJsons>(jsonStr);

                foreach (QuestJson quest in quests.Items)
                {
                    questDic[quest.giver_id].Add(new QuestData(quest));
                }
            }
        }
    }
}
