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

    // NPC �Ӹ� ���� ����â Text ������
    [SerializeField] TMP_Text myRole_Text;

    [SerializeField] TMP_Text myName_Text;

    private void Start()
    {
        ShowMyRole();
        myName_Text.text = interactName;
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
}
