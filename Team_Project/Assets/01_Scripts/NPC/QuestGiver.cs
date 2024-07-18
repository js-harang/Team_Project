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
            PlayerDoneMyQuest();
            PlayerFinMyQuest();
        }
    }

    bool playerQuestDone;

    private void Start()
    {
        interPP = GetComponentInChildren<InteractProperty>();
        questCon = FindObjectOfType<QuestController>().GetComponent<QuestController>();
        GetMyQuest();
    }

    private void Update()
    {
        if (questList.Count == 0)
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

    // IHaveQuest ���� �ڽſ��� �Ҵ�� ����Ʈ ����Ʈ�� �޾ƿ�
    void GetMyQuest()
    {
        if (!questDic.ContainsKey(interPP.InteractId))
            return;

        QuestList = questDic[interPP.InteractId];
    }

    // �÷��̾ �ڽ��� ����Ʈ Ŭ���� ������ �����ߴ��� Ȯ����
    public void PlayerDoneMyQuest()
    {
        if (questList.Count == 0 || questCon.doneQuestID.Length == 0)
            return;

        bool check = false;

        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].questID == questCon.doneQuestID[i])
                check = true;
        }

        playerQuestDone = check ? true : false;
    }

    // �÷��̾ �ڽ��� ����Ʈ�� ������ Ŭ���� �ߴ��� Ȯ��
    public void PlayerFinMyQuest()
    {
        if (questList.Count == 0 || questCon.finQuestID.Length == 0)
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
