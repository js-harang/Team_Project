using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections;
using UnityEngine.Networking;

public class ShowQuest : MonoBehaviour
{
    public QuestData myData;
    public InteractController interCon;
    QuestController questCon;
    [SerializeField]
    TMP_Text showQuestName;
    [SerializeField]
    TMP_Text showComplete_Txt;
    // 자신이 대화 타입 퀘스트인지 확인하는 변수
    bool imConversation;
    // 플레이어의 현재 대화 목표가 자신인지 확인하는 변수
    bool targetIsMe;

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
        showComplete_Txt.text = "";
        if (myData.questType == QuestType.Conversation)
            imConversation = true;
        if (myData.targetID == (int)interCon.interPP.myID)
            targetIsMe = true;

        if (!imConversation && myData.isDone)
        {
            showComplete_Txt.text = "완료";
            return;
        }

        if (targetIsMe && myData.isDone)
            showComplete_Txt.text = "완료";
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
        AcceptBtnProcess(PlayerQuestsCheck());
        QuestAccept questAccept = questAccept_Btn.GetComponent<QuestAccept>();
        questAccept.showQ = this;
    }

    // 퀘스트의 상세 텍스트를 텍스트 파일에서 읽어옴
    void PrintQuestDetail(int questID)
    {
        string filePath = $"Texts/Quest Detail/{questID}";

        TextAsset text = Resources.Load<TextAsset>(filePath);

        string[] questDetail = text.text.Split('\n');

        for (int i = 0; i < questDetail.Length; i++)
        {
            questDetail_Txt.text += questDetail[i] + "\n";
        }
    }

    // 퀘스트 정보창에서 수락 버튼을 눌렀을 시
    public void AcceptBtnOnClicked()
    {
        if (!imConversation && myData.isDone)
        {
            QuestComplete();
            return;
        }
        else if (targetIsMe && myData.isDone)
        {
            QuestComplete();
            return;
        }

        StartCoroutine(UpdatePlayerAcceptQuest());
        questCon.AddMyQuestWindow(myData);
        isAccept_Txt.text = "진행 중";
        questAccept_Btn.enabled = false;
    }

    // 퀘스트 수락시 데이터베이스에 정보 업로드
    IEnumerator UpdatePlayerAcceptQuest()
    {
        string url = GameManager.gm.path + "update_playerquestaccept.php";
        WWWForm form = new WWWForm();

        string isdone = myData.isDone ? "Y" : "N";

        form.AddField("cuid", GameManager.gm.UnitUid);
        form.AddField("questid", myData.questID);
        form.AddField("current", myData.CurrentAmount);
        form.AddField("isdone", isdone);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    // 플레이어의 퀘스트 수주나 진행 여부에 따라 수락 버튼의 텍스트 변경 및 활성화/ 비활성화
    void AcceptBtnProcess(bool playerHave)
    {
        if (!imConversation && playerHave && myData.isDone)
        {
            isAccept_Txt.text = "달성";
            questAccept_Btn.enabled = true;
        }
        else if (targetIsMe && playerHave && myData.isDone)
        {
            isAccept_Txt.text = "달성";
            questAccept_Btn.enabled = true;
        }
        else if (playerHave)
        {
            isAccept_Txt.text = "진행 중";
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
        foreach (QuestData item in questCon.myQuests)
        {
            if (item.questID == myData.questID)
                return true;
        }
        return false;
    }

    // 플레이어가 이 오브젝트가 담고 있는 퀘스트를 완료했을 떄의 동작
    void QuestComplete()
    {
        GameManager.gm.Credit += myData.goldReward;
        GameManager.gm.Exp += myData.expReward;
        questCon.FinQuestCheck(myData);
        interCon.nowGiver.PlayerFinMyQuest(myData.questID);
        questInfoUI.SetActive(false);
        Destroy(gameObject);
    }
}
