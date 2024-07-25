using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuestController : MonoBehaviour
{
    // �������� ����Ʈ���� �ϰ� �ҷ������� Ŭ������ ����
    public QuestsLoad qLoad;

    // ���� ���� ���� �ִ� ����Ʈ��
    List<QuestData> myQuests = new List<QuestData>();
    public List<QuestData> MyQuests { get { return myQuests; } }

    // ������ ������ ����Ʈ��
    public List<int> doneQuestIDs = new List<int>();

    // ���� �÷��̾ ���� �ִ� ����Ʈ���� ����Ʈ UI
    [SerializeField]
    GameObject MyQuestListPref;
    [SerializeField]
    Transform questUIContent;

    // ����Ʈâ UI Ȱ��ȭ�ϸ鼭 ����Ʈ ������ ���� ��ϵ��� ����
    public void MyQuestWindowUpdate(QuestData newQuest)
    {
        myQuests.Add(newQuest);
        Transform questList = Instantiate(MyQuestListPref).transform;
        QuestGoalList questGoal = questList.gameObject.GetComponent<QuestGoalList>();
        questList.transform.SetParent(questUIContent);
        questGoal.myData = myQuests[myQuests.Count - 1];
        questGoal.CreateQuestList();
    }

    public void DoneQuestCheck(int index)
    {
        doneQuestIDs.Add(index);
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
        form.AddField("cuid", 0000000018);
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
