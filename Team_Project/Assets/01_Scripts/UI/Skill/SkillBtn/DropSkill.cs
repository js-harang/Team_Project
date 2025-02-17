using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 버튼에 넣어지는 스크립트
public class DropSkill : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    // 자식 SkillData 오브젝트 가 넣어짐
    public SkillData skillData;
    [SerializeField] private GameObject childObj;
    
    [Space(10)]
    private SkillButton skillButton;

    SkillBtnManager sBM;
    private void Awake()
    {
        skillButton = GetComponent<SkillButton>();
        sBM = GetComponentInParent<SkillBtnManager>();
    }

    // 무언가 드랍 될때
    public void OnDrop(PointerEventData eventData)
    {
        if (skillButton.CoolTime > 0)
        {
            Debug.Log("나 아직 쿨임 ㅇㅇ");
            return;
        }

        #region 원래 스킬 버튼위치에 그대로 놓으면 리턴
        if (childObj == eventData.pointerDrag)
        {
            Debug.Log("내 자식임 ㅇㅇ");
            SkillData dragSkillData = eventData.pointerDrag.GetComponent<SkillData>();
            dragSkillData.SetParent();
            return;
        }
        #endregion

        QuickToQuick(eventData);
        SlotToQuick(eventData);
    }

    #region 퀵슬롯 에서 퀵슬롯 으로 옮길때
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
                skillButton.btnSkillIdx = dragSkillData.SkillButton.btnSkillIdx;

                dragSkillData.Skill = null;
                dragSkillData.SkillIcon.sprite = null;

                dragSkillData.SkillButton.Skill = null;
                dragSkillData.SkillButton.SkillIcon.sprite = null;
                dragSkillData.SkillButton.btnSkillIdx = 0;

                skillButton.SetSkillData();
                sBM.SaveSkillData();
            }
            else
                return;

        }
    }
    #endregion

    #region 스킬창 에서 퀵슬롯으로 옮길때
    void SlotToQuick(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<SkillSlot>())
        {
            SkillSlot dragSkillData = eventData.pointerDrag.GetComponent<SkillSlot>();

            if (sBM.CheckSkillDuplication(dragSkillData.Skill.skillIdx))
                return;

            skillButton.Skill = dragSkillData.Skill;
            skillButton.SkillIcon.sprite = dragSkillData.Skill.sprite;
            skillButton.btnSkillIdx = dragSkillData.Skill.skillIdx;

            skillButton.SetSkillData();
            sBM.SaveSkillData();
        }
    }
    #endregion

    #region 안쓰는 함수
    // 포인터가 들어올때
    public void OnPointerEnter(PointerEventData eventData) { }

    // 포인터가 나갈때
    public void OnPointerExit(PointerEventData eventData) { }
    #endregion
}
