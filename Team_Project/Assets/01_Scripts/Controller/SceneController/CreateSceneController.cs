using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CreateSceneController : MonoBehaviour
{
    [SerializeField] GameObject createPopup;
    bool isCreateActive = false;

    [SerializeField, Space(10)] TMP_InputField characterName;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI infoTxt;

    [SerializeField, Space(10)] GameObject[] Illusts;

    int characterIndex = 0;

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
        characterIndex = num;

        foreach (GameObject obj in Illusts)
            obj.SetActive(false);

        Illusts[num].SetActive(true);
    }

    public void SelectBtn()
    {
        isCreateActive = true;
        createPopup.SetActive(isCreateActive);
    }

    public void CreateBtn()
    {
        createPopup.SetActive(true);
        if (characterName.text == "")
        {
            Debug.Log("�̸� �Է��ϼ���");
            return;
        }

        StartCoroutine(SaveDataPost());
    }

    IEnumerator SaveDataPost()
    {
        string url = GameManager.gm.path + "create_character.php";
        WWWForm form = new();
        form.AddField("uid", PlayerPrefs.GetString("uid"));
        form.AddField("slot", GameManager.gm.slotNum);
        form.AddField("name", characterName.text);
        form.AddField("class", characterIndex);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                switch (www.downloadHandler.text)
                {
                    case "success":
                        Debug.Log("ĳ���� ���� ����");
                        SceneManager.LoadScene("01_CharacterLobby");
                        break;
                    case "name exists":
                        Debug.Log("�ߺ��� �г���");
                        break;
                    default:
                        Debug.Log("�� �� ���� ������ �߻��߽��ϴ�.");
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
        SceneManager.LoadScene("01_CharacterLobby");
    }
}
