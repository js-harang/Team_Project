using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerState pState;
    // player�� ��ȣ�ۿ� ������ ������Ʈ ��ó�� �ִ����� bool ����
    bool isMeetInteract;

    // player�� ������ ��ȣ�ۿ� ���� ������Ʈ�� ������ �����ϴ� ����
    InteractProperty interPP;
    [SerializeField]
    string interactType;
    [SerializeField]
    int interactId;
    [SerializeField]
    string interactName;

    private void Start()
    {
        pState = GetComponent<PlayerState>();
    }

    // ������Ʈ ��ó�� �ִ� ���¶�� X�� ���� ��ȣ�ۿ� ���°� �ȴ�.
    private void Update()
    {
        if (isMeetInteract)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                InteractController.NowInteracting = true;
                pState.UnitState = UnitState.Interact;
            }
        }
    }

    // ������Ʈ�� �������� true��, �� ������Ʈ�� ������ ���� 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InteractObj"))
        {
            isMeetInteract = true;
            interPP = other.GetComponent<InteractProperty>();
            interactType = interPP.InteractType;
            interactId = interPP.InteractId;
            interactName = interPP.InteractName;
        }
    }

    // ������Ʈ�� �������� false�� �ٲ�
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InteractObj"))
        {
            isMeetInteract = false;
        }
    }
}
