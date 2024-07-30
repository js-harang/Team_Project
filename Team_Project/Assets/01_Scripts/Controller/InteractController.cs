using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// npc���� ��ȭ �ܰ� ������ ��)�λ�, ������ ���� ��
public enum InteractStep
{
    Meeting,
    Action,
    End,
}

public class InteractController : MonoBehaviour
{
    // ���� ��ȭ�� ���� �ܰ踦 ��Ÿ���� ������ ����
    InteractStep interactStep;

    // �Ӽ����� ��ȭ�� �ܰ迡 ���� ����
    public InteractStep InteractStep
    {
        get { return interactStep; }
        set
        {
            interactStep = value;
            switch (interactStep)
            {
                case InteractStep.Meeting:
                    PrintSentences();
                    ChoiceMenuOnOff();
                    break;
                case InteractStep.Action:
                    PrintSentences();
                    break;
                case InteractStep.End:
                    ChoiceMenuOff();
                    PrintSentences();
                    break;
                default:
                    break;
            }
        }
    }

    // ���� ��ȭ �������� ��Ÿ���� �Ӽ� ����
    bool nowInteracting;
    public bool NowInteracting
    {
        get { return nowInteracting; }
        set
        {
            nowInteracting = value;
            if (nowInteracting)
                StartInteracting();
            else
                EndInteracting();
        }
    }

    // �÷��̾�κ��� ���޹��� ���� ��ȭ���� ��� ������Ʈ�� ����
    public InteractProperty interPP;
    public QuestGiver nowGiver;

    public QuestController questCon;

    PlayerState pS;

    // UI ĵ���� ����
    [SerializeField] GameObject gameUI;

    // ��ȭâ ĵ���� ����
    [SerializeField] GameObject dialogWindow;

    // ��ȭâ �ؽ�Ʈ ������
    [SerializeField] TMP_Text interactName_Text;
    [SerializeField] TMP_Text dialog_Text;

    // �ؽ�Ʈ�� �ҷ��� �ؽ�Ʈ������ ��ġ ����
    string filePath;

    // �ҷ��� �ؽ�Ʈ ������� ��Ƶδ� ����Ʈ ����
    List<string> sentences = new List<string>();

    // ��ȭ ��� �ڷ�ƾ�� ���� �������� Ȯ���ϴ� ����
    bool activatePrinting;
    bool stopTalking;

    // �ڷ�ƾ ������ PrintSentencesLetter �ڸ�ƾ�� ���� ����
    IEnumerator sentencePrintLetter;

    // NPC Ÿ�Ժ� ��ȭ ����â ����
    [SerializeField, Space(10)] GameObject shopDialogChoiceUI;
    [SerializeField] GameObject equipShopDialogChoiceUI;
    [SerializeField] GameObject gateKeeperDialogChoiceUI;

    // �޴� Ȱ��ȭ �� NPC���� Ÿ�Կ� ���� UI ����
    [SerializeField, Space(10)] GameObject shopUI;
    [SerializeField] GameObject equipShopUI;
    [SerializeField] GameObject selectStageUI;

    // ����Ʈ ���� UI ������
    [SerializeField, Space(10)] GameObject questListPref;
    [SerializeField] GameObject questWindowUI;
    [SerializeField] Transform questScrollContent;
    public GameObject questInfoUI;
    public TMP_Text questName_Txt;
    public TMP_Text questDetail_Txt;
    public TMP_Text goldReward_Txt;
    public TMP_Text expReward_Txt;
    public Button questAccept_Btn;
    public TMP_Text isAcept_Txt;

    // ���� ���� NPC �޴� ��ư�� ��� ����
    GameObject npcMenu_Btn;

    // ���� UI Ȱ��ȭ �� ���� �ʿ��� �÷��̾��� �κ��丮
    public GameObject playerInventory;

    private void Start()
    {
        interactStep = InteractStep.End;
        // PlayerState ������Ʈ�� ������ ����
        pS = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        questCon = FindObjectOfType<QuestController>().GetComponent<QuestController>();
    }

