using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] TMP_InputField id;
    [SerializeField] TMP_InputField pw;
    [SerializeField] TextMeshProUGUI loginResultTxt;

    [SerializeField, Space(10)] TextMeshProUGUI idPlaceholder;
    [SerializeField] TextMeshProUGUI pwPlaceholder;

    // Preference 버튼 관련 변수
    [SerializeField, Space(10)] GameObject preferencePopup;
    bool isPreferencePopup = false;

    [SerializeField, Space(10)] GameObject exitPopup;
    bool isExitPopup = false;

    [SerializeField, Space(10)] ResolutionManager rm;
    [SerializeField] FullScreen fullScreen;
    AudioController ac;

    [SerializeField, Space(10)] GameObject saveSettingPopup;
    bool isSaveSettingPopup = false;

    private void Start()
    {
        ac = GetComponent<AudioController>();

        idPlaceholder.DOFade(0.1f, 1f).SetLoops(-1, LoopType.Yoyo);
        pwPlaceholder.DOFade(0.1f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        // 환경설정 탈출
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPreferencePopup)
                ExitGamePopup();
            else
            {
                if (isSaveSettingPopup)
                    BasicSettingNoBtn();
                else
                    ResetSetting();
            }
        }
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

    public void PreferencesBtn()
    {
        isPreferencePopup = !isPreferencePopup;
        preferencePopup.SetActive(isPreferencePopup);
    }

    public void BasicSettingBtn()
    {
        isSaveSettingPopup = !isSaveSettingPopup;
        saveSettingPopup.SetActive(isSaveSettingPopup);
    }

    public void BasicSettingYesBtn()
    {
        BasicSetting();
        BasicSettingNoBtn();
    }

    public void BasicSettingNoBtn()
    {
        isSaveSettingPopup = !isSaveSettingPopup;
        saveSettingPopup.SetActive(isSaveSettingPopup);
    }

    private void BasicSetting()
    {
        PlayerPrefs.SetInt("ResolutionIndex", rm.resolutionDropdown.value = 0);
        PlayerPrefs.SetInt("FullScreen", 1);
        fullScreen.toggles[0].GetComponent<Toggle>().isOn = true;

        PlayerPrefs.SetFloat("Master", ac.masterSlider.value = 1);
        PlayerPrefs.SetFloat("BGM", ac.bgmSlider.value = 1);
        PlayerPrefs.SetFloat("SFX", ac.sfxSlider.value = 1);

        PlayerPrefs.SetInt("MasterMute", 0);
        ac.masterToggle.isOn = false;
        PlayerPrefs.SetInt("BGMMute", 0);
        ac.bgmToggle.isOn = false;
        PlayerPrefs.SetInt("SFXMute", 0);
        ac.sfxToggle.isOn = false;
    }

    public void SaveSettingBtn()
    {
        PlayerPrefs.SetInt("ResolutionIndex", rm.resolutionDropdown.value);
        PlayerPrefs.SetInt("FullScreen", GameManager.gm.IsFullScreen);

        PlayerPrefs.SetFloat("Master", ac.masterSlider.value);
        PlayerPrefs.SetFloat("BGM", ac.bgmSlider.value);
        PlayerPrefs.SetFloat("SFX", ac.sfxSlider.value);

        if (ac.masterToggle.isOn)
            PlayerPrefs.SetInt("MasterMute", 1);
        else
            PlayerPrefs.SetInt("MasterMute", 0);
        if (ac.bgmToggle.isOn)
            PlayerPrefs.SetInt("BGMMute", 1);
        else
            PlayerPrefs.SetInt("BGMMute", 0);
        if (ac.sfxToggle.isOn)
            PlayerPrefs.SetInt("SFXMute", 1);
        else
            PlayerPrefs.SetInt("SFXMute", 0);

        isPreferencePopup = !isPreferencePopup;
        preferencePopup.SetActive(isPreferencePopup);
    }

    public void ResetSetting()
    {
        rm.resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex");
        if (PlayerPrefs.GetInt("FullScreen") == 1)
            fullScreen.toggles[0].GetComponent<Toggle>().isOn = true;
        else
            fullScreen.toggles[1].GetComponent<Toggle>().isOn = true;

        ac.masterSlider.value = PlayerPrefs.GetFloat("Master");
        ac.bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        ac.sfxSlider.value = PlayerPrefs.GetFloat("SFX");
        if (PlayerPrefs.GetInt("MasterMute") == 1)
            ac.masterToggle.isOn = true;
        else
            ac.masterToggle.isOn = false;
        if (PlayerPrefs.GetInt("BGMMute") == 1)
            ac.bgmToggle.isOn = true;
        else
            ac.bgmToggle.isOn = false;
        if (PlayerPrefs.GetInt("SFXMute") == 1)
            ac.sfxToggle.isOn = true;
        else
            ac.sfxToggle.isOn = false;

        isPreferencePopup = !isPreferencePopup;
        preferencePopup.SetActive(isPreferencePopup);
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
