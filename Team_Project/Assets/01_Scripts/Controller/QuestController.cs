using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuestController : MonoBehaviour
{
    // �������� ����Ʈ���� �ϰ� �ҷ������� Ŭ������ ����
    public QuestsLoad qLoad;

    // ���� ���� ���� �ִ� ����Ʈ��
    public List<QuestData> myQuests = new List<QuestData>();

    // ���� �÷��̾ ���� �ִ� ����Ʈ���� ����Ʈ UI
    [SerializeField]
    GameObject myQuestListPref;
    [SerializeField]
    Transform questUIContent;

    // ����Ʈâ UI Ȱ��ȭ�ϸ鼭 ����Ʈ ������ ���� ��ϵ��� ����
    public void MyQuestWindowUpdate(QuestData newQuest)
    {
        myQuests.Add(newQuest);
        Transform questList = Instantiate(myQuestListPref).transform;
        QuestGoalList questGoal = questList.gameObject.GetComponent<QuestGoalList>();
        questList.transform.SetParent(questUIContent);
        questGoal.myData = myQuests[myQuests.Count - 1];
        questGoal.CreateQuestList();
    }

    // �÷��̾ ���� ���� ����Ʈ�� �ϳ� �Ϸ� ������ �׿� ���� �����͸� �����ϴ� ����
    public void FinQuestCheck(QuestData qData)
    {
        StartCoroutine(FinQuestUpdate(qData.questID));
        for (int i = 0; i < qLoad.questDic[qData.giverID].Count; i++)
        {
            if (qData.questID == qLoad.questDic[qData.giverID][i].questID)
            {
                qLoad.questDic[qData.giverID].RemoveAt(i);
                break;
            }
        }
    }

    // �÷��̾ ����Ʈ�� �Ϸ��� ���� ������ ���� 
    IEnumerator FinQuestUpdate(int questID)
    {
        string url = GameManager.gm.path + "update_playerquestclear.php";
        string cuid = PlayerPrefs.GetString("characteruid");
        WWWForm form = new WWWForm();
        form.AddField("cuid", 0000000004);
        form.AddField("questid", questID);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
