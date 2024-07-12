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
            return;
        }
        #endregion

        #region ������ ���� ������ ���� �ű涧
        if (eventData.pointerDrag.GetComponent<SkillData>())
        {
            SkillData dragSkillData = eventData.pointerDrag.GetComponent<SkillData>();

            if (dragSkillData.Skill == null)
                return;

            if (dragSkillData.SkillButton.CoolTime <= 0)
            {
                skillData.SkillIcon.sprite = dragSkillData.SkillIcon.sprite;
                skillData.Skill = dragSkillData.Skill;

                dragSkillData.Skill = null;
                dragSkillData.SkillIcon.sprite = null;
                dragSkillData.SkillButton.Skill = null;

                if (chkDup.CheckSkillDuplication(skillData.Skill.skillIdx))
                {
                    Debug.Log("�ߺ� �߰� " + this.name);
                    return;
                }
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
            Debug.Log("asdasd");
            skillData.Skill = dragSkillData.Skill;
            skillButton.Skill = dragSkillData.Skill;
            skillData.SkillIcon.sprite = dragSkillData.SkillIcon.sprite;
        }
        #endregion
        skillButton.SaveSkillData();
    }

    #region �Ⱦ��� �Լ�
    // �����Ͱ� ���ö�
    public void OnPointerEnter(PointerEventData eventData) { }

    // �����Ͱ� ������
    public void OnPointerExit(PointerEventData eventData) { }
    #endregion
}
