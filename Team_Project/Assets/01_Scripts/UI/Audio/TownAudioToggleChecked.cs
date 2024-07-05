using UnityEngine;

public class TownAudioToggleChecked : MonoBehaviour
{
    [SerializeField] TownAudioController ac;

    public void IsChecked(bool isChecked)
    {
        ac.AudioMute(gameObject.name, isChecked);
    }
}
