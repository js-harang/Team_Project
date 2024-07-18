using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHaveQuest : MonoBehaviour
{
    public Dictionary<int, List<QuestData>> questDic = new Dictionary<int, List<QuestData>>();
    string questName;
    int giverId;

    public GameObject questHave;
    public GameObject questDone;

    private void Awake()
    {
        List<QuestData> questDatas = new List<QuestData>();
        giverId = 2000;

        questName = "첫 출동";
        questDatas.Add(new QuestData(questName, 0, 1000));

        questName = "소재 수집";
        questDatas.Add(new QuestData(questName, 1, TargetType.Monster, 0, 5));

        questDic.Add(giverId, questDatas);

        List<QuestData> questDatas1 = new List<QuestData>();
        giverId = 1000;

        questName = "자격 증명";
        questDatas1.Add(new QuestData(questName, 2, TargetType.Monster, 1000, 1));

        questDic.Add(giverId, questDatas1);
    }
}
