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

    // NPC 머리 위에 정보창 Text 변수들
    [SerializeField] TMP_Text myRole_Text;

    [SerializeField] TMP_Text myName_Text;

    private void Start()
    {
        ShowMyRole();
        myName_Text.text = interactName;
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
}
