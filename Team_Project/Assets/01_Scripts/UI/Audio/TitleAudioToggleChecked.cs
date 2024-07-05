using UnityEngine;

public class TitleAudioToggleChecked : MonoBehaviour
{
    [SerializeField] TitleAudioController ac;

    public void IsChecked(bool isChecked)
    {
        ac.AudioMute(gameObject.name, isChecked);
    }
}
