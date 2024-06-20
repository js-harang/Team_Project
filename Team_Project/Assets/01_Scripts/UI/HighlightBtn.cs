using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    EventSystem highlightSystem;

    private void Start()
    {
        highlightSystem = EventSystem.current;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
    }
}
