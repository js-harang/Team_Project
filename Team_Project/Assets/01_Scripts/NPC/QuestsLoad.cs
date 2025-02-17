using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuestsLoad : MonoBehaviour
{
    // 각 NPC 의 아이디를 키로 퀘스트 리스트를 담을 딕셔너리
    public Dictionary<int, List<QuestData>> questDic = new Dictionary<int, List<QuestData>>();
    [SerializeField]QuestController questCon;

    private void Start()
    {
        StartCoroutine(LoadQuestDatas());
        StartCoroutine(LoadPlayerQuestsHave());
    }

    // 딕셔너리에 각각의 리스트를 NPC 아이디를 키로 저장하고
    // 각각의 리스트에 각 NPC 별로 할당받을 퀘스트 정보를 리스트에 담음
    IEnumerator LoadQuestDatas()
    {
        if (questDic.Count == 0)
            yield break;

        string url = GameManager.gm.path + "load_NPCquestsdata.php";
        WWWForm form = new WWWForm();
        form.AddField("cuid", GameManager.gm.UnitUid);

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

    // 데이터베이스에 저장된 자신이 수주하고 진행 중인 퀘스트 받아옴
    IEnumerator LoadPlayerQuestsHave()
    {
        string url = GameManager.gm.path + "load_playerquestshave.php";
        WWWForm form = new WWWForm();
        form.AddField("cuid", GameManager.gm.UnitUid);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                if (www.downloadHandler.text == "")
                    yield break;

                string jsonStr = "{\"Items\":" + www.downloadHandler.text + "}";
                QuestsChecks qChecks = JsonUtility.FromJson<QuestsChecks>(jsonStr);
                StartCoroutine(PlayerQuestDataSet(qChecks));
            }
        }
    }

    // 위의 진행 중인 퀘스트의 ID로 그에 해당하는 퀘스트정보를 받아 QuestController에 넘김
    IEnumerator PlayerQuestDataSet(QuestsChecks qChecks)
    {
        string url = GameManager.gm.path + "load_playerquestsdata.php";
        WWWForm form = new WWWForm();

        foreach (QuestsCheck qCheck in qChecks.Items)
        {
            form.AddField("questID", qCheck.quest_id);

            using (UnityWebRequest www = UnityWebRequest.Post(url, form))
            {
                yield return www.SendWebRequest();

                if (www.error == null)
                {
                    string jsonStr = "{\"Items\":" + www.downloadHandler.text + "}";
                    QuestJsons quests = JsonUtility.FromJson<QuestJsons>(jsonStr);
                    QuestJson quest = quests.Items[0];
                    QuestData questData = new QuestData(quest);
                    questData.CurrentAmount = qCheck.current;
                    questCon.AddMyQuestWindow(questData);
                }
            }
        }
    }
}
