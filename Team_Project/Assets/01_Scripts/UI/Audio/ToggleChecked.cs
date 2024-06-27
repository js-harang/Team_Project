using UnityEngine;

public class ToggleChecked : MonoBehaviour
{
    [SerializeField] AudioController ac;

    public void IsChecked(bool isChecked)
    {
        ac.AudioMute(gameObject.name, isChecked);
    }
}
