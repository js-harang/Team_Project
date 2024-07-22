using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : IHaveQuest
{
    QuestController questCon;
    InteractProperty interPP;
    List<QuestData> questList;
    public List<QuestData> QuestList
    {
        get { return questList; }
        set
        {
            questList = value;
            myQuestCount = questList.Count;
            PlayerDoneMyQuest();
            PlayerFinMyQuest();
        }
    }

    int myQuestCount;

    bool playerQuestDone;
    public bool PlayerQuestDone
    {
        get { return playerQuestDone; }
        set
        {
            playerQuestDone = value;
            if (myQuestCount == 0)
                return;
            if (value)
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
    }

    private void Start()
    {
        interPP = GetComponentInChildren<InteractProperty>();
        questCon = FindObjectOfType<QuestController>().GetComponent<QuestController>();
        GetMyQuest();
    }

    // IHaveQuest 에서 자신에게 할당될 퀘스트 리스트를 받아옴
    void GetMyQuest()
    {
        if (!questDic.ContainsKey(interPP.InteractId))
            return;

        QuestList = questDic[interPP.InteractId];
    }

    // 플레이어가 자신의 퀘스트 클리어 조건을 만족했는지 확인함
    public void PlayerDoneMyQuest()
    {
        int num = myQuestCount > questCon.doneQuestID.Length ?
                    questCon.doneQuestID.Length : myQuestCount;

        if (num == 0)
        {
            PlayerQuestDone = false;
            return;
        }

        bool check = false;

        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].questID == questCon.doneQuestID[i])
                check = true;
        }

        PlayerQuestDone = check ? true : false;
    }

    // 플레이어가 자신의 퀘스트를 완전히 클리어 했는지 확인
    public void PlayerFinMyQuest()
    {
        int num = myQuestCount > questCon.doneQuestID.Length ?
                    questCon.doneQuestID.Length : myQuestCount;

        if (num == 0)
            return;

        bool check = true;

        while (check)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].questID == questCon.finQuestID[i])
                {
                    questList.RemoveAt(i);
                    break;
                }
                else if (i + 1 == questList.Count)
                    check = false;
            }
        }
    }
}
