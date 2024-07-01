using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(name);

        if (GameManager.gm.IsFullScreen == 1)
            GetComponent<Toggle>().isOn = true;
    }

    public void ToggleFullscreenMode(bool isChecked)
    {
        if (GameManager.gm.IsFullScreen == 0)
            return;

        GameManager.gm.IsFullScreen = 1;
        Screen.fullScreen = GameManager.gm.boolIsFullScreen;
    }
}