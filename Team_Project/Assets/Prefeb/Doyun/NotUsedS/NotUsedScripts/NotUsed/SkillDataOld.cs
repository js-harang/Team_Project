using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ��ư ĭ�� �ִ� ��ư��ų
public class SkillDataOld : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region ��ų ������ �Ӽ�
    private Image skillIcon;
    public Image SkillIcon { get { return skillIcon; } set { skillIcon = value; } }
    #endregion

    #region ��ų �Ӽ�
    private PlayerAttackOld skill;
    public PlayerAttackOld Skill { get { return skill; } set { skill = value; } }
    #endregion

    [Space(10)]
    #region ��ġ��
    private RectTransform rect;
    private Transform originsPos;
    #endregion

    [Space(10)]
    #region �θ� ��ư �Ӽ�
    [SerializeField] private SkillButtonOld skillButton;
    public SkillButtonOld Button { get { return skillButton; } set { skillButton = value; } }
    #endregion

    #region ĵ�ٽ� ����
    Transform canvas;
    CanvasGroup canvasGroup;
    #endregion

    private void Awake()
    {
        skillButton = GetComponentInParent<SkillButtonOld>();
        skillIcon = GetComponent<Image>();

        rect = GetComponent<RectTransform>();
        originsPos = transform.parent;

        canvas = GetComponentInParent<Canvas>().transform;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    #region �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }
    #endregion

    #region �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        rect.transform.position = eventData.position;
    }
    #endregion

    #region �巡�� ������
    public void OnEndDrag(PointerEventData eventData)
    {
        rect.position = originsPos.position;
        canvasGroup.blocksRaycasts = true;

        if (skillButton.CoolTime > 0)
            return;

        if (transform.parent != canvas)
        {
            skill = null;
            skillIcon.sprite = null;
            skillButton.Skill = null;
        }
    }
    #endregion
}