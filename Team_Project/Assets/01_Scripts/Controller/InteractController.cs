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

    // UI 캔버스 변수
    public Canvas gameUI;

    // 대화창 캔버스 변수
    public Canvas dialogWindow;

    // 대화창 텍스트 변수들
    public TMP_Text interatName_Text;
    public TMP_Text dialog_Text;

    void StartInteracting()
    {
        gameUI.enabled = false;
        dialogWindow.enabled = true;
    }
}
