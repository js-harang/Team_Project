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

    // ���콺�� ���� ������Ʈ ������ �� �� 1ȸ ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    // ���콺�� ���� ������Ʈ ������ ������ 1ȸ ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {

    }

    // ������Ʈ ���� ������ ��� �� �� 1ȸ ȣ��
    public void OnDrop(PointerEventData eventData)
    {
        // pointerDrag : ���� �巡�� �ϰ� �ִ� ���(= ������)
        if (eventData.pointerDrag != null)
        {
            // �巡�� �ϰ� �ִ� ����� �θ� ���� ������Ʈ�� ����, ��ġ�� ���� ������Ʈ�� �����ϰ� ����
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
        }
    }
}