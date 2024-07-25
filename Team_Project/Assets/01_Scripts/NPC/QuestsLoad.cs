using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuestsLoad : MonoBehaviour
{
    public Dictionary<int, List<QuestData>> questDic = new Dictionary<int, List<QuestData>>();

    string questName;
    int giverID;

    // ������ ����Ʈ�� �� NPC ���� �Ҵ���� ����Ʈ ������ ����Ʈ�� ���, 
    // ��ųʸ��� ������ ����Ʈ�� NPC ���̵� Ű�� ������.
    private void Start()
    {
        StartCoroutine(LoadQuestDatas());
    }

    IEnumerator LoadPlayerQuestsHave()
    {
        string url = GameManager.gm.path + "load_playerquestdata.php";
        string cuid = PlayerPrefs.GetString("characteruid");
        WWWForm form = new WWWForm();
        form.AddField("cuid", cuid);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {

            }
        }
    }

    IEnumerator LoadQuestDatas()
    {
        string url = GameManager.gm.path + "load_questdata.php";
        string cuid = PlayerPrefs.GetString("characteruid");
        WWWForm form = new WWWForm();
        form.AddField("cuid", 0000000018);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                string jsonStr = "{\"Items\":" + www.downloadHandler.text + "}";
                QuestJsons quests = JsonUtility.FromJson<QuestJsons>(jsonStr);
                Debug.Log(www.downloadHandler.text);

                foreach (QuestJson quest in quests.Items)
                {
                    Debug.Log(quest.giver_id);
                    questDic[quest.giver_id].Add(new QuestData(quest));
                }
            }
        }
    }
}
