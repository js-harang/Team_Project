using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    // ���� ���� ���� �ִ� ����Ʈ��
    List<QuestData> myQuests = new List<QuestData>();
    public List<QuestData> MyQuests { get { return myQuests; } }

    // ���� �÷��̾ ���� �ִ� ����Ʈ���� ����Ʈ UI
    [SerializeField]
    GameObject MyQuestListPref;
    [SerializeField]
    Transform questUIContent;

    // ������ ������ ����Ʈ��
    public int[] doneQuestID;
    // ������ ���� ����Ʈ��
    public int[] finQuestID;

    private void Awake()
    {
        StartCoroutine(LoadPlayerQuestsHave(PlayerPrefs.GetInt("characteruid")));
    }

    public void MyQuestWindowUpdate(QuestData newQuest)
    {
        myQuests.Add(newQuest);
        Transform questList = Instantiate(MyQuestListPref).transform;
        QuestGoalList questGoal = questList.gameObject.GetComponent<QuestGoalList>();
        questList.transform.SetParent(questUIContent);
        questGoal.myData = myQuests[myQuests.Count - 1];
        questGoal.CreateQuestList();
    }

    IEnumerator LoadPlayerQuestsHave(int characterID)
    {
        string url = GameManager.gm.path + "load_playerquestdata.php";
        WWWForm form = new WWWForm();
        form.AddField("cuid", characterID);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {

            }
        }
    }
}
