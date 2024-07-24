using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IHaveQuest : MonoBehaviour
{
    public Dictionary<int, List<QuestData>> questDic = new Dictionary<int, List<QuestData>>();

    string questName;
    int giverID;

    public GameObject questHave;
    public GameObject questDone;

    // ������ ����Ʈ�� �� NPC ���� �Ҵ���� ����Ʈ ������ ����Ʈ�� ���, 
    // ��ųʸ��� ������ ����Ʈ�� NPC ���̵� Ű�� ������.
    private void Start()
    {
        StartCoroutine(LoadQuestDatas(PlayerPrefs.GetInt("characteruid")));
    }

    IEnumerator LoadPlayerQuestsHave(int characterID)
    {
        string url = GameManager.gm.path + "load_playerquestdata.php";
        WWWForm form = new WWWForm();
        form.AddField("cuid", characterID);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {

            }
        }
    }

    IEnumerator LoadQuestDatas(int characterID)
    {
        string url = GameManager.gm.path + "load_questdata.php";
        WWWForm form = new WWWForm();
        form.AddField("cuid", characterID);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                string jsonStr = "{\"Items\":" + www.downloadHandler.text + "}";
                QuestJsons quests = JsonUtility.FromJson<QuestJsons>(jsonStr);

                foreach (QuestJson quest in quests.Items)
                {
                    questDic[quest.giverID].Add(new QuestData(quest));
                }
            }
        }
    }
}
