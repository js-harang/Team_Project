using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Title Scene Tap ���
/// </summary>
public class ChangeInput : MonoBehaviour
{
    EventSystem eventSystem;

    [SerializeField] Selectable firstSelect;

    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        // ���õ� ������Ʈ�� ������ Id �Է�â�� ����
        if (eventSystem.currentSelectedGameObject == null && Input.GetKeyDown(KeyCode.Tab))
        {
            firstSelect.Select();
            return;
        }

        InputTab();
    }

    private void InputTab()
    {
        // Tab + LeftShift ��� : ���� Selectable ��ü�� ����
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (next != null)
                next.Select();
        }
        // Tab ��� : �Ʒ��� Selectable ��ü�� ����
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
                next.Select();
        }
    }
}
