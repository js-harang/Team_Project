using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    // 현재 내가 갖고 있는 퀘스트들
    List<QuestData> myQuests;
    public List<QuestData> MyQuests 
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

    private void Start()
    {

    }

    private void Update()
    {
        
    }   
}
