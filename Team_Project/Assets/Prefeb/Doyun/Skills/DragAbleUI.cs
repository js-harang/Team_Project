using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAbleUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Transform canvas;           // UI 가 소속되어있는 최상단 Canvas
    [SerializeField]
    private CanvasGroup canvasGroup;    // UI 알파값 과 상호작용 제어를 위한 CanvasGroup
    [Space(10)]
    [SerializeField]
    private RectTransform rect;         // UI 위치 제어를 위한 RectTransform
    [SerializeField]
    private Transform previousParent;   // 해당 오브젝트 직전에 소속되어 있던 부모 오브젝트의 Transform

    [Space(10)]
    // 오브젝트의 스킬 정보
    public PlayerAttack skill;

    [Space(10)]
    // 버튼의 이전 스킬
    [SerializeField]
    SkillButton previousButton;

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

        // 클릭 비활성화
        ClickDisAble();

        if (previousParent.GetComponent<SkillButton>())
        {
            Debug.Log("버튼 등록");
            previousButton = previousParent.GetComponent<SkillButton>();

            // 이전 버튼의 스킬이 쿨타임 일때
            if (previousButton.CoolTime > 0)
            {
                transform.SetParent(previousParent);
                ClickAble();
                Debug.Log("스킬이 쿨타임 일때는 변경할수 없습니다.");
            }
        }
    }

    // 현재 오브젝트 를 드래그 중 일때 매 프레임 호출
    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;     // 현재 스크린상 의 마우스 위치를 UI 의 위치로 함(UI 가 마우스를 쫒아다니는 형태)
    }

    public void OnEndDrag(PointerEventData eventData)   // 현재 오브젝트의 드래그 를 종료 할 때 1 회 호출
    {
        // 드래그 끝난 지점이 Canvas 이면
        if (transform.parent == canvas)
        {
            Destroy(gameObject);
            return;
        }

        // 이전 버튼이 있을때
        if (previousButton != null)
        {
            // 이전 버튼의 스킬이 쿨타임 일때
            if (previousButton.CoolTime > 0)
            {
                transform.SetParent(previousParent);
                rect.transform.position = previousParent.GetComponent<RectTransform>().position;
                return;
            }
        }

        // 클릭 활성화
        ClickAble();


        // 이전 스킬칸 스킬 리셋
        if (previousParent.GetComponent<SkillButton>())     // 이전 부모오브젝트가 SkillButton 이면 스킬 리셋
        {
            Debug.Log("스킬 리셋");
            previousButton = previousParent.GetComponent<SkillButton>();
            previousButton.skill = null;
        }
        // 현재 스킬칸 스킬 등록
        if (transform.parent.GetComponent<SkillButton>())
        {
            Debug.Log("스킬 등록");
            previousButton = transform.parent.GetComponent<SkillButton>();
            previousButton.skill = skill;
            previousButton.transform.SetAsLastSibling();
        }

        transform.SetAsLastSibling();
    }

    #region 클릭 활성,비활성

    // 클릭 활성화
    void ClickAble()
    {
        // 다시 불투명 하게 만들고 충돌 처리 활성화
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    // 클릭 비활성화
    void ClickDisAble()
    {
        // 드래그 하는 오브젝트가 하나가 아닌 자식 오브젝트 들을 가지고 있을수 있기 때문에 CanvasGroup 으로 통제
        canvasGroup.alpha = 0.6f;  // 현재 드래그 중인 오브젝트 알파값 낮추기
        canvasGroup.blocksRaycasts = false;  // 슬롯에 드롭 하기위해 마우스 와 슬롯이 충돌할수 있도록 드래그하는 오브젝트의 충돌 처리를 끔
    }

    #endregion
}
