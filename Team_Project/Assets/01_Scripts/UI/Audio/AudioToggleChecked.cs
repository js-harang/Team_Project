using UnityEngine;

public class AudioToggleChecked : MonoBehaviour
{
    [SerializeField] AudioController ac;

    public void IsChecked(bool isChecked)
    {
        ac.AudioMute(gameObject.name, isChecked);
    }
}
