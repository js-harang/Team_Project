using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuestsLoad : MonoBehaviour
{
    // �� NPC �� ���̵� Ű�� ����Ʈ ����Ʈ�� ���� ��ųʸ�
    public Dictionary<int, List<QuestData>> questDic = new Dictionary<int, List<QuestData>>();

    private void Start()
    {
        StartCoroutine(LoadQuestDatas());
    }

    // ��ųʸ��� ������ ����Ʈ�� NPC ���̵� Ű�� �����ϰ�
    // ������ ����Ʈ�� �� NPC ���� �Ҵ���� ����Ʈ ������ ����Ʈ�� ����
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
