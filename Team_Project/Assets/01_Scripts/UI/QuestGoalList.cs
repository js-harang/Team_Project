using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestGoalList : MonoBehaviour
{
    public QuestData myData;

    [SerializeField]
    TMP_Text questName_Txt;
    [SerializeField]
    TMP_Text questGoal_Txt;
    [SerializeField]
    TMP_Text goalCount_Txt;

    private void Start()
    {
        CreateQuestList();
    }

    private void Update()
    {
        UpdateQuestList();
    }

    void CreateQuestList()
    {
        questName_Txt.text = myData.questName;
    }

    void UpdateQuestList()
    {
        switch (myData.questType)
        {
            case QuestType.Kill:
                break;
            case QuestType.Gathering:
                break;
            case QuestType.Conversation:
                break;
            default:
                break;
        }
    }
}
