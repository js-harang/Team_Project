using TMPro;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

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

    // �ؽ�Ʈ�� �ҷ��� �ؽ�Ʈ������ ��ġ ����
    string filePath;

    // �ҷ��� �ؽ�Ʈ ������� ��Ƶδ� ����Ʈ ����
    List<string> sentences = new List<string>();

    // ���� ����� �ؽ�Ʈ�� �ε��� ��ȣ ����
    int sentencesIndex;

    // npc���� ��ȭ �ܰ� ��)�λ�, ������ ���� ��
    int npcDialogueStep;

    // �÷��̾�κ��� ���޹��� ���� ��ȭ���� ��� ������Ʈ�� ����
    InteractType interactType;
    public InteractType InteractType { get { return interactType; } set { interactType = value; } }

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

    // ��ȣ�ۿ� ���� ���� ����
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
    void ReadLineAndStore(int id, int num)
    {
        filePath = $"C:\\Users\\YONSAI\\Desktop\\Team_Project\\Team_Project\\Assets\\21_Data\\{id} Dialogue\\{num}.txt";

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
