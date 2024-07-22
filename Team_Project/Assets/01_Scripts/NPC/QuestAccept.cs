using UnityEngine;

public class QuestAccept : MonoBehaviour
{
    public ShowQuest showQ;

    public void OnClicked()
    {
        showQ.AcceptBtnOnClicked();
        Debug.Log(1);
    }
}
