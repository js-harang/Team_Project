using UnityEngine;
using UnityEngine.Audio;

public class BattleAudioController : MonoBehaviour
{
    [SerializeField] PerferencesManager pm;

    [SerializeField, Space(10)] AudioMixer audioMixer;

    private void OnEnable()
    {
        pm.masterSlider.onValueChanged.AddListener(SetMasterVolume);
        pm.bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        pm.sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Master"))
            pm.masterSlider.value = PlayerPrefs.GetFloat("Master");
        else
            pm.masterSlider.value = PlayerPrefs.GetFloat("Master", 0);

        if (PlayerPrefs.HasKey("BGM"))
            pm.bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        else
            pm.bgmSlider.value = PlayerPrefs.GetFloat("BGM", 0);

        if (PlayerPrefs.HasKey("SFX"))
            pm.sfxSlider.value = PlayerPrefs.GetFloat("SFX");
        else
            pm.sfxSlider.value = PlayerPrefs.GetFloat("SFX", 0);

        SetMasterVolume(PlayerPrefs.GetFloat("Master"));
        SetBGMVolume(PlayerPrefs.GetFloat("BGM"));
        SetSFXVolume(PlayerPrefs.GetFloat("SFX"));

        if (PlayerPrefs.HasKey("MasterMute"))
        {
            if (PlayerPrefs.GetInt("MasterMute") == 1)
                pm.masterToggle.isOn = true;
        }
        else
            PlayerPrefs.SetInt("MasterMute", 0);

        if (PlayerPrefs.HasKey("BGMMute"))
        {
            if (PlayerPrefs.GetInt("BGMMute") == 1)
                pm.masterToggle.isOn = true;
        }
        else
            PlayerPrefs.SetInt("BGMMute", 0);

        if (PlayerPrefs.HasKey("SFXMute"))
        {
            if (PlayerPrefs.GetInt("SFXMute") == 1)
                pm.masterToggle.isOn = true;
        }
        else
            PlayerPrefs.SetInt("SFXMute", 0);
    }

    public void SetMasterVolume(float volume)
    {
        if (pm.masterToggle.isOn)
            return;

        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetBGMVolume(float volume)
    {
        if (pm.bgmToggle.isOn)
            return;

        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        if (pm.sfxToggle.isOn)
            return;

        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void AudioMute(string toggleName, bool isChecked)
    {
        switch (toggleName)
        {
            case "Master_Toggle":
                if (isChecked)
                    audioMixer.SetFloat("Master", -80f);
                else
                    SetMasterVolume(pm.masterSlider.value);
                break;
            case "BGM_Toggle":
                if (isChecked)
                    audioMixer.SetFloat("BGM", -80f);
                else
                    SetBGMVolume(pm.bgmSlider.value);
                break;
            case "SFX_Toggle":
                if (isChecked)
                    audioMixer.SetFloat("SFX", -80f);
                else
                    SetSFXVolume(pm.sfxSlider.value);
                break;
        }
    }

    private void OnDisable()
    {
        pm.masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        pm.bgmSlider.onValueChanged.RemoveListener(SetBGMVolume);
        pm.sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }
}