    private void Update()
    {
        if (!NowInteracting)
            return;

        DialogControll();
    }

    // ��ȭ �� Ű �Է¿� ���� ����
    void DialogControll()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (InteractStep != InteractStep.End)
                InteractStep = InteractStep.End;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            PrintSentencesSkip();
        }
    }

    /// <summary>
    /// ��ȭ ���� �� ������ ���۵��� �����Ѵ�.
    /// </summary>
    private void StartInteracting()
    {
        interPP.ImTalking = true;
        nowGiver = interPP.giverIsMe;
        gameUI.SetActive(false);
        dialogWindow.SetActive(true);
        interactName_Text.text = interPP.InteractName;
        InteractStep = InteractStep.Meeting;
    }

    // ��ȣ�ۿ� ���� ���� ���� ����
    void EndInteracting()
    {
        if (activatePrinting)                   //���� �ؽ�Ʈ ��� �ڷ�ƾ�� �������̾��ٸ� �ߴ�
            StopCoroutine(sentencePrintLetter);

        stopTalking = false;
        activatePrinting = false;

        if (npcMenu_Btn != null)                 // NPC �޴� ��ư�� ������ ���� ���� ����
            NPCMenuButtonActiveOrFalse(npcMenu_Btn);

        dialogWindow.SetActive(false);
        gameUI.SetActive(true);
        NPCTypeMenuOnOff();
        ChoiceMenuOnOff();
        CloseQuestWindow();
        interPP.ImTalking = false;
        pS.UnitState = UnitState.Idle;
    }

    // ��ȭ ���� ��ư�� ���� ������ ���� ����
    public void EndInteractingBtn()
    {
        InteractStep = InteractStep.End;
    }

    /// <summary>
    /// �ؽ�Ʈ ������ �о ���� ��ȭ �ܰ��� ������� sentences ����Ʈ�� ����
    /// </summary>
    /// <param name="id">��ȭ���� NPC�� ID</param>
    /// <param name="num">NPC���� ��ȭ �ܰ�</param>
    void ReadLineAndStore(int id, InteractStep interactStep)
    {
        filePath = $"C:\\Users\\YONSAI\\Desktop\\Team_Project\\Team_Project\\Assets\\21_Data\\{id} Dialogue\\{interactStep}.txt";

        // ���� ���� ���� Ȯ��
        if (!File.Exists(filePath))
        {
            Debug.LogError("������ ã�� �� �����ϴ�. ���ϰ�� : " + filePath);
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            sentences.Add(lines[i]);
        }
    }

    /// <summary>
    /// �÷��̾��� ���� �� �ʿ��� ��Ȳ�� ���� ��ȭ�� ���� �ܰ�� ����
    /// </summary>
    private void PrintSentences()
    {
        if (activatePrinting)                           // ������ ������̸� �ߴ��Ѵ�.
            StopCoroutine(sentencePrintLetter);

        dialog_Text.text = string.Empty;
        sentences.Clear();
        ReadLineAndStore(interPP.InteractId, interactStep);
        sentencePrintLetter = PrintSentencesLetter();
        StartCoroutine(sentencePrintLetter);
    }

    /// <summary>
    /// sentences ����Ʈ�� ���帶�� �ѱ��ھ� �����־� ����ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    /// 
    IEnumerator PrintSentencesLetter()
    {
        activatePrinting = true;

        for (int i = 0; i < sentences.Count; i++)
        {
            foreach (char letter in sentences[i])
            {
                dialog_Text.text += letter;

                if (stopTalking)                        // ����� Z Ű�� ������ ��ŵ�ϰ� ��ü ���
                    continue;

                yield return new WaitForSeconds(0.05f);
            }
            if (i == sentences.Count - 1)
                break;

            dialog_Text.text += "\n";
        }

        activatePrinting = false;
        stopTalking = false;

        if (interactStep == InteractStep.End)
        {
            yield return new WaitForSeconds(1f);
            NowInteracting = false;
        }
    }

    // PrintSentencesLetter �ڷ�ƾ�� �������̶�� stopTalking �� true�� �ٲ� ��� ��ŵ
    public void PrintSentencesSkip()
    {
        if (activatePrinting)
            stopTalking = true;
    }

    // NPC Ÿ�Կ� ���� ������ UI Ȱ��ȭ, ��Ȱ��ȭ
    void ChoiceMenuOnOff()
    {
        switch (interPP.InteractType)
        {
            case InteractType.Shop:
                shopDialogChoiceUI.SetActive(NowInteracting);
                break;
            case InteractType.EquipmentShop:
                equipShopDialogChoiceUI.SetActive(NowInteracting);
                break;
            case InteractType.GateKeeper:
                gateKeeperDialogChoiceUI.SetActive(NowInteracting);
                break;
            default:
                break;
        }
    }

    void ChoiceMenuOff()
    {
        switch (interPP.InteractType)
        {
            case InteractType.Shop:
                shopDialogChoiceUI.SetActive(false);
                break;
            case InteractType.EquipmentShop:
                equipShopDialogChoiceUI.SetActive(false);
                break;
            case InteractType.GateKeeper:
                gateKeeperDialogChoiceUI.SetActive(false);
                break;
            default:
                break;
        }

    }

    // �� NPC ���ҿ� ���� �޴� ���� ���� ����
    public void NPCTypeMenuOnOff()
    {
        if (NowInteracting)
            InteractStep = InteractStep.Action;

        switch (interPP.InteractType)
        {
            case InteractType.Shop:
                shopUI.SetActive(NowInteracting);
                playerInventory.SetActive(NowInteracting);
                break;
            case InteractType.EquipmentShop:
                equipShopUI.SetActive(NowInteracting);
                playerInventory.SetActive(NowInteracting);
                break;
            case InteractType.GateKeeper:
                selectStageUI.SetActive(NowInteracting);
                break;
            default:
                break;
        }
    }

    // ����Ʈ ��ư�� ������ ����Ʈ â�� ���µɶ�
    public void QuestWindowOpen()
    {
        questWindowUI.SetActive(true);

        CreateMyQuestList();
    }

    // ����Ʈ ����Ʈ �ϳ��� ���� ��, �� ����Ʈ�� ���� ����Ʈ ������ �ʿ��� UI ������ �ѱ�
    void CreateMyQuestList()
    {
        if (nowGiver.MyQuestCount == 0)
            return;

        for (int i = 0; i < nowGiver.MyQuestCount; i++)
        {
            if (nowGiver.questList[i].questType == QuestType.Conversation
                && nowGiver.questList[i].IsDone && nowGiver.questList[i].giverID == interPP.InteractId)
                continue;

            Transform questList = Instantiate(questListPref).transform;
            ShowQuest showQuest = questList.gameObject.GetComponent<ShowQuest>();
            questList.transform.SetParent(questScrollContent);
            showQuest.myData = nowGiver.questList[i];
            showQuest.interCon = this;
            showQuest.CreateList();
        }
    }

    // ����Ʈ â�� �ݴ� ��ư�� ������ ���� ����
    public void CloseQuestWindow()
    {
        foreach (Transform child in questScrollContent)
        {
            Destroy(child.gameObject);
        }
        questWindowUI.SetActive(false);
        questInfoUI.SetActive(false);
    }

    public void NPCMenuButtonActiveOrFalse(GameObject button)
    {
        if (button.activeSelf)              // NPC �޴� ��ư�� ���ȴٸ� ��ư�� ��Ȱ��ȭ�ϰ� ������ ����
        {
            button.SetActive(false);
            npcMenu_Btn = button;
            return;
        }

        button.SetActive(true);             // ���� ������ ��Ȱ��ȭ ���¶�� Ȱ��ȭ �ϰ� ���� �ʱ�ȭ
        npcMenu_Btn = null;
    }

    public void GoBattle(int sceneNumber)
    {
        GameManager.gm.MoveScene(sceneNumber);
    }
}
