using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TitleController : MonoBehaviour
{
    [SerializeField] TMP_InputField id;
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

    /// <summary>
    /// �α���
    /// </summary>
    public void LoginCheck()
    {
        if (!CheckInput(id.text, pw.text))
            return;

        StartCoroutine(LoginDataPost(id.text, pw.text));

        //string pass = PlayerPrefs.GetString(id.text);
        //if (pw.text == pass)
        //{
        //    GameManager.gm.sceneNumber = 1;
        //    SceneManager.LoadScene("99_LoadingScene");
        //}
        //else
        //    loginResultTxt.text = "�Է��Ͻ� ���̵�� �н����尡 ��ġ���� �ʽ��ϴ�.";
    }

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
                        Debug.Log("����");
                        //GameManager.gm.sceneNumber = 1;
                        //SceneManager.LoadScene("99_LoadingScene");
                        break;
                    case "2":
                        loginResultTxt.text = "�α��� ����";
                        break;
                }
        }
    }

    public void SaveUserData()
    {
        if (!CheckInput(id.text, pw.text))
            return;

        StartCoroutine(SaveDataPost(id.text, pw.text));

        //if (!PlayerPrefs.HasKey(id.text))
        //{
        //    PlayerPrefs.SetString(id.text, pw.text);
        //    loginResultTxt.text = "���̵� ������ �Ϸ�ƽ��ϴ�.";
        //}
        //else
        //    loginResultTxt.text = "�̹� �����ϴ� ���̵��Դϴ�.";
    }

    IEnumerator SaveDataPost(string id, string pw)
    {
        string url = path + "login.php";
        WWWForm form = new();

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
                switch (www.downloadHandler.text)
                {
                    case "1":
                        loginResultTxt.text = "�α���!";
                        Debug.Log(1);
                        //GameManager.gm.sceneNumber = 1;
                        //SceneManager.LoadScene("99_LoadingScene");
                        break;
                    case "2":
                        loginResultTxt.text = "�α��� ����";
                        break;
                }
        }
    }

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
