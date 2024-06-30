using UnityEngine;

public class WindowScreen : MonoBehaviour
{
    public void ToggleWindowMode(bool isChecked)
    {
        if (!GameManager.gm.isFullscreen)
            return;

        GameManager.gm.isFullscreen = !GameManager.gm.isFullscreen;
        Screen.fullScreen = GameManager.gm.isFullscreen;
    }
}
