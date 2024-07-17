using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHaveQuest : MonoBehaviour
{
    QuestData questData; 
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
        questData = new QuestData(questName, giverId, 0, 1000);
        questDatas.Add(questData);

        questName = "���� ����";
        questData = new QuestData(questName, giverId, 1, TargetType.Monster, 0, 5);
        questDatas.Add(questData);

        questDic.Add(giverId ,questDatas);
        questDatas.Clear();

        giverId = 1000;

        questName = "�ڰ� ����";
        questData = new QuestData(questName, giverId, 2, TargetType.Monster, 1000, 1);
        questDatas.Add(questData);

        questDic.Add(giverId, questDatas);
    }
}
