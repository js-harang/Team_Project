using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerState pState;
    PlayerBattleController pBC;
    // player�� ��ȣ�ۿ� ������ ������Ʈ ��ó�� �ִ����� bool ����
    bool isMeetInteract;

    // ��ȭ ������ ������Ʈ�� ǥ���ϴ� UI ����
    public GameObject interactCheck;
    public TMP_Text interactName_Txt;

    // InteractController Ŭ���� ����
    InteractController interCon;

    // player�� ������ ��ȣ�ۿ� ���� ������Ʈ�� ������ �����ϴ� ����
    InteractProperty interPP;
    [SerializeField] InteractType interactType;
    [SerializeField] int interactId;
    [SerializeField] string interactName;

    private void Start()
    {
        pState = GetComponent<PlayerState>();
        //pBC = GetComponent<PlayerBattleController>();
        interCon = FindObjectOfType<InteractController>().GetComponent<InteractController>();
    }

    // ������Ʈ ��ó�� �ִ� ���¶�� X�� ���� ��ȣ�ۿ� ���°� �ȴ�.
    private void Update()
    {
        if (interCon.NowInteracting)
            return;

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
            WhoAreYou(other);
        }
    }

    // ������Ʈ�� �������� false�� �ٲ�
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InteractObj"))
        {
            //pBC.enabled = true;
            isMeetInteract = false;
            InteractCheck();
        }
    }

    // �÷��̾ ��ó�� �ν��� NPC�� ������ ������
    private void WhoAreYou(Collider other)
    {
        //pBC.enabled = false;
        isMeetInteract = true;
        interPP = other.GetComponent<InteractProperty>();
        interactType = interPP.InteractType;
        interactId = interPP.InteractId;
        interactName = interPP.InteractName;
        InteractCheck();
    }

    // NPC�� ��ȭ�� ������ ���� ����
    private void LetsTalk()
    {
        PassInteractInfo();
        InteractCheck();
        interCon.NowInteracting = true;
        pState.UnitState = UnitState.Interact;
    }

    // isMeetInteract�� true�� Ȱ��ȭ, false�� ��Ȱ��ȭ
    private void InteractCheck()
    {
        if (isMeetInteract)
        {
            interactCheck.SetActive(true);
            interactName_Txt.text = interactName;
        }
        else if (!isMeetInteract)
            interactCheck.SetActive(false);
    }

    // InteractController ���� ��ȭ�� ������ NPC�� ������ �ѱ�
    private void PassInteractInfo()
    {
        interCon.InteractType = interactType;
        interCon.InteractId = interactId;
        interCon.InteractName = interactName;
    }
}
