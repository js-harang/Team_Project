using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// ��ư�� �־����� ��ũ��Ʈ
public class DropSkillOld : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    // �ܺο��� SkillData ������Ʈ �� �־���
    public SkillDataOld skillData;

    [SerializeField] private GameObject childObj;
    [SerializeField] private SkillButtonOld skillButton;

    private void Awake()
    {
        Debug.Log(this.name);
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
        if (eventData.pointerDrag.GetComponent<SkillDataOld>())
        {
            Debug.Log("SkillData ���");
            SkillDataOld eventSkillData = eventData.pointerDrag.GetComponent<SkillDataOld>();
            if (eventSkillData.Button.CoolTime <= 0)
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
        else if (eventData.pointerDrag.GetComponent<SkillSlotOld>())
        {
            Debug.Log("SkillSlot ���");
            SkillSlotOld eventSkillData = eventData.pointerDrag.GetComponent<SkillSlotOld>();

            skillData.Skill = eventSkillData.Skill;
            skillData.SkillIcon.sprite = eventSkillData.SkillIcon.sprite;
        }
        #endregion
    }

    // �����Ͱ� ���ö�
    public void OnPointerEnter(PointerEventData eventData) { }

    // �����Ͱ� ������
    public void OnPointerExit(PointerEventData eventData) { }

    // ��ų ����
    void SaveSKillData()
    {
        if (skillData.Skill != null)
        {
            PlayerPrefs.SetInt(this.name,skillData.Skill.aST.atkNum);
        }
        PlayerPrefs.Save();
    }
    void LoadSkillData()
    {
        int idx = PlayerPrefs.GetInt(this.name);

    }
}
