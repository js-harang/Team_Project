using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    // ���� ���� ���� �ִ� ����Ʈ��
    List<QuestData> myQuests;
    public List<QuestData> MyQuests 
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

    private void Start()
    {

    }

    private void Update()
    {
        
    }   
}
