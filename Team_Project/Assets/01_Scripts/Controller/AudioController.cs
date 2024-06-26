using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField, Space(10)] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField, Space(10)] AudioSource masterAudio;

    private void OnEnable()
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

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        SetMasterVolume(PlayerPrefs.GetFloat("Master"));
        SetBGMVolume(PlayerPrefs.GetFloat("BGM"));
        SetSFXVolume(PlayerPrefs.GetFloat("SFX"));
    }

    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("Master", volume);
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat("BGM", volume);
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFX", volume);
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void MasterVolumeMute(bool isChecked)
    {
        if (isChecked)
        {
            PlayerPrefs.SetFloat("SaveMaster", PlayerPrefs.GetFloat("Master"));
            SetMasterVolume(0.001f);
        }
        else
            SetMasterVolume(PlayerPrefs.GetFloat("SaveMaster"));

        masterSlider.value = PlayerPrefs.GetFloat("Master");
    }
    
    public void BGMVolumeMute(bool isChecked)
    {
        if (isChecked)
        {
            PlayerPrefs.SetFloat("SaveBGM", PlayerPrefs.GetFloat("BGM"));
            SetMasterVolume(0.001f);
        }
        else
            SetMasterVolume(PlayerPrefs.GetFloat("SaveBGM"));

        bgmSlider.value = PlayerPrefs.GetFloat("BGM");
    }
    
    public void SFXVolumeMute(bool isChecked)
    {
        if (isChecked)
        {
            PlayerPrefs.SetFloat("SaveMaster", PlayerPrefs.GetFloat("SFX"));
            SetMasterVolume(0.001f);
        }
        else
            SetMasterVolume(PlayerPrefs.GetFloat("SaveMaster"));

       sfxSlider.value = PlayerPrefs.GetFloat("SFX");
    }

    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        bgmSlider.onValueChanged.RemoveListener(SetBGMVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }

    //public AudioMixer _mixer;

    //const string MIXER_MUSIC = "MusicVolume";
    //const string MIXER_SFX = "SFXVolume";

    //const string k_MusicVolumeKey = "SaveMusicVolume";
    //const string k_SFXVolumeKey = "SaveSFXVolume";

    //public Slider musicSlider;
    //public Slider SFXSlider;

    //private void Start()
    //{
    //    SetMusicVolume(PlayerPrefs.GetFloat(k_MusicVolumeKey));
    //    SetSFXVolume(PlayerPrefs.GetFloat(k_SFXVolumeKey));
    //}
    //private void OnEnable()
    //{
    //    musicSlider.value = PlayerPrefs.GetFloat(k_MusicVolumeKey);
    //    musicSlider.onValueChanged.AddListener(SetMusicVolume);

    //    SFXSlider.value = PlayerPrefs.GetFloat(k_SFXVolumeKey);
    //    SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    //}

    //private void OnDisable()
    //{
    //    musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
    //    SFXSlider.onValueChanged.RemoveListener(SetSFXVolume);
    //}

    //private void SetMusicVolume(float value)
    //{
    //    PlayerPrefs.SetFloat(k_MusicVolumeKey, value);
    //    _mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    //}

    //private void SetSFXVolume(float value)
    //{
    //    PlayerPrefs.SetFloat(k_SFXVolumeKey, value);
    //    _mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    //}

    //[SerializeField]
    //AudioSource currentVolume;
    //[SerializeField]
    //Slider volumeSlider;
    //float saveVolume;

    //private void Start()
    //{
    //    currentVolume = GetComponent<AudioSource>();

    //    if (PlayerPrefs.HasKey("volume"))
    //        saveVolume = PlayerPrefs.GetFloat("volume");
    //    else
    //        saveVolume = PlayerPrefs.GetFloat("volume", 1);

    //    currentVolume.volume = saveVolume;
    //    SetSlider();
    //}

    //private void Update()
    //{
    //    if (currentVolume.volume != saveVolume)
    //    {
    //        saveVolume = currentVolume.volume;
    //        PlayerPrefs.SetFloat("volume", saveVolume);
    //        PlayerPrefs.Save();
    //        SetSlider();
    //    }
    //}

    //public void VolumeMute(bool isChecked)
    //{
    //    if(isChecked)
    //        currentVolume.mute = true;
    //    else
    //        currentVolume.mute = false;

    //    SetSlider();
    //}

    //private void SetSlider()
    //{
    //    volumeSlider.value = currentVolume.volume;
    //}
}
