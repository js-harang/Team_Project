using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class QuestGiver : MonoBehaviour
{
    QuestController questCon;
    InteractProperty interPP;
    QuestsLoad qLoad;
    public List<QuestData> questList = new List<QuestData>();

    public GameObject questHave;
    public GameObject questDone;

    int myQuestCount;
    public int MyQuestCount
    { get { return myQuestCount; } set {myQuestCount = value; QuestMarkCheck(); } }

    private void Awake()
    {
        interPP = GetComponentInChildren<InteractProperty>();
        questCon = FindObjectOfType<QuestController>().GetComponent<QuestController>();
        qLoad = questCon.qLoad;
        qLoad.questDic.Add(interPP.InteractId, questList);
    }

    private void Update()
    {
        if (myQuestCount == questList.Count)
            return;

        MyQuestCount = questList.Count;
    }

    // 플레이어가 자신의 퀘스트를 얼마나 수주했는지 확인
    public void QuestMarkCheck()
    {
        StartCoroutine(HowManyHave());
    }

    IEnumerator HowManyHave()
    {
        int count = 0;

        string url = GameManager.gm.path + "search_playerquestid.php";
        string cuid = PlayerPrefs.GetString("characteruid");
        WWWForm form = new WWWForm();
        form.AddField("cuid", 0000000004);

        for (int i = 0; i < questList.Count; i++)
        {
            form.AddField("questID", questList[i].questID);

            using (UnityWebRequest www = UnityWebRequest.Post(url, form))
            {
                yield return www.SendWebRequest();

                if (www.error == null)
                {
                    Debug.Log(www.downloadHandler.text);
                    if (www.downloadHandler.text == "")
                        continue;

                    count++;
                    Debug.Log(count);
                }
            }
        }

        if (count >= questList.Count)
            questHave.SetActive(false);
        else
            questHave.SetActive(true);
    }

    // 플레이어가 말을 걸었을 때 현재 퀘스트 목표가 자신인지 확인하는 메서드
    public void HereForTalkToMe()
    {
        if (questCon.myQuests.Count == 0)
            return;

        for (int i = 0; i < questCon.myQuests.Count; i++)
        {
            if (questCon.myQuests[i].targetID != (int)interPP.myID 
                || questCon.myQuests[i].questType != QuestType.Conversation)
                return;

            questCon.myQuests[i].isDone = true;
            if (!AlreadyHaveCheck(questCon.myQuests[i]))
                questList.Add(questCon.myQuests[i]);
        }
    }

    // 플레이어가 진행 중인 퀘스트와 내 퀘스트 목록의 퀘스트가 겹치면 해당 퀘스트의 진행사항을 가져옴
    public void GetPlayerDoingQuest()
    {
        if (questCon.myQuests.Count == 0)
            return;

        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < questCon.myQuests.Count; j++)
            {
                if (questList[i].questID == questCon.myQuests[j].questID)
                    questList[i] = questCon.myQuests[j];
            }
        }
    }

    // 퀘스트 중복 검사
    bool AlreadyHaveCheck(QuestData quest)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].questID == quest.questID)
            {
                return true;
            }
        }
        return false;
    }
}
