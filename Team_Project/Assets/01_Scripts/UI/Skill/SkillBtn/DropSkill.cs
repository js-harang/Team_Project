using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// ��ư�� �־����� ��ũ��Ʈ
public class DropSkill : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    // �ڽ� SkillData ������Ʈ �� �־���
    public SkillData skillData;
    [SerializeField] private GameObject childObj;
    
    [Space(10)]
    private SkillButton skillButton;

    CheckDuplication chkDup;
    private void Awake()
    {
        skillButton = GetComponent<SkillButton>();
        chkDup = GetComponentInParent<CheckDuplication>();
    }

    // ���� ��� �ɶ�
    public void OnDrop(PointerEventData eventData)
    {
        if (skillButton.CoolTime > 0)
        {
            Debug.Log("�� ���� ���� ����");
            return;
        }

        #region ���� ��ų ��ư��ġ�� �״�� ������ ����
        if (childObj == eventData.pointerDrag)
        {
            Debug.Log("�� �ڽ��� ����");
            SkillData dragSkillData = eventData.pointerDrag.GetComponent<SkillData>();
            dragSkillData.SetParent();
            return;
        }
        #endregion

        QuickToQuick(eventData);
        SlotToQuick(eventData);
    }

    #region ������ ���� ������ ���� �ű涧
    void QuickToQuick(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<SkillData>())
        {
            SkillData dragSkillData = eventData.pointerDrag.GetComponent<SkillData>();

            dragSkillData.SetParent();

            if (dragSkillData.Skill == null)
                return;

            if (dragSkillData.SkillButton.CoolTime <= 0)
            {
                skillButton.Skill = dragSkillData.Skill;
                skillButton.SkillIcon.sprite = dragSkillData.SkillIcon.sprite;

                dragSkillData.Skill = null;
                dragSkillData.SkillIcon.sprite = null;
                dragSkillData.SkillButton.Skill = null;
                dragSkillData.SkillButton.SkillIcon.sprite = null;

                skillButton.SetSkillData();
                skillButton.SaveSkillData();
            }
            else
                return;

        }
    }
    #endregion

    #region ��ųâ ���� ���������� �ű涧
    void SlotToQuick(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<SkillSlot>())
        {
            SkillSlot dragSkillData = eventData.pointerDrag.GetComponent<SkillSlot>();

            if (chkDup.CheckSkillDuplication(dragSkillData.Skill.skillIdx))
                return;

            skillButton.Skill = dragSkillData.Skill;
            skillButton.SkillIcon.sprite = dragSkillData.Skill.sprite;

            skillButton.SetSkillData();
            skillButton.SaveSkillData();
        }
    }
    #endregion

    #region �Ⱦ��� �Լ�
    // �����Ͱ� ���ö�
    public void OnPointerEnter(PointerEventData eventData) { }

    // �����Ͱ� ������
    public void OnPointerExit(PointerEventData eventData) { }

    void SetSkillButtonData(PointerEventData eventData) 
    {
        #region ������ ���� ������ ���� �ű涧
        if (eventData.pointerDrag.GetComponent<SkillData>())
        {
            SkillData dragSkillData = eventData.pointerDrag.GetComponent<SkillData>();

            if (dragSkillData.Skill == null)
                return;

            if (dragSkillData.SkillButton.CoolTime <= 0)
            {
                Debug.Log("�� to ��");
                skillButton.Skill = dragSkillData.Skill;
                skillButton.SkillIcon.sprite = dragSkillData.SkillIcon.sprite;

                dragSkillData.Skill = null;
                dragSkillData.SkillIcon.sprite = null;
                dragSkillData.SkillButton.Skill = null;
                dragSkillData.SkillButton.SkillIcon.sprite = null;

            }
            else
            {
                Debug.Log("�� ���� ���� ����");
            }
        }
        #endregion

        #region ��ųâ ���� ���������� �ű涧
        else if (eventData.pointerDrag.GetComponent<SkillSlot>())
        {
            Debug.Log("SkillSlot ���");

            SkillSlot dragSkillData = eventData.pointerDrag.GetComponent<SkillSlot>();

            if (chkDup.CheckSkillDuplication(dragSkillData.Skill.skillIdx))
            {
                Debug.Log("�ߺ� �߰� " + this.name);
                return;
            }

            Debug.Log("�ߺ� ����");

            skillButton.Skill = dragSkillData.Skill;
            skillButton.SkillIcon.sprite = dragSkillData.Skill.sprite;
        }
        #endregion
    }
    #endregion
}
