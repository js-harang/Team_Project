using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractController : MonoBehaviour
{
    // ��ȣ�ۿ��� ���۵Ǿ����� ������ Ȯ���ϸ鼭 �Ӽ��� �̿��� �޼��� ȣ��
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

    // UI ĵ���� ����
    public Canvas gameUI;

    // ��ȭâ ĵ���� ����
    public Canvas dialogWindow;

    // ��ȭâ �ؽ�Ʈ ������
    public TMP_Text interatName_Text;
    public TMP_Text dialog_Text;

    void StartInteracting()
    {
        gameUI.enabled = false;
        dialogWindow.enabled = true;
    }
}
