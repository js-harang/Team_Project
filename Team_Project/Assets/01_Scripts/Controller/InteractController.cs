using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// npc와의 대화 단계 열거형 예)인사, 선택지 고르기 등
public enum InteractStep
{
    Meeting,
    Action,
    End,
}

public class InteractController : MonoBehaviour
{
    // 현재 대화의 진행 단계를 나타내는 열거형 변수
    InteractStep interactStep;

    // 속성으로 대화의 단계에 따라 동작
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

    // 현재 대화 중인지를 나타내는 속성 변수
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

    // 플레이어로부터 전달받을 현재 대화중인 상대 오브젝트의 정보
    public InteractProperty interPP;
    public QuestGiver nowGiver;

    public QuestController questCon;

    PlayerState pS;

    // UI 캔버스 변수
    [SerializeField] GameObject gameUI;

    // 대화창 캔버스 변수
    [SerializeField] GameObject dialogWindow;

    // 대화창 텍스트 변수들
    [SerializeField] TMP_Text interactName_Text;
    [SerializeField] TMP_Text dialog_Text;

    // 텍스트를 불러올 텍스트파일의 위치 변수
    string filePath;

    // 불러온 텍스트 문장들을 모아두는 리스트 변수
    List<string> sentences = new List<string>();

    // 대화 출력 코루틴이 실행 중인지를 확인하는 변수
    bool activatePrinting;
    bool stopTalking;

    // 코루틴 중지용 PrintSentencesLetter 코르틴을 담을 변수
    IEnumerator sentencePrintLetter;

    // NPC 타입별 대화 선택창 변수
    [SerializeField, Space(10)] GameObject shopDialogChoiceUI;
    [SerializeField] GameObject equipShopDialogChoiceUI;
    [SerializeField] GameObject gateKeeperDialogChoiceUI;

    // 메뉴 활성화 시 NPC들의 타입에 따른 UI 변수
    [SerializeField, Space(10)] GameObject shopUI;
    [SerializeField] GameObject equipShopUI;
    [SerializeField] GameObject selectStageUI;

    // 퀘스트 관련 UI 변수들
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

    // 현재 눌린 NPC 메뉴 버튼을 담는 변수
    GameObject npcMenu_Btn;

    // 상점 UI 활성화 시 같이 필요한 플레이어의 인벤토리
    public GameObject playerInventory;

    private void Start()
    {
        interactStep = InteractStep.End;
        // PlayerState 컴포넌트를 변수에 저장
        pS = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        questCon = FindObjectOfType<QuestController>().GetComponent<QuestController>();
    }

    private void Update()
    {
        if (!NowInteracting)
            return;

        DialogControll();
    }

    // 대화 중 키 입력에 따른 조작
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
    /// 대화 시작 시 실행할 동작들을 실행한다.
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

    // 상호작용 끝낼 시의 동작 관리
    void EndInteracting()
    {
        if (activatePrinting)                   //만약 텍스트 출력 코루틴이 실행중이었다면 중단
            StopCoroutine(sentencePrintLetter);

        stopTalking = false;
        activatePrinting = false;

        if (npcMenu_Btn != null)                 // NPC 메뉴 버튼이 눌린적 있을 떄만 실행
            NPCMenuButtonActiveOrFalse(npcMenu_Btn);

        dialogWindow.SetActive(false);
        gameUI.SetActive(true);
        NPCTypeMenuOnOff();
        ChoiceMenuOnOff();
        CloseQuestWindow();
        interPP.ImTalking = false;
        pS.UnitState = UnitState.Idle;
    }

    // 대화 종료 버튼을 따로 눌렀을 시의 동작
    public void EndInteractingBtn()
    {
        InteractStep = InteractStep.End;
    }

    /// <summary>
    /// 텍스트 파일을 읽어서 현재 대화 단계의 문장들을 sentences 리스트에 저장
    /// </summary>
    /// <param name="id">대화중인 NPC의 ID</param>
    /// <param name="num">NPC와의 대화 단계</param>
    void ReadLineAndStore(int id, InteractStep interactStep)
    {
        filePath = $"C:\\Users\\YONSAI\\Desktop\\Team_Project\\Team_Project\\Assets\\21_Data\\{id} Dialogue\\{interactStep}.txt";

        // 파일 존재 여부 확인
        if (!File.Exists(filePath))
        {
            Debug.LogError("파일을 찾을 수 없습니다. 파일경로 : " + filePath);
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            sentences.Add(lines[i]);
        }
    }

    /// <summary>
    /// 플레이어의 조작 및 필요한 상황에 따라 대화를 다음 단계로 진행
    /// </summary>
    private void PrintSentences()
    {
        if (activatePrinting)                           // 문장이 출력중이면 중단한다.
            StopCoroutine(sentencePrintLetter);

        dialog_Text.text = string.Empty;
        sentences.Clear();
        ReadLineAndStore(interPP.InteractId, interactStep);
        sentencePrintLetter = PrintSentencesLetter();
        StartCoroutine(sentencePrintLetter);
    }

    /// <summary>
    /// sentences 리스트의 문장마다 한글자씩 텀을주어 출력하는 코루틴
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

                if (stopTalking)                        // 출력중 Z 키를 누르면 스킵하고 전체 출력
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

    // PrintSentencesLetter 코루틴이 실행중이라면 stopTalking 를 true로 바꿔 출력 스킵
    public void PrintSentencesSkip()
    {
        if (activatePrinting)
            stopTalking = true;
    }

    // NPC 타입에 따른 선택지 UI 활성화, 비활성화
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

    // 각 NPC 역할에 따른 메뉴 선택 시의 동작
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

    // 퀘스트 버튼을 눌러서 퀘스트 창이 오픈될때
    public void QuestWindowOpen()
    {
        questWindowUI.SetActive(true);

        CreateMyQuestList();
    }

    // 퀘스트 리스트 하나를 만들 때, 그 리스트에 각각 퀘스트 정보와 필요한 UI 정보를 넘김
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

    // 퀘스트 창을 닫는 버튼을 눌렀을 시의 동작
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
        if (button.activeSelf)              // NPC 메뉴 버튼이 눌렸다면 버튼을 비활성화하고 변수에 저장
        {
            button.SetActive(false);
            npcMenu_Btn = button;
            return;
        }

        button.SetActive(true);             // 위의 이유로 비활성화 상태라면 활성화 하고 변수 초기화
        npcMenu_Btn = null;
    }

    public void GoBattle(int sceneNumber)
    {
        GameManager.gm.MoveScene(sceneNumber);
    }
}
