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
    private void Awake()
    {
        List<QuestData> questDatas = new List<QuestData>();
        giverID = 2000;

        questName = "ù �⵿";
        questDatas.Add(new QuestData(questName, 0, 2000, 1, 5000, 300));

        questName = "���� ����";
        questDatas.Add(new QuestData(questName, 1, 2000, "Monster", 0, 5, 30000, 600));

        questDic.Add(giverID, questDatas);

        List<QuestData> questDatas1 = new List<QuestData>();
        giverID = 1000;

        questName = "�ڰ� ����";
        questDatas1.Add(new QuestData(questName, 2, 1000, "Monster", 1000, 1, 50000, 1000));

        questDic.Add(giverID, questDatas1);
    }

    IEnumerator LoadPlayerClearedQuests(int characterID)
    {
        string url = GameManager.gm.path + "load_playerclearedquestdata.php";
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
        form.AddField("character_uid", characterID);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {

            }
        }
    }
}
