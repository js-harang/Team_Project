using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    // ���� ���� ���� �ִ� ����Ʈ��
    List<QuestData> myQuests = new List<QuestData>();
    public List<QuestData> MyQuests
    {
        get { return myQuests; }
        set
        {
            myQuests = value;
            MyQuestWindowUpdate();
        }
    }

    // ���� �÷��̾ ���� �ִ� ����Ʈ���� ����Ʈ UI
    [SerializeField]
    GameObject MyQuestListPref;
    [SerializeField]
    Transform questUIContent;

    // ������ ������ ����Ʈ��
    public int[] doneQuestID;
    // ������ ���� ����Ʈ��
    public int[] finQuestID;

    void MyQuestWindowUpdate()
    {
        for (int i = 0; i < myQuests.Count; i++)
        {
            Transform questList = Instantiate(MyQuestListPref).transform;
            QuestGoalList questGoal = questList.gameObject.GetComponent<QuestGoalList>();
            questList.transform.SetParent(questUIContent);
        }
    }
}
