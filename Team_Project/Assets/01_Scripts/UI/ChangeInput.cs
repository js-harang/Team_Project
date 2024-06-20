using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Title Scene Tap 기능
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
        // 선택된 오브젝트가 없으면 Id 입력창을 선택
        if (system.currentSelectedGameObject == null && Input.GetKeyDown(KeyCode.Tab))
        {
            firstSelect.Select();
            return;
        }

        // Tab + LeftShift 기능 : 위의 Selectable 객체를 선택
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (next != null)
                next.Select();
        }
        // Tab 기능 : 아래의 Selectable 객체를 선택
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
                next.Select();
        }
    }
}
