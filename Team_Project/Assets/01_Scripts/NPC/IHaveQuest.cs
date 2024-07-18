using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHaveQuest : MonoBehaviour
{
    public List<QuestData> questDatas = new List<QuestData>();
    public Dictionary<int, List<QuestData>> questDic = new Dictionary<int, List<QuestData>>();
    string questName;
    int giverId;

    public GameObject questHave;
    public GameObject questDone;

    private void Awake()
    {
        giverId = 2000;

        questName = "ù �⵿";
        questDatas.Add(new QuestData(questName, giverId, 0, 1000));

        questName = "���� ����";
        questDatas.Add(new QuestData(questName, giverId, 1, TargetType.Monster, 0, 5));

        SaveAndClear(giverId, questDatas);

        giverId = 1000;

        questName = "�ڰ� ����";
        questDatas.Add(new QuestData(questName, giverId, 2, TargetType.Monster, 1000, 1));

        SaveAndClear(giverId, questDatas);
    }

    void SaveAndClear(int giverId, List<QuestData> questDatas)
    {
        questDic.Add(giverId, questDatas);
        this.questDatas.Clear();
    }
}
