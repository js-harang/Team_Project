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

    PlayerInteract pI;

    // UI ĵ���� ����
    public Canvas gameUI;

    // ��ȭâ ĵ���� ����
    public Canvas dialogWindow;

    // ��ȭâ �ؽ�Ʈ ������
    public TMP_Text interatName_Text;
    public TMP_Text dialog_Text;

    // �÷��̾�κ��� ���޹��� ���� ��ȭ���� ��� ������Ʈ�� ����
    InteractType interactType;
    public InteractType InteractType { get; set; }

    int interactId;
    public int InteractId { get; set; }

    string interactName;
    public string InteractName { get; set; }

    private void Start()
    {
        // PlayerInteract ������Ʈ�� ������ ����
        pI = GameObject.FindWithTag("Player").GetComponent<PlayerInteract>();
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
        interatName_Text.text = interactName;
    }

    // ��ȣ�ۿ� ���� �� UI���� ���� ����
    void EndInteracting()
    {
        gameUI.enabled = true;
        dialogWindow.enabled = false;
    }
}
