using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAbleUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvas;           // UI �� �ҼӵǾ��ִ� �ֻ�� Canvas
    private Transform previousParent;   // �ش� ������Ʈ ������ �ҼӵǾ� �ִ� �θ� ������Ʈ�� Transform
    private RectTransform rect;         // UI ��ġ ��� ���� RectTransform
    private CanvasGroup canvasGroup;    // UI ���İ� �� ��ȣ�ۿ� ��� ���� CanvasGroup

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // ���� ������Ʈ �� �巡�� �ϱ� ���� �� �� 1ȸ ȣ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�� ���� �ҼӵǾ� �ִ� �θ� ������Ʈ�� Transform �� ����
        previousParent = transform.parent;

        // ���� �巡�� ���� UI �� ȭ�� �ֻ�ܿ� ���̰� �ϱ� ����
        transform.SetParent(canvas);            // �θ� ������Ʈ�� canvas �� ����
        transform.SetAsLastSibling();           // ȭ�鿡 �� ���� ���̱� ���� canvas �� ���� �Ʒ� ���� �ڽĿ�����Ʈ �� ����

        // �巡�� �ϴ� ������Ʈ�� �ϳ��� �ƴ� �ڽ� ������Ʈ ���� ������ ������ �ֱ� ������ CanvasGroup ���� ����
        canvasGroup.alpha = 0.6f;  // ���� �巡�� ���� ������Ʈ ���İ� ���߱�
        canvasGroup.blocksRaycasts = false;  // ���Կ� ����ϱ����� ���콺�� ������ �浹�Ҽ� �ֵ��� �巡���ϴ� ������Ʈ�� �浹 ó���� ��
    }

    // ���� ������Ʈ �� �巡�� �� �϶� �� ������ ȣ��
    public void OnDrag(PointerEventData eventData)
    {
        // ���� ��ũ���� �� ���콺 ��ġ�� UI �� ��ġ�� ��(UI �� ���콺�� �i�ƴٴϴ� ����)
        rect.position = eventData.position;
    }

    // ���� ������Ʈ�� �巡�� �� ���� �� �� 1 ȸ ȣ��
    public void OnEndDrag(PointerEventData eventData)
    {
        // �巡�׸� �����ϸ� �θ� Canvas �� �����Ǿ� �ֱ⿡
        // �巡�׸� �����Ҷ� �θ� Canvas �̸� ������ ������ �ƴ� ������ ����
        // ����� �ߴٴ� �� �̱⿡ �巡�� ������ �ִ� ������ �������� ������ �̵�
        if (transform.parent == canvas)
        {
            // �������� �ҼӵǾ��ִ� previousParent �� �ڽ����� �����ϰ�, �ش� ��ġ�� ����
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
        }

        // �ٽ� ������ �ϰ� ����� �浹 ó�� Ȱ��ȭ
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
