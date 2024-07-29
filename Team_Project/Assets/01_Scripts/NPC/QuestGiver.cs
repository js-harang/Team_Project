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

    public int myQuestCount;

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

        myQuestCount = questList.Count;
    }

    // �÷��̾ �ڽ��� ����Ʈ�� �󸶳� �����ߴ��� Ȯ��
    public void PlayerAcceptsMyQuests()
    {
        int count = 0;

        for (int i = 0; i < myQuestCount; i++)
        {
            for (int j = 0; j < questCon.myQuests.Count; j++)
            {
                if (questList[i].questID == questCon.myQuests[j].questID)
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

    // �÷��̾ ���� �ɾ��� �� ���� ����Ʈ ��ǥ�� �ڽ����� Ȯ���ϴ� �޼���
    public void HereForTalkToMe()
    {
        if (questCon.myQuests.Count == 0)
            return;

        for (int i = 0; i < questCon.myQuests.Count; i++)
        {
            if (questCon.myQuests[i].targetID != (int)interPP.myID)
                return;

            questCon.myQuests[i].isDone = true;
            if (!AlreadyHaveCheck(questCon.myQuests[i]))
                questList.Add(questCon.myQuests[i]);
        }
    }

    // �÷��̾ ���� ���� ����Ʈ�� �� ����Ʈ ����� ����Ʈ�� ��ġ�� �ش� ����Ʈ�� ��������� ������
    public void GetPlayerDoingQuest()
    {
        if (questCon.myQuests.Count == 0)
            return;

        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < questCon.myQuests.Count; j++)
            {
                if (questList[i].questID == questCon.myQuests[j].questID)
                    questList[i] = questCon.myQuests[j];
            }
        }
    }

    // ����Ʈ �ߺ� �˻�
    bool AlreadyHaveCheck(QuestData quest)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].questID == quest.questID)
            {
                return true;
            }
        }
        return false;
    }
}
