using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    PlayerState pState;
    // player가 상호작용 가능한 오브젝트 근처에 있는지의 bool 변수
    bool isMeetInteract;

    // 대화 가능한 오브젝트를 표시하는 UI 변수
    public GameObject interactCheck;
    public TMP_Text interactName_Txt;

    // InteractController 클래스 변수
    InteractController interCon;

    // player가 접촉한 상호작용 가능 오브젝트의 정보를 저장하는 변수
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

    // 오브젝트 근처에 있는 상태라면 X를 눌러 상호작용 상태가 된다.
    private void Update()
    {
        if (isMeetInteract)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                interCon.NowInteracting = true;
                pState.UnitState = UnitState.Interact;
                InteractCheck();
            }
        }
    }

    // 오브젝트를 만났음을 true로, 그 오브젝트의 정보를 저장 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InteractObj"))
        {
            isMeetInteract = true;
            interPP = other.GetComponent<InteractProperty>();
            interactType = interPP.InteractType;
            interactId = interPP.InteractId;
            interactName = interPP.InteractName;
            InteractCheck();
        }
    }

    // 오브젝트를 만났음을 false로 바꿈
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InteractObj"))
        {
            isMeetInteract = false;
            InteractCheck();
        }
    }

    // InteractCheck가 비활성화 상태면 활성화, 활성화 상태면 비활성화
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
}
