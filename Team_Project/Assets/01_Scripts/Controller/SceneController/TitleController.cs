using DG.Tweening;
using TMPro;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField] TMP_InputField id;
    [SerializeField] TMP_InputField pw;
    [SerializeField] TextMeshProUGUI idPlaceholder;
    [SerializeField] TextMeshProUGUI pwPlaceholder;
    [SerializeField] TextMeshProUGUI loginResultTxt;

    // Preference ��ư ���� ����
    [SerializeField, Space(10)]
    GameObject preferencePopup;
    bool isPreferencePopup = false;

    private void Start()
    {
        idPlaceholder.DOFade(0.1f, 1f).SetLoops(-1, LoopType.Yoyo);
        pwPlaceholder.DOFade(0.1f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        // ȯ�漳�� Ż��
        if (Input.GetKeyDown(KeyCode.Escape) && isPreferencePopup)
            ClickPreferencesBtn();
    }

    /// <summary>
    /// �α���
    /// </summary>
    public void LoginCheck(int sceneNumber)
    {
        if (!CheckInput(id.text, pw.text))
            return;

        string pass = PlayerPrefs.GetString(id.text);
        if (pw.text == pass)
            GameManager.gm.MoveScene(sceneNumber);
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

    /// <summary>
    /// ȯ�漳�� ��ư
    /// </summary>
    public void ClickPreferencesBtn()
    {
        isPreferencePopup = !isPreferencePopup;
        preferencePopup.SetActive(isPreferencePopup);
    }
}