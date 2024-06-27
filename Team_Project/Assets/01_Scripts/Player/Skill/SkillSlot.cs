using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// ��ųâ���� ������� ��ų
public class SkillSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region ��ų ������ �Ӽ�
    [SerializeField] private Image skillIcon;
    public Image SkillIcon { get { return skillIcon; } set { skillIcon = value; } }
    #endregion

    #region ��ų �Ӽ�
    [SerializeField] private PlayerAttack skill;
    public PlayerAttack Skill { get { return skill; } set { skill = value; } }
    #endregion

    #region ��ġ ����
    [SerializeField] private Transform originPos;
    RectTransform rect;
    #endregion

    #region ĵ�ٽ� ����
    [SerializeField]
    Transform canvas;
    CanvasGroup canvasGroup;
    #endregion

    private void Awake()
    {
        StartSetting();
    }

    #region �巡�� ���� �� ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas);            // �θ� ������Ʈ�� canvas �� ����
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }
    #endregion

    #region �巡�� �� �϶�
    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }
    #endregion

    #region �巡�� ������
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
        skill = GetComponent<PlayerAttack>();
        skillIcon = GetComponent<Image>();

        canvas = GetComponentInParent<Canvas>().transform;
        canvasGroup = GetComponent<CanvasGroup>();

        originPos = transform.parent;
        rect = GetComponent<RectTransform>();
    }
    #endregion
}
