using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : IHaveQuest
{
    QuestController questCon;
    InteractProperty interPP;
    List<QuestData> myQuests;

    bool playerQuestDone;

    private void Start()
    {
        interPP = GetComponentInChildren<InteractProperty>();
        questCon = FindObjectOfType<QuestController>().GetComponent<QuestController>();
        GetMyQuest();
    }

    private void Update()
    {
        if (myQuests.Count == 0)
        {
            questHave.SetActive(false);
            questDone.SetActive(false);
            return;
        }

        if (playerQuestDone)
        {
            questHave.SetActive(false);
            questDone.SetActive(true);
        }
        else
        {
            questDone.SetActive(false);
            questHave.SetActive(true);
        }
    }

    void GetMyQuest()
    {
        if (!questDic.ContainsKey(interPP.InteractId))
            return;

        myQuests = questDic[interPP.InteractId];
    }

    public void PlayerDoneMyQuest()
    {
        bool check = false;

        for (int i = 0; i < myQuests.Count; i++)
        {
            if (myQuests[i].questId == questCon.doneQuestId[i])
                check = true;
        }

        playerQuestDone = check ? true : false;
    }

    public void PlayerFinMyQuest()
    {
        bool check = true;

        while (check)
        {
            for (int i = 0; i < myQuests.Count; i++)
            {
                if (myQuests[i].questId == questCon.finQuestId[i])
                {
                    myQuests.RemoveAt(i);
                    break;
                }
                else if (i + 1 == myQuests.Count)
                    check = false;
            }
        }
    }
}
