using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CreateSceneController : MonoBehaviour
{
    [SerializeField] GameObject createPopup;
    bool isCreateActive = false;

    [SerializeField, Space(10)] TMP_InputField unitName;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI infoTxt;

    [SerializeField, Space(10)] GameObject[] Illusts;

    int unitIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isCreateActive)
                SceneManager.LoadScene("01_CharacterLobby");
        }
    }

    public void SelectNum(int num)
    {
        unitIndex = num;

        foreach (GameObject obj in Illusts)
            obj.SetActive(false);

        Illusts[num].SetActive(true);
    }

    public void SelectBtn()
    {
        if (unitIndex != 0)
            return;

        isCreateActive = true;
        createPopup.SetActive(isCreateActive);
    }

    public void CreateBtn()
    {
        createPopup.SetActive(true);

        if (unitName.text == "")
        {
            Debug.Log("이름 입력하세요");
            return;
        }

        StartCoroutine(SaveDataPost());
    }

    IEnumerator SaveDataPost()
    {
        string url = GameManager.gm.path + "create_character.php";
        WWWForm form = new();
        Debug.Log(GameManager.gm.Uid);
        form.AddField("uid", GameManager.gm.Uid);
        form.AddField("slot", GameManager.gm.slotNum);
        form.AddField("name", unitName.text);
        form.AddField("class", unitIndex);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                switch (www.downloadHandler.text)
                {
                    case "success":
                        GameManager.gm.slotNum = -1;
                        SceneManager.LoadScene("01_CharacterLobby");
                        break;
                    case "name exists":
                        Debug.Log("중복된 닉네임");
                        break;
                    default:
                        Debug.Log("알 수 없는 오류가 발생했습니다.");
                        break;
                }
            }
        }
    }

    public void CancelBtn()
    {
        isCreateActive = !isCreateActive;
        createPopup.SetActive(isCreateActive);
    }

    public void BackBtn()
    {
        GameManager.gm.slotNum = -1;
        SceneManager.LoadScene("01_CharacterLobby");
    }
}
