using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region
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

    public string path;

    public string useCuid;

    [HideInInspector] public bool isPreferencePopup = false;

    #region
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

    [HideInInspector] public int sceneNumber;

    #region 캐릭터/슬롯 연동
    [HideInInspector] public GameObject selectObject;
    [HideInInspector] public int slotNum = 0;
    #endregion

    #region UIController 속성
    UIController ui;
    public UIController UI { get { return ui; } set { ui = value; } }
    #endregion

    #region 경험치 관련
    #region LV 레벨 속성
    int lv;
    public int LV
    {
        get { return lv; }
        set
        {
            lv = value;
            // ㅁㅇㄻㄴㅇㄻㄴㅇㄹ
        }
    }
    #endregion

    #region NowExp; 현재 경험치
    int nowExp;
    public int NowExp
    {
        get { return nowExp; }
        set
        {
            nowExp = value;
            // asdf
        }
    }
    #endregion

    #region MaxExp; 최대 경험치
    int maxExp;
    public int MaxExp
    { get { return maxExp; } set { maxExp = value; } }
    #endregion

    // 필요 경험치
    [SerializeField, Space(10)] int requiredExp = 100;
    #endregion

    #region 유저 이름
    string userName;
    public string UserName { get { return userName; } set { userName = value; } }
    #endregion

    #region Credit 유저 크레딧 관련

    int credit;
    public int Credit { get { return credit; } set { credit = value; SaveUserCredit(); } }

    #endregion

    #region 플레이어 설정
    PlayerCombat player;
    public PlayerCombat Player { get { return player; } set { player = value; } }

    // 레벨당 공격력 배수
    int lvPerPower = 1;
    #endregion

    private void Start()
    {
        PlayerPrefs.DeleteKey("uid");
        PlayerPrefs.DeleteKey("characteruid");
    }

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

    #region 유저 정보 세팅
    /// <summary>
    /// 경험치 추가
    /// </summary>
    /// <param name="exp"></param>
    public void SumEXP(int exp)
    {
        nowExp += exp;
        ChkExp();

        SaveUserData();
        UI.SetEXPSlider(NowExp, MaxExp);
    }

    /// <summary>
    /// 레벨업 가능한 경험치 인지 확인
    /// </summary>
    private void ChkExp()
    {
        while (nowExp >= maxExp)
        {
            nowExp -= maxExp;
            LevelUp();
        }
    }

    /// <summary>
    /// 레벨업
    /// </summary>
    private void LevelUp()
    {
        lv++;
        UI.SetLvText(lv);

        SetPlayerState();

        SetMaxExp();
    }

    /// <summary>
    /// 플레이어 레벨별 공격력 세팅
    /// </summary>
    private void SetPlayerState()
    {
        player.AtkPower = lv * lvPerPower;
        player.MaxHp = 50 + (50 * LV);
        Player.MaxMp = 100 + (100 * LV);

        player.CurrentHp = player.MaxHp;
        player.CurrentMp = player.MaxMp;

        UI.SetHpSlider(player.MaxHp, player.MaxHp);
        UI.SetMpSlider(player.MaxMp, player.MaxMp);
    }

    /// <summary>
    /// 최대 경험치 재설정
    /// </summary>
    private void SetMaxExp()
    {
        maxExp = requiredExp * LV;
    }
    #endregion

    #region LoadUserData()   유저 정보 불러오기
    public void LaodUserData()
    {
        StartCoroutine(LoadeUserDatas());
    }

    IEnumerator LoadeUserDatas()
    {
        string url = path + "loaduserdata.php";
        string cuid = PlayerPrefs.GetString("characteruid");

        WWWForm form = new();
        form.AddField("cuid", useCuid);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                #region 불러온 데이터 변수 저장
                string tmp = www.downloadHandler.text;
                string[] data = tmp.Split(",");
                #endregion

                UserName = data[0];
                LV = System.Convert.ToInt32(data[1]);
                NowExp = System.Convert.ToInt32(data[2]);

                // 레벨에 맞춰 플레이어 공격력 설정
                SetPlayerState();

                SetMaxExp();
                UI.SetCharacterName(UserName);
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

    #region SaveUserData()   유저 정보 저장
    private void SaveUserData()
    {
        StartCoroutine(SaveUserDatas());
    }

    IEnumerator SaveUserDatas()
    {
        string url = gm.path + "saveuserdata.php";
        string cuid = PlayerPrefs.GetString("characteruid");

        WWWForm form = new WWWForm();
        form.AddField("cuid", useCuid);
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
        form.AddField("cuid", useCuid);

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
        form.AddField("cuid", useCuid);
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
}
