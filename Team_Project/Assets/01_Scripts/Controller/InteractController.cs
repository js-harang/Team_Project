using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

// npc와의 대화 단계 열거형 예)인사, 선택지 고르기 등
public enum InteractStep
{
    Meeting,
    ChoiceMenu,
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
                    ChoiceMenuOpen();
                    break;
                case InteractStep.ChoiceMenu:
                    break;
                case InteractStep.Action:
                    PrintSentences();
                    break;
                case InteractStep.End:
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
    InteractType interactType;
    public InteractType InteractType { get { return interactType; } set { interactType = value; } }

    int interactId;
    public int InteractId { get { return interactId; } set { interactId = value; } }

    string interactName;
    public string InteractName { get { return interactName; } set { interactName = value; } }

    PlayerState pS;

    // UI 캔버스 변수
    public Canvas gameUI;

    // 대화창 캔버스 변수
    public Canvas dialogWindow;

    // 대화창 텍스트 변수들
    public TMP_Text interactName_Text;
    public TMP_Text dialog_Text;

    // 텍스트를 불러올 텍스트파일의 위치 변수
    string filePath;

    // 불러온 텍스트 문장들을 모아두는 리스트 변수
    List<string> sentences = new List<string>();

    // 대화 출력 코루틴이 실행 중인지를 확인하는 변수
    bool activatePrinting;
    bool stopTalking;

    // NPC 타입별 대화 선택창 변수
    public GameObject shopDialogChoiceUI;
    public GameObject equipShopDialogChoiceUI;
    public GameObject gateKeeperDialogChoiceUI;

    // 배틀 스테이지 선택창 변수
    public GameObject selectStageUI;

    private void Start()
    {
        interactStep = InteractStep.End;
        // PlayerState 컴포넌트를 변수에 저장
        pS = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (interactStep == InteractStep.End)
            return;

        DialogControll();
    }

    // 대화 중 키 입력에 따른 조작
    void DialogControll()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            NowInteracting = false;

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (activatePrinting)
                stopTalking = true;
        }
    }

    /// <summary>
    /// 대화 시작 시 실행할 동작들을 실행한다.
    /// </summary>
    private void StartInteracting()
    {
        gameUI.enabled = false;
        dialog_Text.text = string.Empty;
        dialogWindow.enabled = true;
        interactName_Text.text = interactName;
        InteractStep = InteractStep.Meeting;
    }

    // 상호작용 끝낼 시의 동작 관리
    public void EndInteracting()
    {
        ChoiceMenuClose();
        dialogWindow.enabled = false;
        pS.UnitState = UnitState.Idle;
    }

    /// <summary>
    /// 텍스트 파일을 읽어서 현재 대화 단계의 문장들을 sentences 리스트에 저장
    /// </summary>
    /// <param name="id">대화중인 NPC의 ID</param>
    /// <param name="num">NPC와의 대화 단계</param>
    private void ReadLineAndStore(int id, InteractStep interactStep)
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
        dialog_Text.text = string.Empty;
        sentences.Clear();
        ReadLineAndStore(InteractId, interactStep);
        StartCoroutine(PrintSentenceLetter());
    }
    /// <summary>
    /// sentences 리스트의 문장마다 한글자씩 텀을주어 출력하는 코루틴
    /// </summary>
    /// <returns></returns>
    /// 
    IEnumerator PrintSentenceLetter()
    {
        activatePrinting = true;

        for (int i = 0; i < sentences.Count; i++)
        {
            foreach (char letter in sentences[i])
            {
                dialog_Text.text += letter;

                if (stopTalking)                        // 출력중 X 키를 누르면 스킵하고 전체 출력
                    continue;

                yield return new WaitForSeconds(0.05f);
            }
            if (i == sentences.Count - 1)
                break;

            dialog_Text.text += "\n";
        }
        activatePrinting = false;
        stopTalking = false;
    }

    // NPC 타입에 따른 UI 활성화
    void ChoiceMenuOpen()
    {
        switch (interactType)
        {
            case InteractType.Shop:
                shopDialogChoiceUI.SetActive(true);
                break;
            case InteractType.EquipmentShop:
                equipShopDialogChoiceUI.SetActive(true);
                break;
            case InteractType.GateKeeper:
                gateKeeperDialogChoiceUI.SetActive(true);
                break;
            default:
                break;
        }
    }

    // NPC 타입에 따른 UI 비활성화
    public void ChoiceMenuClose()
    {
        switch (interactType)
        {
            case InteractType.Shop:
                shopDialogChoiceUI.SetActive(false);
                break;
            case InteractType.EquipmentShop:
                equipShopDialogChoiceUI.SetActive(false);
                break;
            case InteractType.GateKeeper:
                gateKeeperDialogChoiceUI.SetActive(false);
                selectStageUI.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void GoBattle(int sceneNumber)
    {
        GameManager.gm.MoveScene(sceneNumber);
    }
}
