using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    [SerializeField] AudioController ac;

    [SerializeField, Space(10)] Toggle masterToggle;
    [SerializeField] Toggle bgmToggle;
    [SerializeField] Toggle sfxToggle;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MasterToggle"))
        {
            if (PlayerPrefs.GetInt("MasterToggle") == 1)
                MasterToggleChecked(masterToggle.isOn = true);
        }
        else
            PlayerPrefs.SetInt("MasterToggle", 0);

        if (PlayerPrefs.HasKey("BGMToggle"))
        {
            if (PlayerPrefs.GetInt("BGMToggle") == 1)
                BGMToggleChecked(bgmToggle.isOn = true);
        }
        else
            PlayerPrefs.SetInt("BGMToggle", 0);

        if (PlayerPrefs.HasKey("SFXToggle"))
        {
            if (PlayerPrefs.GetInt("SFXToggle") == 1)
                SFXToggleChecked(sfxToggle.isOn = true);
        }
        else
            PlayerPrefs.SetInt("SFXToggle", 0);
    }

    public void MasterToggleChecked(bool isChecked)
    {
        if (isChecked)
        {
            PlayerPrefs.SetInt("MasterToggle", 1);
            ac.MasterVolumeMute(true);
        }
        else
        {
            PlayerPrefs.SetInt("MasterToggle", 0);
            ac.MasterVolumeMute(false);
        }
    }

    public void BGMToggleChecked(bool isChecked)
    {
        if (isChecked)
        {
            PlayerPrefs.SetInt("BGMToggle", 1);
            ac.BGMVolumeMute(true);
        }
        else
        {
            PlayerPrefs.SetInt("BGMToggle", 0);
            ac.BGMVolumeMute(false);
        }
    }

    public void SFXToggleChecked(bool isChecked)
    {
        if (isChecked)
        {
            PlayerPrefs.SetInt("SFXToggle", 1);
            ac.SFXVolumeMute(true);
        }
        else
        {
            PlayerPrefs.SetInt("SFXToggle", 0);
            ac.SFXVolumeMute(false);
        }
    }
}
