using UnityEngine;
using UnityEngine.UI;

public class PerferencesManager : MonoBehaviour
{
    [SerializeField, Space(10)] ResolutionManager rm;
    [SerializeField] FullScreen fullScreen;

    [Space(10)] public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Space(10)] public Toggle masterToggle;
    public Toggle bgmToggle;
    public Toggle sfxToggle;

    [SerializeField, Space(10)] GameObject saveSettingPopup;
    bool isSaveSettingPopup = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isSaveSettingPopup)
            {
                isSaveSettingPopup = false;
                saveSettingPopup.SetActive(isSaveSettingPopup);
            }
            else
                ResetSettingBtn();
        }
    }

    public void BasicSettingBtn()
    {
        isSaveSettingPopup = true;
        saveSettingPopup.SetActive(isSaveSettingPopup);
    }

    public void BasicSettingYesBtn()
    {
        BasicSetting();
        BasicSettingNoBtn();
    }

    public void BasicSettingNoBtn()
    {
        isSaveSettingPopup = false;
        saveSettingPopup.SetActive(isSaveSettingPopup);
    }

    private void BasicSetting()
    {
        PlayerPrefs.SetInt("ResolutionIndex", rm.resolutionDropdown.value = 0);
        PlayerPrefs.SetInt("FullScreen", 1);
        fullScreen.toggles[0].GetComponent<Toggle>().isOn = true;

        PlayerPrefs.SetFloat("Master", masterSlider.value = 1);
        PlayerPrefs.SetFloat("BGM", bgmSlider.value = 1);
        PlayerPrefs.SetFloat("SFX", sfxSlider.value = 1);

        PlayerPrefs.SetInt("MasterMute", 0);
        masterToggle.isOn = false;
        PlayerPrefs.SetInt("BGMMute", 0);
        bgmToggle.isOn = false;
        PlayerPrefs.SetInt("SFXMute", 0);
        sfxToggle.isOn = false;
    }

    public void SaveSettingBtn()
    {
        PlayerPrefs.SetInt("ResolutionIndex", rm.resolutionDropdown.value);
        PlayerPrefs.SetInt("FullScreen", GameManager.gm.IsFullScreen);

        PlayerPrefs.SetFloat("Master", masterSlider.value);
        PlayerPrefs.SetFloat("BGM", bgmSlider.value);
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);

        if (masterToggle.isOn)
            PlayerPrefs.SetInt("MasterMute", 1);
        else
            PlayerPrefs.SetInt("MasterMute", 0);
        if (bgmToggle.isOn)
            PlayerPrefs.SetInt("BGMMute", 1);
        else
            PlayerPrefs.SetInt("BGMMute", 0);
        if (sfxToggle.isOn)
            PlayerPrefs.SetInt("SFXMute", 1);
        else
            PlayerPrefs.SetInt("SFXMute", 0);

        OffPreferencePopup();
    }

    public void ResetSettingBtn()
    {
        rm.resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex");
        if (PlayerPrefs.GetInt("FullScreen") == 1)
            fullScreen.toggles[0].GetComponent<Toggle>().isOn = true;
        else
            fullScreen.toggles[1].GetComponent<Toggle>().isOn = true;

        masterSlider.value = PlayerPrefs.GetFloat("Master");
        bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX");
        if (PlayerPrefs.GetInt("MasterMute") == 1)
            masterToggle.isOn = true;
        else
            masterToggle.isOn = false;
        if (PlayerPrefs.GetInt("BGMMute") == 1)
            bgmToggle.isOn = true;
        else
            bgmToggle.isOn = false;
        if (PlayerPrefs.GetInt("SFXMute") == 1)
            sfxToggle.isOn = true;
        else
            sfxToggle.isOn = false;

        OffPreferencePopup();
    }

    private void OffPreferencePopup()
    {
        GameManager.gm.isPreferencePopup = false;
        gameObject.SetActive(GameManager.gm.isPreferencePopup);
    }
}
