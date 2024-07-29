using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuestsLoad : MonoBehaviour
{
    // �� NPC �� ���̵� Ű�� ����Ʈ ����Ʈ�� ���� ��ųʸ�
    public Dictionary<int, List<QuestData>> questDic = new Dictionary<int, List<QuestData>>();
    [SerializeField]QuestController questCon;

    private void Start()
    {
        StartCoroutine(LoadQuestDatas());
        StartCoroutine(LoadPlayerQuestsHave());
    }

    // ��ųʸ��� ������ ����Ʈ�� NPC ���̵� Ű�� �����ϰ�
    // ������ ����Ʈ�� �� NPC ���� �Ҵ���� ����Ʈ ������ ����Ʈ�� ����
    IEnumerator LoadQuestDatas()
    {
        string url = GameManager.gm.path + "load_NPCquestsdata.php";
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

    // �����ͺ��̽��� ����� �ڽ��� �����ϰ� ���� ���� ����Ʈ �޾ƿ�
    IEnumerator LoadPlayerQuestsHave()
    {
        string url = GameManager.gm.path + "load_playerquestshave.php";
        string cuid = PlayerPrefs.GetString("characteruid");
        WWWForm form = new WWWForm();
        form.AddField("cuid", 0000000004);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                string jsonStr = "{\"Items\":" + www.downloadHandler.text + "}";
                if (jsonStr == "")
                    yield break;

                QuestsChecks qChecks = JsonUtility.FromJson<QuestsChecks>(jsonStr);
                StartCoroutine(PlayerQuestDataSet(qChecks));
            }
        }
    }

    // ���� ���� ���� ����Ʈ�� ID�� �׿� �ش��ϴ� ����Ʈ������ �޾� QuestController�� �ѱ�
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
                    QuestJson quest = JsonUtility.FromJson<QuestJson>(jsonStr);
                    QuestData questData = new QuestData(quest);
                    questData.CurrentAmount = qCheck.current;
                    questCon.myQuests.Add(questData);
                }
            }
        }
    }
}
