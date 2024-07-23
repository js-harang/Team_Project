using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    // ���� ���� ���� �ִ� ����Ʈ��
    List<QuestData> myQuests = new List<QuestData>();
    public List<QuestData> MyQuests { get { return myQuests; } }

    // ���� �÷��̾ ���� �ִ� ����Ʈ���� ����Ʈ UI
    [SerializeField]
    GameObject MyQuestListPref;
    [SerializeField]
    Transform questUIContent;

    // ������ ������ ����Ʈ��
    public int[] doneQuestID;
    // ������ ���� ����Ʈ��
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
