using UnityEngine;
using UnityEngine.UI;

public class WindowScreen : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(name);

        if (GameManager.gm.IsFullScreen == 0)
            GetComponent<Toggle>().isOn = true;
    }

    public void ToggleWindowMode(bool isChecked)
    {
        if (GameManager.gm.IsFullScreen == 1)
            return;

        GameManager.gm.IsFullScreen = 0;
        Screen.fullScreen = GameManager.gm.boolIsFullScreen;
    }
}
