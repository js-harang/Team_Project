using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuestController : MonoBehaviour
{
    // 서버에서 퀘스트들을 일괄 불러오기한 클래스의 변수
    public QuestsLoad qLoad;

    // 현재 내가 갖고 있는 퀘스트들
    public List<QuestData> myQuests = new List<QuestData>();

    // 현재 플레이어가 갖고 있는 퀘스트들의 리스트 UI
    [SerializeField]
    GameObject myQuestListPref;
    [SerializeField]
    Transform questUIContent;

    // 퀘스트창 UI 활성화하면서 퀘스트 정보를 담은 목록들을 생성
    public void MyQuestWindowUpdate(QuestData newQuest)
    {
        myQuests.Add(newQuest);
        Transform questList = Instantiate(myQuestListPref).transform;
        QuestGoalList questGoal = questList.gameObject.GetComponent<QuestGoalList>();
        questList.transform.SetParent(questUIContent);
        questGoal.myData = myQuests[myQuests.Count - 1];
        questGoal.CreateQuestList();
    }

    // 플레이어가 게임 내의 퀘스트를 하나 완료 했을때 그에 관한 데이터를 정리하는 동작
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

    // 플레이어가 퀘스트를 완료한 것을 서버에 저장 
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

    // 퀘스트의 타겟쪽에서 자신의 타겟 아이디를 매개로 하여 퀘스트 목록에 자신을 목표로 하는
    // 퀘스트가 있는지 찾고 있다면 진행도를 하나 올린 다음에 데이터베이스에 업데이트 한다.
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

    // 퀘스트의 현재 진행도를 데이터베이스에 저장하는 코루틴
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

    // 퀘스트가 완료 조건을 달성하였을 때 데이터베이스에 업로드하는 코루틴
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
