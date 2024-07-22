using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    // ���� ���� ���� �ִ� ����Ʈ��
    List<QuestData> myQuests = new List<QuestData>();
    public List<QuestData> MyQuests
    {
        get { return myQuests; }
        set
        {
            myQuests = value;
            MyQuestWindowUpdate();
        }
    }

    // ���� �÷��̾ ���� �ִ� ����Ʈ���� ����Ʈ UI
    [SerializeField]
    GameObject MyQuestListUI;

    // ������ ������ ����Ʈ��
    public int[] doneQuestID;
    // ������ ���� ����Ʈ��
    public int[] finQuestID;

    void MyQuestWindowUpdate()
    {

    }
}
