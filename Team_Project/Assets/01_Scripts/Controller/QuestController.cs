using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    // ���� ���� ���� �ִ� ����Ʈ��
    QuestData[] myQuests;
    public QuestData[] MyQuests 
    { 
        get { return myQuests; } 
        set
        {

        }
    }
    // ������ ������ ����Ʈ��
    public int[] doneQuestID;
    // ������ ���� ����Ʈ��
    public int[] finQuestID;

    [SerializeField]
    GameObject QuestListPref;
    [SerializeField]
    GameObject questWindow;
    [SerializeField]
    GameObject questInfo;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void QuestWindowOpen()
    {
        questWindow.SetActive(true);
    }

    public void QUestInforOpen()
    {

    }
}
