using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField, Space(10)]
    TMP_InputField id;
    [SerializeField]
    TMP_InputField pw;
    [SerializeField]
    TextMeshProUGUI idPlaceholder;
    [SerializeField]
    TextMeshProUGUI pwPlaceholder;
    [SerializeField]
    TextMeshProUGUI loginResultTxt;

    private void Start()
    {
        idPlaceholder.DOFade(0.1f, 1f).SetLoops(-1, LoopType.Yoyo);
        pwPlaceholder.DOFade(0.1f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// 로그인
    /// </summary>
    public void LoginCheck()
    {
        if (!CheckInput(id.text, pw.text))
            return;

        string pass = PlayerPrefs.GetString(id.text);
        if (pw.text == pass)
        {
            GameManager.gm.sceneNumber = 1;
            SceneManager.LoadScene("99_LoadingScene");
        }
        else
            loginResultTxt.text = "입력하신 아이디와 패스워드가 일치하지 않습니다.";
    }

    public void SaveUserData()
    {
        if (!CheckInput(id.text, pw.text))
            return;

        if (!PlayerPrefs.HasKey(id.text))
        {
            PlayerPrefs.SetString(id.text, pw.text);
            loginResultTxt.text = "아이디 생성이 완료됐습니다.";
        }
        else
            loginResultTxt.text = "이미 존재하는 아이디입니다.";
    }

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
}
