using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAbleUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Transform canvas;           // UI �� �ҼӵǾ��ִ� �ֻ�� Canvas
    [SerializeField]
    private CanvasGroup canvasGroup;    // UI ���İ� �� ��ȣ�ۿ� ��� ���� CanvasGroup
    [Space(10)]
    [SerializeField]
    private RectTransform rect;         // UI ��ġ ��� ���� RectTransform
    [SerializeField]
    private Transform previousParent;   // �ش� ������Ʈ ������ �ҼӵǾ� �ִ� �θ� ������Ʈ�� Transform

    [Space(10)]
    // ������Ʈ�� ��ų ����
    public PlayerAttack skill;

    [Space(10)]
    // ��ư�� ���� ��ų
    [SerializeField]
    SkillButton previousButton;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
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

        if (previousParent.GetComponent<SkillButton>())
        {
            Debug.Log("��ư ���");
            previousButton = previousParent.GetComponent<SkillButton>();

            // ���� ��ư�� ��ų�� ��Ÿ�� �϶�
            if (previousButton.CoolTime > 0)
            {
                transform.SetParent(previousParent);
                ClickAble();
                Debug.Log("��ų�� ��Ÿ�� �϶��� �����Ҽ� �����ϴ�.");
            }
        }
    }

    // ���� ������Ʈ �� �巡�� �� �϶� �� ������ ȣ��
    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;     // ���� ��ũ���� �� ���콺 ��ġ�� UI �� ��ġ�� ��(UI �� ���콺�� �i�ƴٴϴ� ����)
    }

    public void OnEndDrag(PointerEventData eventData)   // ���� ������Ʈ�� �巡�� �� ���� �� �� 1 ȸ ȣ��
    {
        // �巡�� ���� ������ Canvas �̸�
        if (transform.parent == canvas)
        {
            Destroy(gameObject);
            return;
        }

        // ���� ��ư�� ������
        if (previousButton != null)
        {
            // ���� ��ư�� ��ų�� ��Ÿ�� �϶�
            if (previousButton.CoolTime > 0)
            {
                transform.SetParent(previousParent);
                rect.transform.position = previousParent.GetComponent<RectTransform>().position;
                return;
            }
        }

        // Ŭ�� Ȱ��ȭ
        ClickAble();


        // ���� ��ųĭ ��ų ����
        if (previousParent.GetComponent<SkillButton>())     // ���� �θ������Ʈ�� SkillButton �̸� ��ų ����
        {
            Debug.Log("��ų ����");
            previousButton = previousParent.GetComponent<SkillButton>();
            previousButton.skill = null;
        }
        // ���� ��ųĭ ��ų ���
        if (transform.parent.GetComponent<SkillButton>())
        {
            Debug.Log("��ų ���");
            previousButton = transform.parent.GetComponent<SkillButton>();
            previousButton.skill = skill;
            previousButton.transform.SetAsLastSibling();
        }

        transform.SetAsLastSibling();
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
}
