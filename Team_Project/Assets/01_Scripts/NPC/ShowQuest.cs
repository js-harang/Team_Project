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

    // ����Ʈ ����â�� UI ������
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

    // Ŭ���Ǿ��� ���� ����
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
        questCon.MyQuestWindowUpdate(myData);
        interCon.nowGiver.PlayerAcceptsMyQuests();
        isAccept_Txt.text = "���� ��";
        questAccept_Btn.enabled = false;
    }

    // �÷��̾��� ����Ʈ ���� ���ο� ���� ���� ��ư Ȱ��ȭ/ ��Ȱ��ȭ
    void AcceptBtnOnOff(bool onOff)
    {
        if (onOff)
        {
            isAccept_Txt.text = "���� ��";
            questAccept_Btn.enabled = false;
        }
        else
        {
            isAccept_Txt.text = "����";
            questAccept_Btn.enabled = true;
        }
    }

    // Questcontroller ���� �÷��̾ ���� �ִ� ����Ʈ ����� Ȯ���Ͽ� �ߺ�üũ
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
