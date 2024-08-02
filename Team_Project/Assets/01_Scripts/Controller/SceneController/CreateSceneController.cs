using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CreateSceneController : MonoBehaviour
{
    [SerializeField] GameObject createPopup;
    bool isCreateActive = false;

    [SerializeField] GameObject popup;
    bool isPopupActive = false;
    [SerializeField] TextMeshProUGUI popupTxt;

    [SerializeField] GameObject checkPopup;
    bool isCheckActive = false;
    [SerializeField] TextMeshProUGUI checkTxt;

    [SerializeField, Space(10)] TMP_InputField unitName;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI infoTxt;

    [SerializeField, Space(10)] GameObject[] Illusts;

    int unitIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isCheckActive)
                CheckBtn();
            else if (isPopupActive)
                CreateCancleBtn();
            else if (isCreateActive)
                CancleBtn();
            else
                BackBtn();
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
        isCreateActive = true;
        createPopup.SetActive(isCreateActive);
    }

    public void CreateBtn()
    {
        if (unitName.text == "")
        {
            checkTxt.text = "�г����� �Է����ּ���";

            isCreateActive = false;
            createPopup.SetActive(isCreateActive);
            isCheckActive = true;
            checkPopup.SetActive(isCheckActive);

            return;
        }

        if (unitName.text.Length > 12)
        {
            checkTxt.text = "�г����� 12���ڱ��� �����մϴ�.";

            isCreateActive = false;
            createPopup.SetActive(isCreateActive);
            isCheckActive = true;
            checkPopup.SetActive(isCheckActive);

            return;
        }

        popupTxt.text = unitName.text + "(��)�� �����Ͻðڽ��ϱ�?";
        isCreateActive = false;
        createPopup.SetActive(isCreateActive);
        isPopupActive = true;
        popup.SetActive(isPopupActive);
    }

    public void CancleBtn()
    {
        isCreateActive = false;
        createPopup.SetActive(isCreateActive);
    }

    public void CheckBtn()
    {
        isCheckActive = false;
        checkPopup.SetActive(isCheckActive);
        isCreateActive = true;
        createPopup.SetActive(isCreateActive);
    }

    public void CreateCheckBtn()
    {
        StartCoroutine(SaveDataPost());
    }

    public void CreateCancleBtn()
    {
        isPopupActive = false;
        popup.SetActive(isPopupActive);
        isCreateActive = true;
        createPopup.SetActive(isCreateActive);
    }

    IEnumerator SaveDataPost()
    {
        string url = GameManager.gm.path + "create_character.php";
        WWWForm form = new();
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
                    case "y":
                        GameManager.gm.slotNum = -1;
                        SceneManager.LoadScene("01_CharacterLobby");
                        break;
                    case "n":
                        checkTxt.text = "�ߺ��� �г����Դϴ�.";
                        isPopupActive = false;
                        popup.SetActive(isPopupActive);
                        isCheckActive = true;
                        checkPopup.SetActive(isCheckActive);
                        break;
                    default:
                        checkTxt.text = "�� �� ���� ������ �߻��Ͽ����ϴ�.";
                        isPopupActive = false;
                        popup.SetActive(isPopupActive);
                        isCheckActive = true;
                        checkPopup.SetActive(isCheckActive);
                        break;
                }
            }
        }
    }

    public void BackBtn()
    {
        GameManager.gm.slotNum = -1;
        SceneManager.LoadScene("01_CharacterLobby");
    }
}
