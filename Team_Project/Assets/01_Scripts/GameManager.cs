using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    private void Awake()
    {
        if (gm != null)
            Destroy(this);
        else
        {
            gm = this;

            DontDestroyOnLoad(gm);
        }
    }

    [HideInInspector] public int sceneNumber;

    // 환경설정 선택되어 있는 옵션
    [HideInInspector] public GameObject selectObject;

    private int isFullScreen;
    // 전체화면 / 창모드 선택
    public int IsFullScreen
    {
        get
        {
            if (PlayerPrefs.HasKey("IsFullScreen"))
                isFullScreen = PlayerPrefs.GetInt("IsFullScreen");
            else
            {
                isFullScreen = 0;
                PlayerPrefs.SetInt("IsFullScreen", 0);
            }
            return isFullScreen;
        }
        set
        {
            isFullScreen = value;
            PlayerPrefs.SetInt("IsFullScreen", isFullScreen);

            switch (value)
            {
                case 0:
                    boolIsFullScreen = !boolIsFullScreen;
                    break;
                case 1:
                    boolIsFullScreen = !boolIsFullScreen;
                    break;
            }
        }
    }
    public bool boolIsFullScreen = true;

    /// <summary>
    /// 0 = TitleScene
    /// 1 = LobbyScene
    /// 2 = TownScene
    /// 3 = BattleScene
    /// </summary>
    public void MoveScene(int number)
    {
        sceneNumber = number;
        SceneManager.LoadScene("99_LoadingScene");
    }
}
