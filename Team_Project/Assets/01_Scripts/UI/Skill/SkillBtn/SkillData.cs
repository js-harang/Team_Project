using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 버튼 칸에 있는 버튼스킬
public class SkillData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region 스킬 속성
    [SerializeField] private Attack skill;
    public Attack Skill { get { return skill; } set { skill = value; } }

    [Space(10)]
    private Image skillIcon;
    public Image SkillIcon { get { return skillIcon; } set { skillIcon = value; } }
    #endregion

    #region 위치값
    private RectTransform rect;
    private Transform originsPos;
    #endregion

    [Space(10)]
    #region 부모 버튼 속성
    private SkillButton skillButton;
    public SkillButton SkillButton { get { return skillButton; } set { skillButton = value; } }
    #endregion

    [SerializeField] SkillBtnManager sBM;
    #region 캔바스 변수
    Transform canvas;
    CanvasGroup canvasGroup;
    #endregion

    private void Awake()
    {
        sBM = GetComponentInParent<SkillBtnManager>();
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
        if (skillButton.Skill == null || skillButton.CoolTime > 0)
            return;

        // 캔바스를 부모오브젝트로 설정
        transform.SetParent(canvas);
        // 자식오브젝트들 중 최하단으로 위치
        transform.SetAsLastSibling();

        skill = skillButton.Skill;
        skillIcon.sprite = skillButton.Skill.sprite;

        canvasGroup.blocksRaycasts = false;
    }
    #endregion

    #region 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        if (skillButton.Skill == null || skillButton.CoolTime > 0)
            return;

        rect.transform.position = eventData.position;
    }
    #endregion

    #region 드래그 끝날때
    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas)
        {
            Debug.Log("부모 오브젝트가 캔버스 임");
            skill = null;
            skillIcon.sprite = null;

            skillButton.Skill = null;
            skillButton.SkillIcon.sprite = null;

            SetParent();
        }

        canvasGroup.blocksRaycasts = true;

        if (skillButton.CoolTime > 0)
            return;

        // 부모 오브젝트가 캔버스 와 같을때
        sBM.SaveSkillData();
    }
    #endregion
    public void SetParent()
    {
        rect.position = originsPos.position;
        transform.SetParent(originsPos);
        transform.SetAsFirstSibling();
    }
}
