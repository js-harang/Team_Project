using TMPro;
using UnityEngine;

public enum InteractType
{
    Shop,
    EquipmentShop,
    GateKeeper,
}

public class InteractProperty : MonoBehaviour
{
    [SerializeField] InteractType interactType;

    // 현재 자신이 갖고 있는 퀘스트 데이터
    public QuestGiver giverIsMe;

    // 퀘스트용 자신의 ID
    public NPCID myID;

    public InteractType InteractType { get { return interactType; } }

    [SerializeField] int interactId;
    public int InteractId { get { return interactId; } }

    string interactName;
    public string InteractName { get { return interactName; } }

    bool imTalking;
    public bool ImTalking 
    { 
        set 
        { 
            imTalking = value;
            if (value)
                PlayerTalkToMe();
            else
                EndTalkWithPlayer();
        } 
    }

    // NPC 머리 위에 정보창 변수들
    [SerializeField] GameObject npcProperty_Canvas;
    [SerializeField] TMP_Text myRole_Text;
    [SerializeField] TMP_Text myName_Text;

    // npc의 애니메이터 변수
    Animator npcAnim;
    // 대화 시 메인 카메라를 위치시킬 포인트
    public Transform closeUpPoint;

    private void Start()
    {
        ShowMyRole();
        giverIsMe = GetComponentInParent<QuestGiver>();
        interactName = myID.ToString();
        myName_Text.text = interactName;
        npcAnim = GetComponentInParent<Animator>();
    }

    // 자신의 타입에 따라 역할을 줄력
    private void ShowMyRole()
    {
        switch (interactType)
        {
            case InteractType.Shop:
                myRole_Text.text = "<상점>";
                break;
            case InteractType.EquipmentShop:
                myRole_Text.text = "<장비 상점>";
                break;
            case InteractType.GateKeeper:
                myRole_Text.text = "<게이트 키퍼>";
                break;
            default:
                break;
        }
    }

    void PlayerTalkToMe()
    {
        npcProperty_Canvas.SetActive(false);
        CameraFollow cF = Camera.main.GetComponent<CameraFollow>();
        cF.target = closeUpPoint;
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("Player"));
        npcAnim.SetBool("end", false);
        npcAnim.SetBool("start", true);
        if (giverIsMe == null)
            return;

        giverIsMe.HereForTalkToMe();
        giverIsMe.GetPlayerDoingQuest();
    }

    void EndTalkWithPlayer()
    {
        npcProperty_Canvas.SetActive(true);
        CameraFollow cF = Camera.main.GetComponent<CameraFollow>();
        cF.target = GameObject.FindWithTag("Player").transform;
        Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Player");
        Camera.main.transform.rotation = Quaternion.Euler(20, 0, 0);
        npcAnim.SetBool("end", true);
        npcAnim.SetBool("start", false);
    }
}
