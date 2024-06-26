using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField, Space(10)] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField, Space(10)] Toggle masterToggle;
    [SerializeField] Toggle bmgToggle;
    [SerializeField] Toggle sfxToggle;

    [HideInInspector] public bool masterCheck = false;

    private void OnEnable()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        //bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        //sfxSlider.onValueChanged.AddListener(SetSFXVolume);
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

        //SetMasterVolume(PlayerPrefs.GetFloat("Master"));
        //SetBGMVolume(PlayerPrefs.GetFloat("BGM"));
        //SetSFXVolume(PlayerPrefs.GetFloat("SFX"));
    }

    private void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("Master", volume);

        if (masterCheck)
            return;

        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void ToggleChecked(string keyName, bool isChecked)
    {
        switch (keyName)
        {
            case "MasterToggle":
                if (isChecked)
                {
                    SetMasterVolume(0.001f);
                }
                else
                {
                    PlayerPrefs.SetFloat("Master", masterSlider.value);
                    SetMasterVolume(PlayerPrefs.GetFloat("Master"));
                }
                break;
                //case "BGMToggle":
                //    if (isChecked)
                //    {
                //        PlayerPrefs.SetFloat("SaveBGM", PlayerPrefs.GetFloat("BGM"));
                //        SetBGMVolume(0.001f);
                //    }
                //    else
                //        SetBGMVolume(PlayerPrefs.GetFloat("SaveBGM"));

                //    bgmSlider.value = PlayerPrefs.GetFloat("BGM");
                //    break;
                //case "SFXToggle":
                //    if (isChecked)
                //    {
                //        PlayerPrefs.SetFloat("SaveSFX", PlayerPrefs.GetFloat("SFX"));
                //        SetSFXVolume(0.001f);
                //    }
                //    else
                //        SetSFXVolume(PlayerPrefs.GetFloat("SaveSFX"));

                //    sfxSlider.value = PlayerPrefs.GetFloat("SFX");
                //    break;
        }
    }

    //private void SetMasterVolume(float volume)
    //{
    //    PlayerPrefs.SetFloat("Master", volume);
    //    audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);

    //    if (masterSlider.value <= 0.001f)
    //    {
    //        masterToggle.isOn = true;
    //        masterCheck = true;
    //        Debug.Log(1);
    //    }
    //    else
    //    {
    //        masterToggle.isOn = false;
    //    }
    //}

    //private void SetBGMVolume(float volume)
    //{
    //    PlayerPrefs.SetFloat("BGM", volume);
    //    audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    //}

    //private void SetSFXVolume(float volume)
    //{
    //    PlayerPrefs.SetFloat("SFX", volume);
    //    audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    //}

    //public void ToggleChecked(string keyName, bool isChecked)
    //{
    //    switch (keyName)
    //    {
    //        case "MasterToggle":
    //            if (isChecked)
    //            {
    //                if (!masterCheck)
    //                    PlayerPrefs.SetFloat("SaveMaster", PlayerPrefs.GetFloat("Master"));

    //                masterCheck = true;
    //                SetMasterVolume(0.001f);
    //            }
    //            else
    //                SetMasterVolume(PlayerPrefs.GetFloat("SaveMaster"));

    //            masterSlider.value = PlayerPrefs.GetFloat("Master");
    //            break;
    //        case "BGMToggle":
    //            if (isChecked)
    //            {
    //                PlayerPrefs.SetFloat("SaveBGM", PlayerPrefs.GetFloat("BGM"));
    //                SetBGMVolume(0.001f);
    //            }
    //            else
    //                SetBGMVolume(PlayerPrefs.GetFloat("SaveBGM"));

    //            bgmSlider.value = PlayerPrefs.GetFloat("BGM");
    //            break;
    //        case "SFXToggle":
    //            if (isChecked)
    //            {
    //                PlayerPrefs.SetFloat("SaveSFX", PlayerPrefs.GetFloat("SFX"));
    //                SetSFXVolume(0.001f);
    //            }
    //            else
    //                SetSFXVolume(PlayerPrefs.GetFloat("SaveSFX"));

    //            sfxSlider.value = PlayerPrefs.GetFloat("SFX");
    //            break;
    //    }
    //}

    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        //bgmSlider.onValueChanged.RemoveListener(SetBGMVolume);
        //sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }

    //private void Update()
    //{
    //    Debug.Log(PlayerPrefs.GetFloat("Master"));
    //    Debug.Log(PlayerPrefs.GetFloat("BGM"));
    //    Debug.Log(PlayerPrefs.GetFloat("SFX"));
    //}
}
