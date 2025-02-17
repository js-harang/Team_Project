using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerState pState;
    PlayerCombatInput pComInput;

    // player가 상호작용 가능한 오브젝트 근처에 있는지의 bool 변수
    bool isMeetInteract;

    // 대화 가능한 오브젝트를 표시하는 UI 변수
    [SerializeField] GameObject interactCheck;
    [SerializeField] TMP_Text interactName_Txt;

    // InteractController 클래스 변수
    InteractController interCon;

    // player가 접촉한 상호작용 가능 오브젝트의 정보를 저장하는 변수
    InteractProperty interPP;

    private void Start()
    {
        pState = GetComponent<PlayerState>();
        pComInput = GetComponent<PlayerCombatInput>();
        interCon = FindObjectOfType<InteractController>().GetComponent<InteractController>();
    }

    // 오브젝트 근처에 있는 상태라면 C를 눌러 상호작용 상태가 된다.
    private void Update()
    {
        if (interCon.NowInteracting)
            return;

        if (isMeetInteract)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                LetsTalk();
            }
        }
    }

    // 오브젝트를 만났음을 true로, 그 오브젝트의 정보를 저장 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InteractObj"))
        {
            WhoAreYou(other);
            if (pComInput == null)
                return;
            pComInput.enabled = false;
        }
    }

    // 오브젝트를 만났음을 false로 바꿈
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InteractObj"))
        {
            isMeetInteract = false;
            InteractCheck();
            if (pComInput == null)
                return;
            pComInput.enabled = true;
        }
    }

    // 플레이어가 근처에 인식한 NPC의 정보를 가져옴
    private void WhoAreYou(Collider other)
    {
        isMeetInteract = true;
        interPP = other.GetComponent<InteractProperty>();
        InteractCheck();
    }

    // NPC와 대화를 시작할 때의 동작
    private void LetsTalk()
    {
        // InteractController 에게 대화를 시작한 NPC의 정보를 넘김
        interCon.interPP = interPP;
        interCon.NowInteracting = true;
        InteractCheck();
        pState.UnitState = UnitState.Interact;
    }

    // isMeetInteract가 true면 활성화, false면 비활성화
    private void InteractCheck()
    {
        if (isMeetInteract)
        {
            interactCheck.SetActive(true);
            interactName_Txt.text = interPP.InteractName;
        }
        else if (!isMeetInteract)
            interactCheck.SetActive(false);
    }
}
