using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [Space(10)] public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Space(10)] public Toggle masterToggle;
    public Toggle bgmToggle;
    public Toggle sfxToggle;

    private void OnEnable()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Master"))
            masterSlider.value = PlayerPrefs.GetFloat("Master");
        else
            masterSlider.value = PlayerPrefs.GetFloat("Master", 1);

        if (PlayerPrefs.HasKey("BGM"))
            bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        else
            bgmSlider.value = PlayerPrefs.GetFloat("BGM", 1);

        if (PlayerPrefs.HasKey("SFX"))
            sfxSlider.value = PlayerPrefs.GetFloat("SFX");
        else
            sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1);

        SetMasterVolume(PlayerPrefs.GetFloat("Master"));
        SetBGMVolume(PlayerPrefs.GetFloat("BGM"));
        SetSFXVolume(PlayerPrefs.GetFloat("SFX"));

        if (PlayerPrefs.HasKey("MasterMute"))
        {
            if (PlayerPrefs.GetInt("MasterMute") == 1)
                masterToggle.isOn = true;
        }
        else
            PlayerPrefs.SetInt("MasterMute", 0);

        if (PlayerPrefs.HasKey("BGMMute"))
        {
            if (PlayerPrefs.GetInt("BGMMute") == 1)
                masterToggle.isOn = true;
        }
        else
            PlayerPrefs.SetInt("BGMMute", 0);

        if (PlayerPrefs.HasKey("SFXMute"))
        {
            if (PlayerPrefs.GetInt("SFXMute") == 1)
                masterToggle.isOn = true;
        }
        else
            PlayerPrefs.SetInt("SFXMute", 0);
    }

    public void SetMasterVolume(float volume)
    {
        if (masterToggle.isOn)
            return;

        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetBGMVolume(float volume)
    {
        if (bgmToggle.isOn)
            return;

        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxToggle.isOn)
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
                    SetMasterVolume(masterSlider.value);
                break;
            case "BGM_Toggle":
                if (isChecked)
                    audioMixer.SetFloat("BGM", -80f);
                else
                    SetBGMVolume(bgmSlider.value);
                break;
            case "SFX_Toggle":
                if (isChecked)
                    audioMixer.SetFloat("SFX", -80f);
                else
                    SetSFXVolume(sfxSlider.value);
                break;
        }
    }

    //public void SetMasterVolume(float volume)
    //{
    //    PlayerPrefs.SetFloat("Master", volume);

    //    if (PlayerPrefs.GetInt("MasterMute") == 1)
    //    {
    //        PlayerPrefs.SetFloat("SaveMaster", volume);
    //        return;
    //    }

    //    audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    //}

    //public void SetBGMVolume(float volume)
    //{
    //    PlayerPrefs.SetFloat("BGM", volume);

    //    if (PlayerPrefs.GetInt("BGMMute") == 1)
    //    {
    //        PlayerPrefs.SetFloat("SaveBGM", volume);
    //        return;
    //    }

    //    audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    //}

    //public void SetSFXVolume(float volume)
    //{
    //    PlayerPrefs.SetFloat("SFX", volume);

    //    if (PlayerPrefs.GetInt("SFXMute") == 1)
    //    {
    //        PlayerPrefs.SetFloat("SaveSFX", volume);
    //        return;
    //    }

    //    audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    //}

    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        bgmSlider.onValueChanged.RemoveListener(SetBGMVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }
}
