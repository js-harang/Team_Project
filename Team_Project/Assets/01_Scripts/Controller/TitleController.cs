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
    /// �α���
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
            loginResultTxt.text = "�Է��Ͻ� ���̵�� �н����尡 ��ġ���� �ʽ��ϴ�.";
    }

    public void SaveUserData()
    {
        if (!CheckInput(id.text, pw.text))
            return;

        if (!PlayerPrefs.HasKey(id.text))
        {
            PlayerPrefs.SetString(id.text, pw.text);
            loginResultTxt.text = "���̵� ������ �Ϸ�ƽ��ϴ�.";
        }
        else
            loginResultTxt.text = "�̹� �����ϴ� ���̵��Դϴ�.";
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
}
