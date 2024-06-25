using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAbleUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvas;           // UI �� �ҼӵǾ��ִ� �ֻ�� Canvas
    private Transform previousParent;   // �ش� ������Ʈ ������ �ҼӵǾ� �ִ� �θ� ������Ʈ�� Transform
    private RectTransform rect;         // UI ��ġ ��� ���� RectTransform
    private CanvasGroup canvasGroup;    // UI ���İ� �� ��ȣ�ۿ� ��� ���� CanvasGroup

    // ������Ʈ�� ��ų ����
    public PlayerAttack skill;
    // ��ư�� ���� ��ų
    SkillButton currentSkill;
    // ��ư�� ���� ��ų
    SkillButton previousSkill;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (currentSkill == null)
            return;

        if (currentSkill.CoolTime > 0)
            ClickDisAble();
        else
            ClickAble();
    }

    // ���� ������Ʈ �� �巡�� �ϱ� ���� �� �� 1ȸ ȣ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�� ���� �ҼӵǾ� �ִ� �θ� ������Ʈ�� Transform �� ����
        previousParent = transform.parent;

        // ���� �巡�� ���� UI �� ȭ�� �ֻ�ܿ� ���̰� �ϱ� ����
        transform.SetParent(canvas);            // �θ� ������Ʈ�� canvas �� ����
        transform.SetAsLastSibling();           // ȭ�鿡 �� ���� ���̱� ���� canvas �� ���� �Ʒ� ���� �ڽĿ�����Ʈ �� ����

        // Ŭ�� ��Ȱ��ȭ
        ClickDisAble();
    }

    // ���� ������Ʈ �� �巡�� �� �϶� �� ������ ȣ��
    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;     // ���� ��ũ���� �� ���콺 ��ġ�� UI �� ��ġ�� ��(UI �� ���콺�� �i�ƴٴϴ� ����)
    }

    public void OnEndDrag(PointerEventData eventData)   // ���� ������Ʈ�� �巡�� �� ���� �� �� 1 ȸ ȣ��
    {
        // �巡�� ���� ������ Canvas �̸�
       /* if (transform.parent == canvas)
        {
            Destroy(gameObject);
            return;
        }*/

        // Ŭ�� Ȱ��ȭ
        ClickAble();

        // ���� ��ųĭ ��ų ����
        if (previousParent.GetComponent<SkillButton>())     // ���� �θ������Ʈ�� SkillButton �̸� ��ų ����
        {
            Debug.Log("��ų��ư ����");
            previousSkill = previousParent.GetComponent<SkillButton>();
            previousSkill.skill = null;
        }
        // ���� ��ųĭ ��ų ���
        if (transform.parent.GetComponent<SkillButton>())
        {
            Debug.Log("��ų��ư");
            currentSkill = transform.parent.GetComponent<SkillButton>();
            currentSkill.skill = skill;
        }
    }

    #region Ŭ�� Ȱ��,��Ȱ��

    // Ŭ�� Ȱ��ȭ
    void ClickAble()
    {
        // �ٽ� ������ �ϰ� ����� �浹 ó�� Ȱ��ȭ
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    // Ŭ�� ��Ȱ��ȭ
    void ClickDisAble()
    {
        // �巡�� �ϴ� ������Ʈ�� �ϳ��� �ƴ� �ڽ� ������Ʈ ���� ������ ������ �ֱ� ������ CanvasGroup ���� ����
        canvasGroup.alpha = 0.6f;  // ���� �巡�� ���� ������Ʈ ���İ� ���߱�
        canvasGroup.blocksRaycasts = false;  // ���Կ� ��� �ϱ����� ���콺 �� ������ �浹�Ҽ� �ֵ��� �巡���ϴ� ������Ʈ�� �浹 ó���� ��
    }

    #endregion

    // ��ų ��Ÿ�� Ȯ�ο� �żҵ�
    bool CheckPreviousSkillButtonCoolTime()
    {
        // �巡�� �Ϸ� �Ҷ� ��ų�� ��Ÿ���̸� ����
        if (previousParent != null && 
            previousParent.GetComponent<SkillButton>())
        {
            SkillButton previousSkill = previousParent.GetComponent<SkillButton>();
            if (previousSkill.CoolTime > 0)
                return true;
        }
        return false;
    }
}
