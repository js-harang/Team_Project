using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    // 플레이어로부터 전달받을 현재 대화중인 상대 오브젝트의 정보
    InteractType interactType;
    public InteractType InteractType { get { return interactType; } set {interactType = value; } }

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

    // 상호작용 시작 시 UI들의 상태 관리
    void StartInteracting()
    {
        gameUI.enabled = false;
        dialogWindow.enabled = true;
        interactName_Text.text = interactName;

        if (interactType == InteractType.GateKeeper)
            selectStageUI.SetActive(true);
    }

    // 상호작용 끝낼 시의 동작 관리
    void EndInteracting()
    {
        dialogWindow.enabled = false;
        pS.UnitState = UnitState.Idle;
    }

    public void GoBattle()
    {
        GameManager.gm.sceneNumber = 3;
        SceneManager.LoadScene("99_LoadingScene");
    }
}
