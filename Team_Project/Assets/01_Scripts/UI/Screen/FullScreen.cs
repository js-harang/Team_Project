using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.gm.IsFullScreen == 1)
            GetComponent<Toggle>().isOn = true;
        else
            GameObject.Find("Window_Toggle").GetComponent<Toggle>().isOn = true;
    }

    public void ToggleFullscreenMode(bool isChecked)
    {
        if (GameManager.gm.IsFullScreen == 1)
            return;

        GameManager.gm.IsFullScreen = 1;
        Screen.fullScreen = GameManager.gm.boolIsFullScreen;
    }
}