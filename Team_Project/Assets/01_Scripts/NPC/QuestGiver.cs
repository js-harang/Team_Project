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

    // IHaveQuest 에서 자신에게 할당될 퀘스트 리스트를 받아옴
    void GetMyQuest()
    {
        if (!questDic.ContainsKey(interPP.InteractId))
            return;

        myQuests = questDic[interPP.InteractId];
    }

    // 플레이어가 자신의 퀘스트 클리어 조건을 만족했는지 확인함
    public void PlayerDoneMyQuest()
    {
        bool check = false;

        for (int i = 0; i < myQuests.Count; i++)
        {
            if (myQuests[i].questID == questCon.doneQuestID[i])
                check = true;
        }

        playerQuestDone = check ? true : false;
    }

    // 플레이어가 자신의 퀘스트를 완전히 클리어 했는지 확인
    public void PlayerFinMyQuest()
    {
        bool check = true;

        while (check)
        {
            for (int i = 0; i < myQuests.Count; i++)
            {
                if (myQuests[i].questID == questCon.finQuestID[i])
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
