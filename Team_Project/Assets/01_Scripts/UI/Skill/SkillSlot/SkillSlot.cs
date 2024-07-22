using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// ��ųâ���� ������� ��ų
public class SkillSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public SkillDirectory skillPannel;

    [SerializeField] int skillIndex;

    [Space(10)]
    #region ��ų ������ �Ӽ�
    [SerializeField] private Image skillIcon;
    public Image SkillIcon { get { return skillIcon; } set { skillIcon = value; } }
    #endregion

    #region ��ų �Ӽ�
    [SerializeField] private Attack skill;
    public Attack Skill { get { return skill; } set { skill = value; } }
    #endregion

    [Space(10)]
    #region ��ġ ����
    [SerializeField] private Transform originPos;
    RectTransform rect;
    #endregion

    [Space(10)]
    #region ĵ�ٽ� ����
    [SerializeField]
    Transform canvas;
    CanvasGroup canvasGroup;
    #endregion

    private void Awake()
    {
        StartSetting();
    }

    #region �巡�� ���� �� �� ĵ�ٽ� �θ���
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas);            // �θ� ������Ʈ�� canvas �� ����
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }
    #endregion

    #region �巡�� �� �϶� ���� ���콺 ��ġ�� ��ų ��ġ
    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }
    #endregion

    #region �巡�� ������ ���� ��ġ�� ���ư�
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originPos);
        rect.position = originPos.position;
        canvasGroup.blocksRaycasts = true;
    }
    #endregion

    #region ���� ����
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
            Debug.Log("�����");
            return;
        }

        skill = skillPannel.skillAtks[skillIndex];
        skillIcon.sprite = skillPannel.skillSprites[skillIndex];
    }
    #endregion
}
