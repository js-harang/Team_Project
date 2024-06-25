using TMPro;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;

// npc���� ��ȭ �ܰ� ������ ��)�λ�, ������ ���� ��
public enum InteractStep
{
    Meeting,
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
                    break;
                case InteractStep.Action:
                    PrintSentences();
                    break;
                case InteractStep.End:
                    EndInteracting();
                    break;
                default:
                    break;
            }
        }
    }

    bool nowInteracting;
    public bool NowInteracting 
    { 
        get { return nowInteracting; } 
        set 
        {
            nowInteracting = value;
            if (nowInteracting)
                TalkStart();
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

    // �ؽ�Ʈ�� �ҷ��� �ؽ�Ʈ������ ��ġ ����
    string filePath;

    // �ҷ��� �ؽ�Ʈ ������� ��Ƶδ� ����Ʈ ����
    List<string> sentences = new List<string>();

    // �÷��̾�κ��� ���޹��� ���� ��ȭ���� ��� ������Ʈ�� ����
    InteractType interactType;
    public InteractType InteractType { get { return interactType; } set { interactType = value; } }

    int interactId;
    public int InteractId { get { return interactId; } set { interactId = value; } }

    string interactName;
    public string InteractName { get { return interactName; } set { interactName = value; } }

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

        if (Input.GetKeyDown(KeyCode.Escape))
            InteractStep = InteractStep.End;

        if (Input.GetKeyDown(KeyCode.X))
            InteractStep++;
    }

    // ��ȣ�ۿ� ���� ���� ���� ����
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
    /// �ؽ�Ʈ ������ �о ���� ��ȭ �ܰ��� ������� sentences ����Ʈ�� ����
    /// </summary>
    /// <param name="id">��ȭ���� NPC�� ID</param>
    /// <param name="num">NPC���� ��ȭ �ܰ�</param>
    void ReadLineAndStore(int id, InteractStep interactStep)
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
    /// ��ȭ ���� �� ������ ���۵��� �����Ѵ�.
    /// </summary>
    void TalkStart()
    {
        gameUI.enabled = false;
        dialogWindow.enabled = true;
        interactName_Text.text = interactName;
        sentences.Clear();
        InteractStep = 0;
    }

    /// <summary>
    /// �÷��̾��� ���� �� �ʿ��� ��Ȳ�� ���� ��ȭ�� ���� �ܰ�� ����
    /// </summary>
    void PrintSentences()
    {
        dialog_Text.text = string.Empty;
        ReadLineAndStore(InteractId, interactStep);
        StartCoroutine(PrintSentenceLetter());
    }
    /// <summary>
    /// sentences ����Ʈ�� ���帶�� �ѱ��ھ� �����־� ����ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator PrintSentenceLetter()
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
