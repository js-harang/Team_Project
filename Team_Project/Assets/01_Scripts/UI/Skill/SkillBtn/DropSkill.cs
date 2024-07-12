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

    CheckDuplication chkDup;
    private void Awake()
    {
        skillButton = GetComponent<SkillButton>();
        chkDup = GetComponentInParent<CheckDuplication>();
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
            return;
        }
        #endregion

        #region 퀵슬롯 에서 퀵슬롯 으로 옮길때
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
                    Debug.Log("중복 발견 " + this.name);
                    return;
                }
            }
            else
            {
                Debug.Log("님 아직 쿨임 ㅇㅇ");
            }
        }
        #endregion

        #region 스킬창 에서 퀵슬롯으로 옮길때
        else if (eventData.pointerDrag.GetComponent<SkillSlot>())
        {
            Debug.Log("SkillSlot 드롭");
            SkillSlot dragSkillData = eventData.pointerDrag.GetComponent<SkillSlot>();

            if (chkDup.CheckSkillDuplication(dragSkillData.Skill.skillIdx))
            {
                Debug.Log("중복 발견 " + this.name);
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

    #region 안쓰는 함수
    // 포인터가 들어올때
    public void OnPointerEnter(PointerEventData eventData) { }

    // 포인터가 나갈때
    public void OnPointerExit(PointerEventData eventData) { }
    #endregion
}
