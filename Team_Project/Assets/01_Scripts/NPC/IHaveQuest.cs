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

    // ������ ����Ʈ�� �� NPC ���� �Ҵ���� ����Ʈ ������ ����Ʈ�� ���, 
    // ��ųʸ��� ������ ����Ʈ�� NPC ���̵� Ű�� ������.
    private void Awake()
    {
        List<QuestData> questDatas = new List<QuestData>();
        giverID = 2000;

        questName = "ù �⵿";
        questDatas.Add(new QuestData(questName, 0, 1000));

        questName = "���� ����";
        questDatas.Add(new QuestData(questName, 1, TargetType.Monster, 0, 5));

        questDic.Add(giverID, questDatas);

        List<QuestData> questDatas1 = new List<QuestData>();
        giverID = 1000;

        questName = "�ڰ� ����";
        questDatas1.Add(new QuestData(questName, 2, TargetType.Monster, 1000, 1));

        questDic.Add(giverID, questDatas1);
    }
}
