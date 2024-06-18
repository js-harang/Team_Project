using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
            else
                EndInteracting();
        } 
    }

    PlayerState pS;

    // UI ĵ���� ����
    public Canvas gameUI;

    // ��ȭâ ĵ���� ����
    public Canvas dialogWindow;

    // ��Ʋ �������� ����â ����
    public GameObject selectStageUI;

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
            NowInteracting = false;
    }

    // ��ȣ�ۿ� ���� �� UI���� ���� ����
    void StartInteracting()
    {
        gameUI.enabled = false;
        dialogWindow.enabled = true;
        interactName_Text.text = interactName;

        if (interactType == InteractType.GateKeeper)
            selectStageUI.SetActive(true);
    }

    // ��ȣ�ۿ� ���� ���� ���� ����
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
