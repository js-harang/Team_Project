using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public enum Network
{
    Online,
    Offline,
}

public class TitleController : MonoBehaviour
{
    [SerializeField] Network network;

    [SerializeField, Space(10)] TMP_InputField id;
    [SerializeField] TMP_InputField pw;
    [SerializeField] TextMeshProUGUI loginResultTxt;
    [SerializeField] TextMeshProUGUI idPlaceholder;
    [SerializeField] TextMeshProUGUI pwPlaceholder;

    [SerializeField, Space(10)] GameObject preferencePopup;
    [SerializeField] GameObject exitPopup;
    bool isExitActive = false;

    private void Start()
    {
        idPlaceholder.DOFade(0.1f, 1f).SetLoops(-1, LoopType.Yoyo);
        pwPlaceholder.DOFade(0.1f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.gm.isPreferencePopup)
                ExitGamePopup();
        }
    }

    public void LoginBtn()
    {
        if (!CheckInput(id.text, pw.text))
            return;

        switch (network)
        {
            case Network.Online:
                StartCoroutine(LoginDataPost(id.text, pw.text));
                break;
            case Network.Offline:
                LoginDataPlayerPrefs();
                break;
        }
    }

    public void SaveDataBtn()
    {
        if (!CheckInput(id.text, pw.text))
            return;

        if (id.text.Length > 20)
        {
            loginResultTxt.text = "���̵�� 20�ڱ��� �Է��� �� �ֽ��ϴ�.";
            return;
        }

        switch (network)
        {
            case Network.Online:
                StartCoroutine(SaveDataPost(id.text, pw.text));
                break;
            case Network.Offline:
                SaveDataPlayerPrefs();
                break;
        }
    }

    #region �����ͺ��̽� �¶��ο�
    IEnumerator LoginDataPost(string id, string pw)
    {
        string url = GameManager.gm.path + "login.php";
        WWWForm form = new();
        form.AddField("userid", id);
        form.AddField("userpw", pw);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                string[] result = www.downloadHandler.text.Split(" ");

                switch (result[0])
                {
                    case "y":
                        GameManager.gm.Uid = result[1];
                        GameManager.gm.sceneNumber = 1;
                        SceneManager.LoadScene("99_LoadingScene");
                        break;
                    case "n":
                        loginResultTxt.text = "�������� ���� ���̵��̰ų� �߸��� ��й�ȣ�Դϴ�.";
                        break;
                    default:
                        loginResultTxt.text = "�� �� ���� ������ �߻��߽��ϴ�.";
                        break;
                }
            }
        }
    }

    IEnumerator SaveDataPost(string id, string pw)
    {
        string url = GameManager.gm.path + "create.php";
        WWWForm form = new();
        form.AddField("userid", id);
        form.AddField("userpw", pw);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                loginResultTxt.text = www.downloadHandler.text switch
                {
                    "y" => "���̵� ������ �Ϸ�ƽ��ϴ�.",
                    "n" => "�̹� �����ϴ� ���̵��Դϴ�.",
                    _ => "�� �� ���� ������ �߻��߽��ϴ�.",
                };
            }
        }
    }
    #endregion

    #region �����ͺ��̽� �������ο�
    private void LoginDataPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey(id.text))
        {
            loginResultTxt.text = "�Է��Ͻ� ���̵�� �н����尡 ��ġ���� �ʽ��ϴ�.";
            return;
        }

        if (pw.text == PlayerPrefs.GetString(id.text))
        {
            GameManager.gm.sceneNumber = 1;
            SceneManager.LoadScene("99_LoadingScene");
        }
        else
            loginResultTxt.text = "�Է��Ͻ� ���̵�� �н����尡 ��ġ���� �ʽ��ϴ�.";
    }

    private void SaveDataPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey(id.text))
        {
            PlayerPrefs.SetString(id.text, pw.text);
            loginResultTxt.text = "���̵� ������ �Ϸ�ƽ��ϴ�.";
        }
        else
            loginResultTxt.text = "�̹� �����ϴ� ���̵��Դϴ�.";
    }
    #endregion

    private bool CheckInput(string id, string pw)
    {
        if (id == "" || pw == "")
        {
            loginResultTxt.text = "���̵� �Ǵ� �н����带 �Է����ּ���.";
            return false;
        }
        else
            return true;
    }

    public void PreferencesBtn()
    {
        GameManager.gm.isPreferencePopup = true;
        preferencePopup.SetActive(GameManager.gm.isPreferencePopup);
    }

    public void ExitGamePopup()
    {
        isExitActive = !isExitActive;
        exitPopup.SetActive(isExitActive);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
