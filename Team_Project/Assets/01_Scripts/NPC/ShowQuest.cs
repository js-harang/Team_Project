using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ShowQuest : MonoBehaviour
{
    public QuestData myData;
    public QuestController questCon;
    public InteractController interCon;
    [SerializeField]
    TMP_Text showQuestName;

    // ����Ʈ ����â�� UI ������
    GameObject questInfoUI;
    TMP_Text questName_Txt;
    TMP_Text questDetail_Txt;
    TMP_Text goldReward_Txt;
    TMP_Text expReward_Txt;
    Button questAccept_Btn;

    public void CreateList()
    {
        questInfoUI = interCon.questInfoUI;
        questName_Txt = interCon.questName_Txt;
        questDetail_Txt = interCon.questDetail_Txt;
        goldReward_Txt = interCon.goldReward_Txt;
        expReward_Txt = interCon.expReward_Txt;
        questAccept_Btn = interCon.questAccept_Btn;
        showQuestName.text = myData.questName;
    }

    // Ŭ���Ǿ��� ���� ����
    public void OnClicked()
    {
        questInfoUI.SetActive(true);
        questName_Txt.text = myData.questName;
        questDetail_Txt.text = string.Empty;
        PrintQuestDetail(myData.questID);
        goldReward_Txt.text = "Gold : " + myData.goldReward;
        expReward_Txt.text = "Exp : " + myData.expReward;
        QuestAccept questAccept = questAccept_Btn.GetComponent<QuestAccept>();
        questAccept.showQ = this;
    }

    // ����Ʈ�� �� �ؽ�Ʈ�� �ؽ�Ʈ ���Ͽ��� �о��
    void PrintQuestDetail(int questID)
    {
        string filePath = $"C:\\Users\\YONSAI\\Desktop\\Team_Project\\Team_Project\\Assets\\21_Data\\Quest Detail\\{questID}.txt";

        // ���� ���� ���� Ȯ��
        if (!File.Exists(filePath))
        {
            Debug.LogError("������ ã�� �� �����ϴ�. ���ϰ�� : " + filePath);
            return;
        }

        string[] questDetail = File.ReadAllLines(filePath);

        for (int i = 0; i < questDetail.Length; i++)
        {
            questDetail_Txt.text += questDetail[i] + "\n";
        }
    }

    // ����Ʈ ����â���� ���� ��ư�� ������ ��
    public void AcceptBtnOnClicked()
    {
        questCon.MyQuests.Add(myData);
    }
}
