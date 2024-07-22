using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ��ư ĭ�� �ִ� ��ư��ų
public class SkillData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region ��ų �Ӽ�
    [SerializeField] private Attack skill;
    public Attack Skill { get { return skill; } set { skill = value; } }

    [Space(10)]
    private Image skillIcon;
    public Image SkillIcon { get { return skillIcon; } set { skillIcon = value; } }
    #endregion

    #region ��ġ��
    private RectTransform rect;
    private Transform originsPos;
    #endregion

    [Space(10)]
    #region �θ� ��ư �Ӽ�
    private SkillButton skillButton;
    public SkillButton SkillButton { get { return skillButton; } set { skillButton = value; } }
    #endregion

    [SerializeField] SkillBtnManager sBM;
    #region ĵ�ٽ� ����
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

    #region �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (skillButton.Skill == null || skillButton.CoolTime > 0)
            return;

        // ĵ�ٽ��� �θ������Ʈ�� ����
        transform.SetParent(canvas);
        // �ڽĿ�����Ʈ�� �� ���ϴ����� ��ġ
        transform.SetAsLastSibling();

        skill = skillButton.Skill;
        skillIcon.sprite = skillButton.Skill.sprite;

        canvasGroup.blocksRaycasts = false;
    }
    #endregion

    #region �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        if (skillButton.Skill == null || skillButton.CoolTime > 0)
            return;

        rect.transform.position = eventData.position;
    }
    #endregion

    #region �巡�� ������
    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas)
        {
            Debug.Log("�θ� ������Ʈ�� ĵ���� ��");
            skill = null;
            skillIcon.sprite = null;

            skillButton.Skill = null;
            skillButton.SkillIcon.sprite = null;

            SetParent();
        }

        canvasGroup.blocksRaycasts = true;

        if (skillButton.CoolTime > 0)
            return;

        // �θ� ������Ʈ�� ĵ���� �� ������
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
