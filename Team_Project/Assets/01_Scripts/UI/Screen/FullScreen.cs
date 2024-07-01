using UnityEngine;

public class FullScreen : MonoBehaviour
{
    public void ToggleFullscreenMode(bool isChecked)
    {
        if (GameManager.gm.isFullscreen)
            return;

        GameManager.gm.isFullscreen = !GameManager.gm.isFullscreen;
        Screen.fullScreen = GameManager.gm.isFullscreen;
    }
}