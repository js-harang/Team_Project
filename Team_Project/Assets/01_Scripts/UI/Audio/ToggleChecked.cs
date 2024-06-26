using UnityEngine;

public class ToggleChecked : MonoBehaviour
{
    [SerializeField] AudioController ac;

    public void IsChecked(bool isChecked)
    {
        ac.masterCheck = isChecked;
        ac.ToggleChecked(gameObject.name, isChecked);
    }
}
