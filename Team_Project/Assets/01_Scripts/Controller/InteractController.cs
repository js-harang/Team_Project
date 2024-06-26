using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

// npc���� ��ȭ �ܰ� ������ ��)�λ�, ������ ���� ��
public enum InteractStep
{
    Meeting,
    ChoiceMenu,
    Action,
    End,
}

public class InteractController : MonoBehaviour
{
    // ���� ��ȭ�� ���� �ܰ踦 ��Ÿ���� ������ ����
    InteractStep interactStep;

    // �Ӽ����� ��ȭ�� �ܰ迡 ���� ����
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

    // ���� ��ȭ �������� ��Ÿ���� �Ӽ� ����
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

    // �÷��̾�κ��� ���޹��� ���� ��ȭ���� ��� ������Ʈ�� ����
    InteractType interactType;
    public InteractType InteractType { get { return interactType; } set { interactType = value; } }

    int interactId;
    public int InteractId { get { return interactId; } set { interactId = value; } }

    string interactName;
    public string InteractName { get { return interactName; } set { interactName = value; } }

    PlayerState pS;

    // UI ĵ���� ����
    public Canvas gameUI;

    // ��ȭâ ĵ���� ����
    public Canvas dialogWindow;

    // ��ȭâ �ؽ�Ʈ ������
    public TMP_Text interactName_Text;
    public TMP_Text dialog_Text;

    // �ؽ�Ʈ�� �ҷ��� �ؽ�Ʈ������ ��ġ ����
    string filePath;

    // �ҷ��� �ؽ�Ʈ ������� ��Ƶδ� ����Ʈ ����
    List<string> sentences = new List<string>();

    // ��ȭ ��� �ڷ�ƾ�� ���� �������� Ȯ���ϴ� ����
    bool activatePrinting;
    bool stopTalking;

    // NPC Ÿ�Ժ� ��ȭ ����â ����
    public GameObject shopDialogChoiceUI;
    public GameObject equipShopDialogChoiceUI;
    public GameObject gateKeeperDialogChoiceUI;

    // ��Ʋ �������� ����â ����
    public GameObject selectStageUI;

    private void Start()
    {
        interactStep = InteractStep.End;
        // PlayerState ������Ʈ�� ������ ����
        pS = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (interactStep == InteractStep.End)
            return;

        DialogControll();
    }

    // ��ȭ �� Ű �Է¿� ���� ����
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
    /// ��ȭ ���� �� ������ ���۵��� �����Ѵ�.
    /// </summary>
    private void StartInteracting()
    {
        gameUI.enabled = false;
        dialog_Text.text = string.Empty;
        dialogWindow.enabled = true;
        interactName_Text.text = interactName;
        InteractStep = InteractStep.Meeting;
    }

    // ��ȣ�ۿ� ���� ���� ���� ����
    public void EndInteracting()
    {
        ChoiceMenuClose();
        dialogWindow.enabled = false;
        pS.UnitState = UnitState.Idle;
    }

    /// <summary>
    /// �ؽ�Ʈ ������ �о ���� ��ȭ �ܰ��� ������� sentences ����Ʈ�� ����
    /// </summary>
    /// <param name="id">��ȭ���� NPC�� ID</param>
    /// <param name="num">NPC���� ��ȭ �ܰ�</param>
    private void ReadLineAndStore(int id, InteractStep interactStep)
    {
        filePath = $"C:\\Users\\YONSAI\\Desktop\\Team_Project\\Team_Project\\Assets\\21_Data\\{id} Dialogue\\{interactStep}.txt";

        // ���� ���� ���� Ȯ��
        if (!File.Exists(filePath))
        {
            Debug.LogError("������ ã�� �� �����ϴ�. ���ϰ�� : " + filePath);
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            sentences.Add(lines[i]);
        }
    }

    /// <summary>
    /// �÷��̾��� ���� �� �ʿ��� ��Ȳ�� ���� ��ȭ�� ���� �ܰ�� ����
    /// </summary>
    private void PrintSentences()
    {
        dialog_Text.text = string.Empty;
        sentences.Clear();
        ReadLineAndStore(InteractId, interactStep);
        StartCoroutine(PrintSentenceLetter());
    }
    /// <summary>
    /// sentences ����Ʈ�� ���帶�� �ѱ��ھ� �����־� ����ϴ� �ڷ�ƾ
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

                if (stopTalking)                        // ����� X Ű�� ������ ��ŵ�ϰ� ��ü ���
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

    // NPC Ÿ�Կ� ���� UI Ȱ��ȭ
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

    // NPC Ÿ�Կ� ���� UI ��Ȱ��ȭ
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
