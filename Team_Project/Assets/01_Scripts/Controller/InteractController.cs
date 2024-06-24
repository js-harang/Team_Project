using TMPro;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class InteractController : MonoBehaviour
{
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

    // 불러온 텍스트 문장들을 모아두는 리스트 변수
    List<string> sentences = new List<string>();

    // 현재 출력한 텍스트의 인덱스 번호 변수
    int sentencesIndex;

    // npc와의 대화 단계 예)인사, 선택지 고르기 등
    int npcDialogueStep;

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
        gameUI.enabled = false;
        dialogWindow.enabled = true;
        interactName_Text.text = interactName;
        npcDialogueStep = 0;
        sentencesIndex = 0;
        sentences.Clear();

        ReadLineAndStore(InteractId, npcDialogueStep);

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
    void ReadLineAndStore(int id, int num)
    {
        filePath = $"C:\\Users\\YONSAI\\Desktop\\Team_Project\\Team_Project\\Assets\\21_Data\\{id} Dialogue\\{num}.txt";

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

    public void Continue()
    {
        /*if (sentencesIndex == sentences.Count)
            ;*/

        dialog_Text.text = sentences[sentencesIndex];
        sentencesIndex++;
    }

    public void GoBattle(int sceneNumber)
    {
        GameManager.gm.MoveScene(sceneNumber);
    }

}
