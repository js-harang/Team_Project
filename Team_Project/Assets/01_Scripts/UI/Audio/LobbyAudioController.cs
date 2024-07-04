using UnityEngine;
using UnityEngine.Audio;

public class LobbyAudioController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Master"))
            audioMixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat("Master")) * 20);
        else
            audioMixer.SetFloat("Master", PlayerPrefs.GetFloat("Master", 0) * 20);

        if (PlayerPrefs.HasKey("BGM"))
            audioMixer.SetFloat("BGM", Mathf.Log10(PlayerPrefs.GetFloat("BGM")) * 20);
        else
            audioMixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGM", 0) * 20);

        if (PlayerPrefs.HasKey("SFX"))
            audioMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFX")) * 20);
        else
            audioMixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX", 0) * 20);

        if (PlayerPrefs.GetInt("MasterMute") == 1)
            audioMixer.SetFloat("Master", -80f);

        if (PlayerPrefs.GetInt("BGMMute") == 1)
            audioMixer.SetFloat("BGM", -80f);

        if (PlayerPrefs.GetInt("SFXMute") == 1)
            audioMixer.SetFloat("SFX", -80f);
    }
}
