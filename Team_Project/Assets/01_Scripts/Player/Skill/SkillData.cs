using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 버튼 칸에 있는 버튼스킬
public class SkillData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region 스킬 아이콘 속성
    private Image skillIcon;
    public Image SkillIcon { get { return skillIcon; } set { skillIcon = value; } }
    #endregion

    #region 스킬 속성
    private PlayerAttack skill;
    public PlayerAttack Skill { get { return skill; } set { skill = value; } }
    #endregion

    [Space(10)]
    #region 위치값
    private RectTransform rect;
    private Transform originsPos;
    #endregion

    [Space(10)]
    #region 부모 버튼 속성
    [SerializeField] private SkillButton skillButton;
    public SkillButton Button { get { return skillButton; } set { skillButton = value; } }
    #endregion

    #region 캔바스 변수
    Transform canvas;
    CanvasGroup canvasGroup;
    #endregion

    private void Awake()
    {
        skillButton = GetComponentInParent<SkillButton>();
        skillIcon = GetComponent<Image>();

        rect = GetComponent<RectTransform>();
        originsPos = transform.parent;

        canvas = GetComponentInParent<Canvas>().transform;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    #region 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }
    #endregion

    #region 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        rect.transform.position = eventData.position;
    }
    #endregion

    #region 드래그 끝날때
    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent != canvas)
        {
            skill = null;
            skillIcon.sprite = null;
            skillButton.Skill = null;
        }

        rect.position = originsPos.position;
        canvasGroup.blocksRaycasts = true;
    }
    #endregion
}
