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
    bool isExitPopup = false;

    string path = "http://192.168.52.187/team_project/";

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
            loginResultTxt.text = "아이디는 20자까지 입력할 수 있습니다.";
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

    /// <summary>
    /// 데이터베이스 온라인용
    /// </summary>
    #region
    IEnumerator LoginDataPost(string id, string pw)
    {
        string url = path + "login.php";
        WWWForm form = new();
        form.AddField("userid", id);
        form.AddField("userpw", pw);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
                switch (www.downloadHandler.text)
                {
                    case "1":
                        GameManager.gm.sceneNumber = 1;
                        SceneManager.LoadScene("99_LoadingScene");
                        break;
                    case "2":
                        loginResultTxt.text = "가입하지 않은 아이디이거나 잘못된 비밀번호입니다.";
                        break;
                }
        }
    }

    IEnumerator SaveDataPost(string id, string pw)
    {
        string url = path + "create.php";
        WWWForm form = new();
        form.AddField("userid", id);
        form.AddField("userpw", pw);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
                switch (www.downloadHandler.text)
                {
                    case "1":
                        loginResultTxt.text = "아이디 생성이 완료됐습니다.";
                        break;
                    default:
                        loginResultTxt.text = "이미 존재하는 아이디입니다.";
                        break;
                }
        }
    }
    #endregion

    /// <summary>
    /// 데이터베이스 오프라인용
    /// </summary>
    #region
    private void LoginDataPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey(id.text))
        {
            loginResultTxt.text = "입력하신 아이디와 패스워드가 일치하지 않습니다.";
            return;
        }

        if (pw.text == PlayerPrefs.GetString(id.text))
        {
            GameManager.gm.sceneNumber = 1;
            SceneManager.LoadScene("99_LoadingScene");
        }
        else
            loginResultTxt.text = "입력하신 아이디와 패스워드가 일치하지 않습니다.";
    }

    private void SaveDataPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey(id.text))
        {
            PlayerPrefs.SetString(id.text, pw.text);
            loginResultTxt.text = "아이디 생성이 완료됐습니다.";
        }
        else
            loginResultTxt.text = "이미 존재하는 아이디입니다.";
    }
    #endregion

    private bool CheckInput(string id, string pw)
    {
        if (id == "" || pw == "")
        {
            loginResultTxt.text = "아이디 또는 패스워드를 입력해주세요.";
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
        isExitPopup = !isExitPopup;
        exitPopup.SetActive(isExitPopup);
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
