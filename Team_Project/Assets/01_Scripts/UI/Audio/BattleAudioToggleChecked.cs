using UnityEngine;

public class BattleAudioToggleChecked : MonoBehaviour
{
    [SerializeField] BattleAudioController ac;

    public void IsChecked(bool isChecked)
    {
        ac.AudioMute(gameObject.name, isChecked);
    }
}
