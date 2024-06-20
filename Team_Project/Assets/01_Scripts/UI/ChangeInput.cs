using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// tab ����
/// </summary>
public class ChangeInput : MonoBehaviour
{
    EventSystem system;

    [SerializeField]
    Button loginBtn;
    [SerializeField]
    Button createBtn;

    private void Start()
    {
        system = EventSystem.current;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab))
        {
            // Tab + LeftShift�� ���� Selectable ��ü�� ����
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();

            if (next != null)
            {
                next.Select();

                //if (next == loginBtn || next == createBtn)
                    //next.interactable = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Tab�� �Ʒ��� Selectable ��ü�� ����
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {
                next.Select();

                //if (next == loginBtn || next == createBtn)
                //    next.interactable = true;
            }
        }
    }
}
