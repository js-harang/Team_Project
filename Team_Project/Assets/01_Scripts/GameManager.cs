using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    private void Awake()
    {
        if (gm != null)
            Destroy(gameObject);
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
        get { return isFullScreen; }
        set
        {
            isFullScreen = value;

            switch (value)
            {
                case 0:
                    boolIsFullScreen = false;
                    break;
                case 1:
                    boolIsFullScreen = true;
                    break;
            }
        }
    }
    [HideInInspector] public bool boolIsFullScreen = true;

    [HideInInspector] public bool isPreferencePopup = false;

    public string path;

    UIController ui;
    public UIController UI { get { return ui; } set { ui = value; } }

    [HideInInspector] public int slotNum = 0;

    #region 경험치 관련
    [SerializeField] int nowExp;
    public int NowExp
    { get { return nowExp; } set { nowExp = value; SetNowExp(); } }

    [SerializeField] int maxExp;
    public int MaxExp
    { get { return maxExp; } set { maxExp = value; } }

    // 필요 경험치
    [SerializeField] int requiredExp;

    [SerializeField] int lv;
    public int LV 
    { get { return lv; } set { lv = value; SetLevel(); } }
    #endregion

    private void Start()
    {
        ui = FindObjectOfType<UIController>().GetComponent<UIController>();
        PlayerPrefs.DeleteKey("uid");
        PlayerPrefs.DeleteKey("characteruid");

        LaodUserData();
        SetMaxExp();
        UI.SetLvText(LV);
        UI.SetEXPSlider(NowExp, maxExp);
    }

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

    public void SumEXP(int exp)
    {
        NowExp += exp;
        ChkExp();

        PlayerPrefs.Save();
        UI.SetEXPSlider(NowExp, maxExp);
    }

    private void ChkExp()
    {
        while (NowExp >= maxExp)
        {
            NowExp -= maxExp;
            LV++;
            UI.SetLvText(LV);

            Debug.Log("레벨업");
            Debug.Log("현재 레벨 : " + LV);

            SetMaxExp();
        }
    }

    private void SetMaxExp()
    {
        maxExp = requiredExp * LV;

        Debug.Log("다음 필요 경험치 : " + maxExp);
    }

    private void LaodUserData()
    {
        LV = PlayerPrefs.GetInt("LV");
        NowExp = PlayerPrefs.GetInt("NowExp");
    }
    private void SetLevel()
    {
        PlayerPrefs.SetInt("LV", LV);
    }
    private void SetNowExp()
    {
        PlayerPrefs.SetInt("NowExp", NowExp);
    }
}
