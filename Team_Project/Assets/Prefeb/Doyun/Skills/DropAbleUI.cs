using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropAbleUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private Image image;
    private RectTransform rect;

    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    // 마우스가 현재 오브젝트 안으로 들어갈 때 1회 호출
    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    // 마우스가 현재 오브젝트 밖으로 나갈때 1회 호출
    public void OnPointerExit(PointerEventData eventData)
    {

    }

    // 오브젝트 영역 내에서 드롭 할 때 1회 호출
    public void OnDrop(PointerEventData eventData)
    {
        // pointerDrag : 현재 드래그 하고 있는 대상(= 아이템)
        if (eventData.pointerDrag != null)
        {
            // 드래그 하고 있는 대상의 부모를 현재 오브젝트로 설정, 위치를 현재 오브젝트와 동일하게 설정
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
        }
    }
}