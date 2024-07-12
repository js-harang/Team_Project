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

    public InteractType InteractType { get { return interactType; } }

    [SerializeField] int interactId;
    public int InteractId { get { return interactId; } }

    [SerializeField] string interactName;
    public string InteractName { get { return interactName; } }

    bool imtalking;
    public bool ImTalking 
    { 
        set 
        { 
            imtalking = value;
            if (value)
                PlayerTalkToMe();
            else
                EndTalkWithPlayer();
        } 
    }

    // NPC �Ӹ� ���� ����â ������
    [SerializeField] GameObject npcProperty_Canvas;
    [SerializeField] TMP_Text myRole_Text;
    [SerializeField] TMP_Text myName_Text;

    // npc�� �ִϸ����� ����
    Animator npcAnim;
    // ��ȭ �� ���� ī�޶� ��ġ��ų ����Ʈ
    public Transform closeUpPoint;

    private void Start()
    {
        ShowMyRole();
        myName_Text.text = interactName;
        npcAnim = GetComponentInParent<Animator>();
    }

    // �ڽ��� Ÿ�Կ� ���� ������ �ٷ�
    private void ShowMyRole()
    {
        switch (interactType)
        {
            case InteractType.Shop:
                myRole_Text.text = "<����>";
                break;
            case InteractType.EquipmentShop:
                myRole_Text.text = "<��� ����>";
                break;
            case InteractType.GateKeeper:
                myRole_Text.text = "<����Ʈ Ű��>";
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
