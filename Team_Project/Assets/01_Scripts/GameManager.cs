using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : MonoBehaviour
{
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region GM 싱글톤 (Awake 메소드 여기 들어있음)
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
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    
    [HideInInspector] public int sceneNumber;
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // 환경설정 선택되어 있는 옵션
    [HideInInspector] public GameObject selectObject;
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region 스크린 모드 설정

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

    #endregion

    [HideInInspector] public bool isPreferencePopup = false;

    public string path;

    [HideInInspector] public int slotNum = 0;
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // UIController 속성
    #region UIController 속성
    UIController ui;
    public UIController UI { get { return ui; } set { ui = value; } }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // 유저 레벨, 경험치 속성
    #region 경험치 관련

    #region LV 레벨 속성
    [SerializeField] int lv;
    public int LV 
    {
        get { return lv; } 
        set { lv = value; }
    }
    #endregion

    // 필요 경험치
    [SerializeField] int requiredExp;

    #region NowExp; 현재 경험치
    [SerializeField] int nowExp;
    public int NowExp
    { 
        get { return nowExp; } 
        set { nowExp = value; }
    }
    #endregion

    #region MaxExp; 최대 경험치
    [SerializeField] int maxExp;
    public int MaxExp
    { get { return maxExp; } set { maxExp = value; } }
    #endregion

    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // 유저 크레딧 속성
    #region Credit 유저 크레딧 관련
    int credit;
    public int Credit
    {
        get { return credit; }
        set { credit = value; SaveUserCredit(); }
    }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    private void Start()
    {
        // UI 컨트롤러가 생성될때 자동으로 참조됨
        // ui = FindObjectOfType<UIController>().GetComponent<UIController>();
        PlayerPrefs.DeleteKey("uid");
        PlayerPrefs.DeleteKey("characteruid");

        // 나중에 UI 컨트롤러 로 옮길것
        GameManager.gm.LaodUserData();
        GameManager.gm.LoadUserCredit();
    }
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // 씬이동
    #region MoveScene() 씬이동 메소드
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
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // 유저 레벨, 경험치 관련
    #region 유저 정보 세팅
    public void SumEXP(int exp)
    {
        NowExp += exp;
        ChkExp();

        SaveUserData();
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

    #endregion

    #region SaveUserData() 유저 정보 저장
    private void SaveUserData()
    {
        StartCoroutine(SaveUserDatas());
    }
    IEnumerator SaveUserDatas()
    {
        string url = gm.path + "saveuserdata.php";
        string cuid = PlayerPrefs.GetString("characteruid");

        WWWForm form = new WWWForm();
        form.AddField("cuid", 0000000018);
        form.AddField("lv", LV);
        form.AddField("exp", NowExp);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                Debug.Log("유저 정보 저장 완료");
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }
    #endregion

    #region LoadUserData() 유저 정보 불러오기
    public void LaodUserData()
    {
/*
        LV = PlayerPrefs.GetInt("LV");
        NowExp = PlayerPrefs.GetInt("NowExp");
*/
        StartCoroutine(LoadeUserDatas());
    }

    IEnumerator LoadeUserDatas()
    {
        string url = gm.path + "loaduserdata.php";
        string cuid = PlayerPrefs.GetString("characteruid");

        WWWForm form = new WWWForm();
        form.AddField("cuid", 0000000018);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                string tmp = www.downloadHandler.text;
                string[] data = tmp.Split(",");
                LV = System.Convert.ToInt32(data[0]);
                NowExp = System.Convert.ToInt32(data[1]);

                Debug.Log("현재 레벨" + LV);
                Debug.Log("현재 경험치" + NowExp);

                SetMaxExp();
                UI.SetLvText(LV);
                UI.SetEXPSlider(NowExp, maxExp);
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // 크레딧 저장 관련
    #region LoadUserCredit() 유저 크레딧 불러오기
    public void LoadUserCredit()
    {
        StartCoroutine(LoadCredit());
    }
    IEnumerator LoadCredit()
    {
        string url = gm.path + "loadusercredit.php";
        string cuid = PlayerPrefs.GetString("characteruid");

        WWWForm form = new WWWForm();
        form.AddField("cuid", 0000000018);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                Credit = System.Convert.ToInt32(www.downloadHandler.text);

                Debug.Log("현재 보유 골드" + Credit);

                //UI 컨트롤러에 골드 보유 수량 보이게 설정할것
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }
    #endregion

    #region SaveUserCredit() 유저 크레딧 저장
    private void SaveUserCredit()
    {
        StartCoroutine(SaveCredit());
    }
    IEnumerator SaveCredit()
    {
        string url = gm.path + "saveusercredit.php";
        string cuid = PlayerPrefs.GetString("characteruid");

        WWWForm form = new WWWForm();
        form.AddField("cuid", 0000000018);
        form.AddField("credit", Credit);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                Debug.Log("유저 크레딧 저장 완료");
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
}