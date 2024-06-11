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

    PlayerState pS;

    // UI ĵ���� ����
    public Canvas gameUI;

    // ��ȭâ ĵ���� ����
    public Canvas dialogWindow;

    // ��ȭâ �ؽ�Ʈ ������
    public TMP_Text interactName_Text;
    public TMP_Text dialog_Text;

    // �÷��̾�κ��� ���޹��� ���� ��ȭ���� ��� ������Ʈ�� ����
    InteractType interactType;
    public InteractType InteractType { get { return interactType; } set {interactType = value; } }

    int interactId;
    public int InteractId { get { return interactId; } set { interactId = value; } }

    string interactName;
    public string InteractName { get { return interactName; } set { interactName = value; } }

    private void Start()
    {
        // PlayerState ������Ʈ�� ������ ����
        pS = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (!nowInteracting)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            EndInteracting();
    }

    // ��ȣ�ۿ� ���� �� UI���� ���� ����
    void StartInteracting()
    {
        gameUI.enabled = false;
        dialogWindow.enabled = true;
        interactName_Text.text = interactName;
    }

    // ��ȣ�ۿ� ���� �� UI���� ���� ����
    void EndInteracting()
    {
        gameUI.enabled = true;
        dialogWindow.enabled = false;
        pS.UnitState = UnitState.Idle;
    }
}
