using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : IHaveQuest
{
    QuestController questCon;
    InteractProperty interPP;

    bool playerQuestDone;

    private void Start()
    {
        interPP = GetComponentInChildren<InteractProperty>();
        questCon = FindObjectOfType<QuestController>().GetComponent<QuestController>();
    }

    private void Update()
    {
        if (questDatas.Count == 0)
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
        
    }

    public void PlayerDoneMyQuest()
    {
        bool check = false;

        for (int i = 0; i < questDatas.Count; i++)
        {
            if (questDatas[i].questId == questCon.doneQuestId[i])
                check = true;
        }

        playerQuestDone = check ? true : false;
    }

    public void PlayerFinMyQuest()
    {
        bool check = true;

        while (check)
        {
            for (int i = 0; i < questDatas.Count; i++)
            {
                if (questDatas[i].questId == questCon.finQuestId[i])
                {
                    questDatas.RemoveAt(i);
                    break;
                }
                else if (i + 1 == questDatas.Count)
                    check = false;
            }
        }
    }
}
