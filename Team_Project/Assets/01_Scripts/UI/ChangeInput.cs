using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeInput : MonoBehaviour
{
    EventSystem system;

    [SerializeField]
    Selectable firstInput;
    [SerializeField]
    Button submitButton;

    void Start()
    {
        system = EventSystem.current;
        // 처음은 이메일 Input Field를 선택하도록 한다.
        firstInput.Select();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            // Tab + LeftShift는 위의 Selectable 객체를 선택
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Tab은 아래의 Selectable 객체를 선택
            //Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            //if (next != null)
            //{
            //    next.Select();
            //}
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            // 엔터키를 치면 로그인 (제출) 버튼을 클릭
            submitButton.onClick.Invoke();
            Debug.Log("Button pressed!");
        }
    }
}