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

    #region 스킬 아이콘 속성
    [SerializeField] private Image skillIcon;
    public Image SkillIcon { get { return skillIcon; } set { skillIcon = value; } }
    #endregion

    #region 스킬 속성
    [SerializeField] private PlayerAttack skill;
    public PlayerAttack Skill { get { return skill; } set { skill = value; } }
    #endregion

    #region 위치 변수
    [SerializeField] private Transform originPos;
    RectTransform rect;
    #endregion

    #region 캔바스 관련
    [SerializeField]
    Transform canvas;
    CanvasGroup canvasGroup;
    #endregion

    private void Awake()
    {
        StartSetting();
    }

    #region 드래그 시작 할 때
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas);            // 부모 오브젝트를 canvas 로 설정
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }
    #endregion

    #region 드래그 중 일때
    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }
    #endregion

    #region 드래그 끝날때
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

        if (skillPannel.skillAtk[skillIndex] == null || skillPannel.skillIcon[skillIndex].sprite == null)
            return;

        skill = skillPannel.skillAtk[skillIndex];
        skillIcon.sprite = skillPannel.skillIcon[skillIndex].sprite;
    }
    #endregion
}
