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
    [SerializeField] float currentExp;
    public float CurrentExp { get { return currentExp; } set { currentExp = value; } }

    [SerializeField] int maxExp;
    public int MaxExp { get { return maxExp; } set { maxExp = value; } }

    // 필요 경험치
    [SerializeField] float requiredExp;

    [SerializeField] int lv;
    public int LV { get { return lv; } set { lv = value; } }
    #endregion

    private void Start()
    {
        ui = FindObjectOfType<UIController>().GetComponent<UIController>();
        PlayerPrefs.DeleteKey("uid");
        PlayerPrefs.DeleteKey("characteruid");

        maxExp = 100;
        SetMaxExp();
        UI.SetEXPSlider(currentExp, maxExp);
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
        currentExp += exp;
        ChkExp();

        UI.SetEXPSlider(currentExp, maxExp);
    }

    private void ChkExp()
    {
        while (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            LV++;
            Debug.Log("레벨업");
            UI.SetLvText(LV);
            Debug.Log("현재 레벨 : " + LV);

            SetMaxExp();
        }
    }

    private void SetMaxExp()
    {
        float tmp = 0f;
        tmp = maxExp * (requiredExp * (LV - 1));
        maxExp += (int)tmp;

        Debug.Log("다음 필요 경험치 : " + maxExp);
    }
}
