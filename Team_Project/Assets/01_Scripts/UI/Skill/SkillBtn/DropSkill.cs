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

    private void Awake()
    {
        skillButton = GetComponent<SkillButton>();
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
            Debug.Log("SkillData ���");
            SkillData eventSkillData = eventData.pointerDrag.GetComponent<SkillData>();

            if (eventSkillData.SkillButton.CoolTime <= 0)
            {
                skillData.SkillIcon.sprite = eventSkillData.SkillIcon.sprite;
                skillData.Skill = eventSkillData.Skill;

                eventSkillData.Skill = null;
                eventSkillData.SkillIcon.sprite = null;
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

            SkillSlot eventSkillData = eventData.pointerDrag.GetComponent<SkillSlot>();

            skillData.Skill = eventSkillData.Skill;
            skillButton.Skill = eventSkillData.Skill;
            skillData.SkillIcon.sprite = eventSkillData.SkillIcon.sprite;
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
