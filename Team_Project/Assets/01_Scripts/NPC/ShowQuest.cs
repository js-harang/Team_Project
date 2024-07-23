using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ShowQuest : MonoBehaviour
{
    public QuestData myData;
    public InteractController interCon;
    QuestController questCon;
    [SerializeField]
    TMP_Text showQuestName;

    // 퀘스트 정보창의 UI 변수들
    GameObject questInfoUI;
    TMP_Text questName_Txt;
    TMP_Text questDetail_Txt;
    TMP_Text goldReward_Txt;
    TMP_Text expReward_Txt;
    Button questAccept_Btn;
    public TMP_Text isAccept_Txt;

    public void CreateList()
    {
        questCon = interCon.questCon;
        questInfoUI = interCon.questInfoUI;
        questName_Txt = interCon.questName_Txt;
        questDetail_Txt = interCon.questDetail_Txt;
        goldReward_Txt = interCon.goldReward_Txt;
        expReward_Txt = interCon.expReward_Txt;
        questAccept_Btn = interCon.questAccept_Btn;
        isAccept_Txt = interCon.isAcept_Txt;
        showQuestName.text = myData.questName;
    }

    // 클릭되었을 때의 동작
    public void OnClicked()
    {
        questInfoUI.SetActive(true);
        questName_Txt.text = myData.questName;
        questDetail_Txt.text = string.Empty;
        PrintQuestDetail(myData.questID);
        goldReward_Txt.text = "Gold : " + myData.goldReward;
        expReward_Txt.text = "Exp : " + myData.expReward;
        AcceptBtnOnOff(PlayerQuestsCheck());
        QuestAccept questAccept = questAccept_Btn.GetComponent<QuestAccept>();
        questAccept.showQ = this;
    }

    // 퀘스트의 상세 텍스트를 텍스트 파일에서 읽어옴
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
            questDetail_Txt.text += questDetail[i] + "\n";
        }
    }

    // 퀘스트 정보창에서 수락 버튼을 눌렀을 시
    public void AcceptBtnOnClicked()
    {
        questCon.MyQuestWindowUpdate(myData);
        interCon.nowGiver.PlayerAcceptsMyQuests();
        isAccept_Txt.text = "수락 중";
        questAccept_Btn.enabled = false;
    }

    // 플레이어의 퀘스트 수주 여부에 따라 수락 버튼 활성화/ 비활성화
    void AcceptBtnOnOff(bool onOff)
    {
        if (onOff)
        {
            isAccept_Txt.text = "수락 중";
            questAccept_Btn.enabled = false;
        }
        else
        {
            isAccept_Txt.text = "수락";
            questAccept_Btn.enabled = true;
        }
    }

    // Questcontroller 에서 플레이어가 갖고 있는 퀘스트 목록을 확인하여 중복체크
    bool PlayerQuestsCheck()
    {
        foreach (QuestData item in questCon.MyQuests)
        {
            if (item.questID == myData.questID)
                return true;
        }
        return false;
    }
}
