using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    // 현재 내가 갖고 있는 퀘스트들
    QuestData[] myQuests;
    public QuestData[] MyQuests 
    { 
        get { return myQuests; } 
        set
        {

        }
    }
    // 조건을 충족한 퀘스트들
    public int[] doneQuestID;
    // 완전히 끝낸 퀘스트들
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
