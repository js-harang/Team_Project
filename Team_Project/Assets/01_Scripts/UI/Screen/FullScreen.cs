using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    public GameObject[] toggles;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("FullScreen"))
            PlayerPrefs.SetInt("FullScreen", 1);

        if (PlayerPrefs.GetInt("FullScreen") == 1)
            toggles[0].GetComponent<Toggle>().isOn = true;
        else
            toggles[1].GetComponent<Toggle>().isOn = true;
    }

    public void ScreenMode(bool isChecked)
    {
        if (isChecked)
            GameManager.gm.IsFullScreen = 1;
        else
            GameManager.gm.IsFullScreen = 0;

        Screen.fullScreen = GameManager.gm.boolIsFullScreen;
    }
}