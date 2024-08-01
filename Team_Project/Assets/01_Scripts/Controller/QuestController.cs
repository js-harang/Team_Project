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
        WWWForm form = new WWWForm();
        form.AddField("cuid", GameManager.gm.UnitUid);
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

    // ����Ʈ�� Ÿ���ʿ��� �ڽ��� Ÿ�� ���̵� �Ű��� �Ͽ� ����Ʈ ��Ͽ� �ڽ��� ��ǥ�� �ϴ�
    // ����Ʈ�� �ִ��� ã�� �ִٸ� ���൵�� �ϳ� �ø� ������ �����ͺ��̽��� ������Ʈ �Ѵ�.
    public void QuestCurrentUpdate(int targetID)
    {
        for (int i = 0; i < myQuests.Count; i++)
        {
            if (myQuests[i].isDone)
                return;

            if (myQuests[i].targetID == targetID)
            {
                myQuests[i].CurrentAmount += 1;
                StartCoroutine(UpdateQuestCurrent(myQuests[i].questID, myQuests[i].CurrentAmount));

                if (myQuests[i].isDone)
                    StartCoroutine(UpdateQuestDone(myQuests[i].questID));
            }
        }
    }

    // ����Ʈ�� ���� ���൵�� �����ͺ��̽��� �����ϴ� �ڷ�ƾ
    IEnumerator UpdateQuestCurrent(int questID, int currentAmount)
    {
        string url = GameManager.gm.path + "update_playerquestcurrent.php";
        WWWForm form = new WWWForm();
        form.AddField("cuid", GameManager.gm.UnitUid);
        form.AddField("questid", questID);
        form.AddField("current", currentAmount);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    // ����Ʈ�� �Ϸ� ������ �޼��Ͽ��� �� �����ͺ��̽��� ���ε��ϴ� �ڷ�ƾ
    IEnumerator UpdateQuestDone(int questID)
    {
        string url = GameManager.gm.path + "update_playerquestdone.php";
        WWWForm form = new WWWForm();
        form.AddField("cuid", GameManager.gm.UnitUid);
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
