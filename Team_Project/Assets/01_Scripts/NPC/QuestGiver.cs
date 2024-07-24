using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : IHaveQuest
{
    QuestController questCon;
    InteractProperty interPP;
    List<QuestData> questList = new List<QuestData>();

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

    private void Awake()
    {
        interPP = GetComponentInChildren<InteractProperty>();
        questCon = FindObjectOfType<QuestController>().GetComponent<QuestController>();
        questDic.Add(interPP.InteractId, QuestList);
    }

    // �÷��̾ �ڽ��� ����Ʈ�� �󸶳� �����ߴ��� Ȯ��
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

    // �÷��̾ �ڽ��� ����Ʈ Ŭ���� ������ �����ߴ��� Ȯ����
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

        for (int i = 0; i < myQuestCount; i++)
        {
            if (questList[i].questID == questCon.doneQuestID[i])
                check = true;
        }

        PlayerQuestDone = check ? true : false;
    }

    // �÷��̾ �ڽ��� ����Ʈ�� ������ Ŭ���� �ߴ��� Ȯ��
    public void PlayerFinMyQuest()
    {
        int num = myQuestCount > questCon.doneQuestID.Length ?
                    questCon.doneQuestID.Length : myQuestCount;

        if (num == 0)
            return;

        bool check = true;

        while (check)
        {
            for (int i = 0; i < myQuestCount; i++)
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
