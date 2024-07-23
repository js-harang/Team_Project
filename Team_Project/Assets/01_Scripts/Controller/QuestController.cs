using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    // 현재 내가 갖고 있는 퀘스트들
    List<QuestData> myQuests = new List<QuestData>();
    public List<QuestData> MyQuests { get { return myQuests; } }

    // 현재 플레이어가 갖고 있는 퀘스트들의 리스트 UI
    [SerializeField]
    GameObject MyQuestListPref;
    [SerializeField]
    Transform questUIContent;

    // 조건을 충족한 퀘스트들
    public int[] doneQuestID;
    // 완전히 끝낸 퀘스트들
    public int[] finQuestID;

    public void MyQuestWindowUpdate(QuestData newQuest)
    {
        myQuests.Add(newQuest);
        Transform questList = Instantiate(MyQuestListPref).transform;
        QuestGoalList questGoal = questList.gameObject.GetComponent<QuestGoalList>();
        questList.transform.SetParent(questUIContent);
        questGoal.myData = myQuests[myQuests.Count - 1];
        questGoal.CreateQuestList();
    }
}
