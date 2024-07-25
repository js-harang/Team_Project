using UnityEngine;
using TMPro;

public class QuestGoalList : MonoBehaviour
{
    public QuestData myData;
    EnemyID enemyID;
    NPCID npcID;
    ItemID itemID;

    [SerializeField]
    TMP_Text questName_Txt;
    [SerializeField]
    TMP_Text questGoal_Txt;
    [SerializeField]
    TMP_Text goalCount_Txt;

    private void Update()
    {
        if (myData == null)
            return;

        UpdateQuestList();
    }

    public void CreateQuestList()
    {
        questName_Txt.text = myData.questName;
    }

    void UpdateQuestList()
    {
        switch (myData.questType)
        {
            case QuestType.Kill:
                enemyID = (EnemyID)myData.targetID;
                questGoal_Txt.text = $"{enemyID} Ã³Ä¡";
                goalCount_Txt.text = $"{myData.CurrentAmount} / {myData.requiredAmount}";
                break;
            case QuestType.Gathering:
                itemID = (ItemID)myData.targetID;
                questGoal_Txt.text = $"{itemID} È¹µæ";
                goalCount_Txt.text = $"{myData.CurrentAmount} / {myData.requiredAmount}";
                break;
            case QuestType.Conversation:
                npcID = (NPCID)myData.targetID;
                questGoal_Txt.text = $"{npcID}°ú(¿Í) ´ëÈ­"; goalCount_Txt.text = "";
                break;
            default:
                break;
        }
    }
}
