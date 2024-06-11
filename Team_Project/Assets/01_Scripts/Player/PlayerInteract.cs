using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    PlayerState pState;
    // player�� ��ȣ�ۿ� ������ ������Ʈ ��ó�� �ִ����� bool ����
    bool isMeetInteract;

    // ��ȭ ������ ������Ʈ�� ǥ���ϴ� UI ����
    public GameObject interactCheck;
    public TMP_Text interactName_Txt;

    // InteractController Ŭ���� ����
    InteractController interCon;

    // player�� ������ ��ȣ�ۿ� ���� ������Ʈ�� ������ �����ϴ� ����
    InteractProperty interPP;
    [SerializeField]
    InteractType interactType;
    [SerializeField]
    int interactId;
    [SerializeField]
    string interactName;

    private void Start()
    {
        pState = GetComponent<PlayerState>();
        interCon = FindObjectOfType<InteractController>().GetComponent<InteractController>();
    }

    // ������Ʈ ��ó�� �ִ� ���¶�� X�� ���� ��ȣ�ۿ� ���°� �ȴ�.
    private void Update()
    {
        if (isMeetInteract)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                LetsTalk();
            }
        }
    }

    // ������Ʈ�� �������� true��, �� ������Ʈ�� ������ ���� 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InteractObj"))
        {
            interPP = other.GetComponent<InteractProperty>();
            WhoAreYou();
        }
    }

    // ������Ʈ�� �������� false�� �ٲ�
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InteractObj"))
        {
            isMeetInteract = false;
            InteractCheck();
        }
    }

    // �÷��̾ ��ó�� �ν��� NPC�� ������ ������
    void WhoAreYou()
    {
        isMeetInteract = true;
        interactType = interPP.InteractType;
        interactId = interPP.InteractId;
        interactName = interPP.InteractName;
        InteractCheck();
    }

    // NPC�� ��ȭ�� ������ ���� ����
    void LetsTalk()
    {
        interCon.NowInteracting = true;
        PassInteractInfo();
        pState.UnitState = UnitState.Interact;
        InteractCheck();
    }

    // InteractCheck�� ��Ȱ��ȭ ���¸� Ȱ��ȭ, Ȱ��ȭ ���¸� ��Ȱ��ȭ
    void InteractCheck()
    {
        if (!interactCheck.activeSelf)
        {
            interactCheck.SetActive(true);
            interactName_Txt.text = interactName;
        }
        else if (interactCheck.activeSelf)
            interactCheck.SetActive(false);
    }

    // InteractController ���� ��ȭ�� ������ NPC�� ������ �ѱ�
    void PassInteractInfo()
    {
        interCon.InteractType = interactType;
        interCon.InteractId = interactId;
        interCon.InteractName = interactName;
    }
}
