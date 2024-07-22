using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 스킬창에서 끌어오는 스킬
public class SkillSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public SkillDirectory skillPannel;

    [SerializeField] int skillIndex;

    [Space(10)]
    #region 스킬 아이콘 속성
    [SerializeField] private Image skillIcon;
    public Image SkillIcon { get { return skillIcon; } set { skillIcon = value; } }
    #endregion

    #region 스킬 속성
    [SerializeField] private Attack skill;
    public Attack Skill { get { return skill; } set { skill = value; } }
    #endregion

    [Space(10)]
    #region 위치 변수
    [SerializeField] private Transform originPos;
    RectTransform rect;
    #endregion

    [Space(10)]
    #region 캔바스 관련
    [SerializeField]
    Transform canvas;
    CanvasGroup canvasGroup;
    #endregion

    private void Awake()
    {
        StartSetting();
    }

    #region 드래그 시작 할 때 캔바스 부모설정
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas);            // 부모 오브젝트를 canvas 로 설정
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }
    #endregion

    #region 드래그 중 일때 현재 마우스 위치에 스킬 위치
    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }
    #endregion

    #region 드래그 끝날때 원래 위치로 돌아감
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originPos);
        rect.position = originPos.position;
        canvasGroup.blocksRaycasts = true;
    }
    #endregion

    #region 시작 세팅
    void StartSetting()
    {
        canvas = GetComponentInParent<Canvas>().transform;
        canvasGroup = GetComponent<CanvasGroup>();

        skillIcon = GetComponent<Image>();

        originPos = transform.parent;
        rect = GetComponent<RectTransform>();

        skillPannel = FindObjectOfType<SkillDirectory>();

        if (skillPannel.skillAtks[skillIndex] == null || skillPannel.skillSprites[skillIndex] == null)
        {
            Debug.Log("비었음");
            return;
        }

        skill = skillPannel.skillAtks[skillIndex];
        skillIcon.sprite = skillPannel.skillSprites[skillIndex];
    }
    #endregion
}
