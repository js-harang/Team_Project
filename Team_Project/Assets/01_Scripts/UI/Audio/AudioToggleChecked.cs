using UnityEngine;

public class AudioToggleChecked : MonoBehaviour
{
    [SerializeField] TitleAudioController ac;

    public void IsChecked(bool isChecked)
    {
        ac.AudioMute(gameObject.name, isChecked);
    }
}
