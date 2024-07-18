using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHaveQuest : MonoBehaviour
{
    public Dictionary<int, List<QuestData>> questDic = new Dictionary<int, List<QuestData>>();
    string questName;
    int giverID;

    public GameObject questHave;
    public GameObject questDone;

    // 각각의 리스트에 각 NPC 별로 할당받을 퀘스트 정보를 리스트에 담고, 
    // 딕셔너리에 각각의 리스트를 NPC 아이디를 키로 저장함.
    private void Awake()
    {
        List<QuestData> questDatas = new List<QuestData>();
        giverID = 2000;

        questName = "첫 출동";
        questDatas.Add(new QuestData(questName, 0, 1000));

        questName = "소재 수집";
        questDatas.Add(new QuestData(questName, 1, TargetType.Monster, 0, 5));

        questDic.Add(giverID, questDatas);

        List<QuestData> questDatas1 = new List<QuestData>();
        giverID = 1000;

        questName = "자격 증명";
        questDatas1.Add(new QuestData(questName, 2, TargetType.Monster, 1000, 1));

        questDic.Add(giverID, questDatas1);
    }
}
