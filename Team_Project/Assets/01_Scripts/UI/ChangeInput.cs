using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Title Scene Tap ���
/// </summary>
public class ChangeInput : MonoBehaviour
{
    EventSystem system;

    [SerializeField]
    Selectable firstSelect;

    void Start()
    {
        system = EventSystem.current;
    }

    void Update()
    {
        // ���õ� ������Ʈ�� ������ Id �Է�â�� ����
        if (system.currentSelectedGameObject == null && Input.GetKeyDown(KeyCode.Tab))
        {
            firstSelect.Select();
            return;
        }

        // Tab + LeftShift ��� : ���� Selectable ��ü�� ����
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (next != null)
                next.Select();
        }
        // Tab ��� : �Ʒ��� Selectable ��ü�� ����
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
                next.Select();
        }
    }
}
