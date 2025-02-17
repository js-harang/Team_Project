using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 버튼에 넣어지는 스크립트
public class DropSkillOld : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    // 외부에서 SkillData 오브젝트 가 넣어짐
    public SkillDataOld skillData;

    [SerializeField] private GameObject childObj;
    [SerializeField] private SkillButtonOld skillButton;

    private void Awake()
    {
        Debug.Log(this.name);
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
        if (eventData.pointerDrag.GetComponent<SkillDataOld>())
        {
            Debug.Log("SkillData 드롭");
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
                Debug.Log("님 아직 쿨임 ㅇㅇ");
            }
        }
        #endregion

        #region 스킬창 에서 퀵슬롯으로 옮길때
        else if (eventData.pointerDrag.GetComponent<SkillSlotOld>())
        {
            Debug.Log("SkillSlot 드롭");
            SkillSlotOld eventSkillData = eventData.pointerDrag.GetComponent<SkillSlotOld>();

            skillData.Skill = eventSkillData.Skill;
            skillData.SkillIcon.sprite = eventSkillData.SkillIcon.sprite;
        }
        #endregion
    }

    // 포인터가 들어올때
    public void OnPointerEnter(PointerEventData eventData) { }

    // 포인터가 나갈때
    public void OnPointerExit(PointerEventData eventData) { }

    // 스킬 저장
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
