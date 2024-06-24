using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAbleUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvas;           // UI 가 소속되어있는 최상단 Canvas
    private Transform previousParent;   // 해당 오브젝트 직전에 소속되어 있던 부모 오브젝트의 Transform
    private RectTransform rect;         // UI 위치 제어를 위한 RectTransform
    private CanvasGroup canvasGroup;    // UI 알파값 과 상호작용 제어를 위한 CanvasGroup

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // 현재 오브젝트 를 드래그 하기 시작 할 때 1회 호출
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 직전 소속되어 있던 부모 오브젝트의 Transform 을 저장
        previousParent = transform.parent;

        // 현재 드래그 중인 UI 를 화면 최상단에 보이게 하기 위해
        transform.SetParent(canvas);            // 부모 오브젝트를 canvas 로 설정
        transform.SetAsLastSibling();           // 화면에 맨 위에 보이기 위해 canvas 의 가장 아래 순서 자식오브젝트 로 설정

        // 드래그 하는 오브젝트가 하나가 아닌 자식 오브젝트 들을 가지고 있을수 있기 때문에 CanvasGroup 으로 통제
        canvasGroup.alpha = 0.6f;  // 현재 드래그 중인 오브젝트 알파값 낮추기
        canvasGroup.blocksRaycasts = false;  // 슬롯에 드롭하기위해 마우스와 슬롯이 충돌할수 있도록 드래그하는 오브젝트의 충돌 처리를 끔
    }

    // 현재 오브젝트 를 드래그 중 일때 매 프레임 호출
    public void OnDrag(PointerEventData eventData)
    {
        // 현재 스크린상 의 마우스 위치를 UI 의 위치로 함(UI 가 마우스를 쫒아다니는 형태)
        rect.position = eventData.position;
    }

    // 현재 오브젝트의 드래그 를 종료 할 때 1 회 호출
    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그를 시작하면 부모가 Canvas 로 설정되어 있기에
        // 드래그를 종료할때 부모가 Canvas 이면 아이템 슬롯이 아닌 엉뚱한 곳에
        // 드롭을 했다는 뜻 이기에 드래그 직전에 있던 아이템 슬롯으로 아이템 이동
        if (transform.parent == canvas)
        {
            // 마지막에 소속되어있던 previousParent 의 자식으로 설정하고, 해당 위치로 설정
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
        }

        // 다시 불투명 하게 만들고 충돌 처리 활성화
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
