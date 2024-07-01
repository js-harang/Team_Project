using UnityEngine;

public class WindowScreen : MonoBehaviour
{
    public void ToggleWindowMode(bool isChecked)
    {
        if (GameManager.gm.IsFullScreen == 0)
            return;

        GameManager.gm.IsFullScreen = 0;
        Screen.fullScreen = GameManager.gm.boolIsFullScreen;
    }
}
