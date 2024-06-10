using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        } 
    }

    PlayerInteract pI;

    // UI 캔버스 변수
    public Canvas gameUI;

    // 대화창 캔버스 변수
    public Canvas dialogWindow;

    // 대화창 텍스트 변수들
    public TMP_Text interatName_Text;
    public TMP_Text dialog_Text;

    // 플레이어로부터 전달받을 현재 대화중인 상대 오브젝트의 정보
    InteractType interactType;
    public InteractType InteractType { get; set; }

    int interactId;
    public int InteractId { get; set; }

    string interactName;
    public string InteractName { get; set; }

    private void Start()
    {
        // PlayerInteract 컴포넌트를 변수에 저장
        pI = GameObject.FindWithTag("Player").GetComponent<PlayerInteract>();
    }

    private void Update()
    {
        if (!nowInteracting)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            EndInteracting();
    }

    // 상호작용 시작 시 UI들의 상태 관리
    void StartInteracting()
    {
        gameUI.enabled = false;
        dialogWindow.enabled = true;
        interatName_Text.text = interactName;
    }

    // 상호작용 끝낼 시 UI들의 상태 관리
    void EndInteracting()
    {
        gameUI.enabled = true;
        dialogWindow.enabled = false;
    }
}
