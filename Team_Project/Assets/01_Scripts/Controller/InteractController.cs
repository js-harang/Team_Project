using TMPro;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;

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

    // 상호작용이 시작되었는지 변수로 확인하면서 속성을 이용해 메서드 호출
    bool nowInteracting;
    public bool NowInteracting
    {
        get
        {
            return nowInteracting;
        }
        set
        {
            nowInteracting = value;
            if (nowInteracting)
                StartInteracting();
            else
                EndInteracting();
        }
    }

    PlayerState pS;

    // UI 캔버스 변수
    public Canvas gameUI;

    // 대화창 캔버스 변수
    public Canvas dialogWindow;

    // 배틀 스테이지 선택창 변수
    public GameObject selectStageUI;

    // 대화창 텍스트 변수들
    public TMP_Text interactName_Text;
    public TMP_Text dialog_Text;

    // 텍스트를 불러올 텍스트파일의 위치 변수
    string filePath;

    // 현재 리스트에서 불러낼 문장의 인데스
    int sentencesIndex;

    // 불러온 텍스트 문장들을 모아두는 리스트 변수
    List<string> sentences = new List<string>();

    // 플레이어로부터 전달받을 현재 대화중인 상대 오브젝트의 정보
    InteractType interactType;
    public InteractType InteractType { get { return interactType; } set { interactType = value; } }

    int interactId;
    public int InteractId { get { return interactId; } set { interactId = value; } }

    string interactName;
    public string InteractName { get { return interactName; } set { interactName = value; } }

    private void Start()
    {
        // PlayerState 컴포넌트를 변수에 저장
        pS = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (!nowInteracting)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            NowInteracting = false;
    }

    // 상호작용 시작 시의 동작
    void StartInteracting()
    {
        TalkStartProcess();

        ReadLineAndStore(InteractId, interactStep);

        Continue();

        switch (interactType)
        {
            case InteractType.Shop:
                break;
            case InteractType.EquipmentShop:
                break;
            case InteractType.GateKeeper:
                selectStageUI.SetActive(true);
                break;
            default:
                break;
        }
    }

    // 상호작용 끝낼 시의 동작 관리
    void EndInteracting()
    {
        dialogWindow.enabled = false;
        pS.UnitState = UnitState.Idle;

        switch (interactType)
        {
            case InteractType.Shop:
                break;
            case InteractType.EquipmentShop:
                break;
            case InteractType.GateKeeper:
                selectStageUI.SetActive(false);
                break;
            default:
                break;
        }
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
    /// 대화 시작 시 실행할 동작들을 실행한다.
    /// </summary>
    void TalkStartProcess()
    {
        interactStep = InteractStep.Meeting;
        gameUI.enabled = false;
        dialogWindow.enabled = true;
        interactName_Text.text = interactName;
        sentencesIndex = 0;
        sentences.Clear();
    }

    /// <summary>
    /// 플레이어의 조작 및 필요한 상황에 따라 대화를 다음 단계로 진행
    /// </summary>
    public void Continue()
    {
        /*if (sentencesIndex == sentences.Count)
            ;*/

        dialog_Text.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(PrintSenteces());
    }
    /// <summary>
    /// sentences 리스트의 문장마다 한글자씩 텀을주어 출력하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator PrintSenteces()
    {
        for (int i = 0; i < sentences.Count; i++)
        {
            foreach (char letter in sentences[i])
            {
                dialog_Text.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
            if (i == sentences.Count - 1)
                break;
            dialog_Text.text += "\n";
        }
    }

    public void GoBattle(int sceneNumber)
    {
        GameManager.gm.MoveScene(sceneNumber);
    }
}
