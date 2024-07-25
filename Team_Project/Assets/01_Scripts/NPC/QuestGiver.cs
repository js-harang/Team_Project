using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    QuestController questCon;
    InteractProperty interPP;
    QuestsLoad qLoad;
    public List<QuestData> questList = new List<QuestData>();

    public GameObject questHave;
    public GameObject questDone;

    int myQuestCount;
    public int MyQuestCount
    {
        get { return myQuestCount; }
        set { myQuestCount = value; PlayerDoneMyQuest(); }
    }

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

    private void Awake()
    {
        interPP = GetComponentInChildren<InteractProperty>();
        questCon = FindObjectOfType<QuestController>().GetComponent<QuestController>();
        qLoad = questCon.qLoad;
        qLoad.questDic.Add(interPP.InteractId, questList);
    }

    private void Update()
    {
        if (myQuestCount == questList.Count)
            return;

        MyQuestCount = questList.Count;
    }

    // 플레이어가 자신의 퀘스트를 얼마나 수주했는지 확인
    public void PlayerAcceptsMyQuests()
    {
        int count = 0;

        for (int i = 0; i < myQuestCount; i++)
        {
            for (int j = 0; j < questCon.MyQuests.Count; j++)
            {
                if (questList[i].questID == questCon.MyQuests[j].questID)
                {
                    count++;
                    break;
                }
            }
        }
        if (count == myQuestCount)
            questHave.SetActive(false);
        else
            questHave.SetActive(true);
    }

    // 플레이어가 자신의 퀘스트 클리어 조건을 만족했는지 확인함
    void PlayerDoneMyQuest()
    {
        int num = myQuestCount > questCon.doneQuestIDs.Count ?
                    questCon.doneQuestIDs.Count : myQuestCount;

        if (num == 0)
        {
            PlayerQuestDone = false;
            return;
        }

        bool check = false;

        for (int i = 0; i < myQuestCount; i++)
        {
            if (questList[i].questID == questCon.doneQuestIDs[i])
                check = true;
        }

        PlayerQuestDone = check ? true : false;
    }
}
