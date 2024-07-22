using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ShowQuest : MonoBehaviour
{
    public QuestData myData;
    public QuestController questCon;
    [SerializeField]
    TMP_Text showQuestName;

    // 퀘스트 정보창의 UI 변수들
    public GameObject questInfoUI;
    public TMP_Text questName_Txt;
    public TMP_Text questDetail_Txt;
    public TMP_Text goldReward_Txt;
    public TMP_Text expReward_Txt;

    public void CreateList()
    {
        showQuestName.text = myData.questName;
    }

    public void OnClicked()
    {
        questInfoUI.SetActive(true);
        questName_Txt.text = myData.questName;
        PrintQuestDetail(myData.questID);
        goldReward_Txt.text = "" + myData.goldReward;
        expReward_Txt.text = "" + myData.expReward;
    }

    void PrintQuestDetail(int questID)
    {
        string filePath = $"C:\\Users\\YONSAI\\Desktop\\Team_Project\\Team_Project\\Assets\\21_Data\\Quest Detail\\{questID}.txt";

        // 파일 존재 여부 확인
        if (!File.Exists(filePath))
        {
            Debug.LogError("파일을 찾을 수 없습니다. 파일경로 : " + filePath);
            return;
        }

        string[] questDetail = File.ReadAllLines(filePath);

        for (int i = 0; i < questDetail.Length; i++)
        {
            questDetail_Txt.text = questDetail[i];
        }
    }

    // 퀘스트 정보창에서 수락 버튼을 눌렀을 시
    public void AcceptBtnOnClicked()
    {
        questCon.MyQuests.Add(myData);
    }
}
