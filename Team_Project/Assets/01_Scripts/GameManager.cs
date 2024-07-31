using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
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
        PlayerPrefs.DeleteKey("uid");
        PlayerPrefs.DeleteKey("characteruid");

        PlayerPrefs.DeleteKey("UserName");
        PlayerPrefs.DeleteKey("Lv");
        PlayerPrefs.DeleteKey("Exp");
        PlayerPrefs.DeleteKey("Credit");

        // 테스트용
        PlayerPrefs.SetString("UserName", "테스트");
        PlayerPrefs.SetInt("Lv", 1);
        PlayerPrefs.SetInt("Exp", 0);
        PlayerPrefs.SetInt("Credit", 0);
        PlayerPrefs.Save();
    }

    public string path;

    public string useCuid;

    [HideInInspector] public bool isPreferencePopup = false;

    [HideInInspector] public int sceneNumber;

    #region screen setting
    private int isFullScreen;
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

    #region 캐릭터/슬롯 연동
    [HideInInspector] public GameObject selectObject;
    [HideInInspector] public int slotNum = 0;
    #endregion

    #region UIController 속성
    UIController ui;
    public UIController UI { get { return ui; } set { ui = value; } }
    #endregion

    #region player info
    public string uid;
    private string Uid
    {
        get { return uid; }
        set { uid = value; }
    }

    public string cUid;
    private string Cuid
    {
        get { return cUid; }
        set { cUid = value; }
    }

    public string userName;
    private string UserName
    {
        get { return userName; }
        set { userName = value; }
    }

    public int lv;
    private int Lv
    {
        get { return lv; }
        set { lv = value; }
    }
    #endregion

    #region player stats
    public int maxHp = 100;
    public int maxMp = 100;
    public float recoveryRate = 1f;

    public int hp;
    private int Hp
    {
        get { return hp; }
        set { hp = Mathf.Clamp(value, 0, maxHp); }
    }

    public int mp;
    private int Mp
    {
        get { return mp; }
        set { mp = Mathf.Clamp(value, 0, maxMp); }
    }
    #endregion

    #region 경험치 관련

    #region NowExp; 현재 경험치
    [SerializeField] int nowExp;
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

    #region MaxExp 최대 경험치
    [SerializeField] int maxExp;
    public int MaxExp
    {
        get { return maxExp; }
        set { maxExp = value; }
    }
    #endregion

    // 필요 경험치
    [SerializeField, Space(10)] int requiredExp = 100;
    #endregion

    #region Credit 유저 크레딧 관련

    [SerializeField] int credit;
    public int Credit
    {
        get { return credit; }
        set { credit = value; SaveUserCredit(); }
    }

    #endregion

    #region 플레이어 설정
    PlayerCombat player;
    public PlayerCombat Player
    {
        get { return player; }
        set { player = value; }
    }

    // 레벨당 공격력 배수
    int lvPerPower = 1;
    #endregion

    private void Start()
    {
        hp = maxHp;
        mp = maxMp;
        /*      싱글톤의 awake 함수 안에있음 추후 수정 바람
                PlayerPrefs.DeleteKey("uid");
                PlayerPrefs.DeleteKey("characteruid");

                PlayerPrefs.DeleteKey("UserName");
                PlayerPrefs.DeleteKey("Lv");
                PlayerPrefs.DeleteKey("Exp");
                PlayerPrefs.DeleteKey("Credit");

                // 테스트용
                PlayerPrefs.SetString("UserName", "나나나나");
                PlayerPrefs.SetInt("Lv", 230);
                PlayerPrefs.SetInt("Exp", 230);
                PlayerPrefs.SetInt("Credit", 232323);
                PlayerPrefs.Save();*/
        StartCoroutine(RecoverStats()); // 코루틴 시작
    }

    private IEnumerator RecoverStats()
    {
        while (true)
        {
            if (hp < maxHp)
                hp += Mathf.RoundToInt(recoveryRate);

            if (mp < maxMp)
                mp += Mathf.RoundToInt(recoveryRate);

            yield return new WaitForSeconds(1f);
        }
    }

    #region 씬이동 메소드
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

        // 레벨업 가능한 경험치 인지 확인
        while (nowExp >= maxExp)
        {
            nowExp -= maxExp;
            LevelUp();
        }

        // 데이터 베이스 및 플레이어 프리팹에 변경된 값 저장
        SaveUserData();
        UI.SetEXPSlider(NowExp, MaxExp);
    }

    /// <summary>
    /// 테스트용 버튼에 할당할 함수
    /// </summary>
    /// <param name="gold"></param>
    public void SumCredit(int gold)
    {
        Credit += gold;
    }

    /// <summary>
    /// 레벨업
    /// </summary>
    private void LevelUp()
    {
        lv++;
        UI.SetLvText(Lv);

        SetPlayerState();

        SetMaxExp();
    }

    /// <summary>
    /// 플레이어 레벨별 스텟 설정 및 UI 슬라이더 설정
    /// </summary>
    private void SetPlayerState()
    {
        player.AtkPower = lv * lvPerPower;
        player.MaxHp = 50 + (50 * Lv);
        Player.MaxMp = 100 + (100 * Lv);

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
        maxExp = requiredExp * Lv;
    }
    #endregion

    #region LoadUserData()   유저 정보 불러오기
    public void LaodUserData()
    {
        userName = PlayerPrefs.GetString("UserName");
        lv = PlayerPrefs.GetInt("Lv");
        nowExp = PlayerPrefs.GetInt("Exp");
        credit = PlayerPrefs.GetInt("Credit");
        Debug.Log(PlayerPrefs.GetInt("Credit"));
        SetPlayerState();

        SetMaxExp();
        UI.SetCharacterName(UserName);
        //UI.SetLvText(Lv);
        UI.SetEXPSlider(NowExp, MaxExp);

        // StartCoroutine(LoadeUserDatas());
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
                Lv = System.Convert.ToInt32(data[1]);
                NowExp = System.Convert.ToInt32(data[2]);
                Credit = System.Convert.ToInt32(data[3]);

                // 레벨에 맞춰 플레이어 공격력 설정
                SetPlayerState();

                SetMaxExp();
                UI.SetCharacterName(UserName);
                UI.SetLvText(Lv);
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

        WWWForm form = new();
        form.AddField("cuid", useCuid);
        form.AddField("lv", Lv);
        form.AddField("exp", NowExp);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                PlayerPrefs.SetInt("Lv", Lv);
                PlayerPrefs.SetInt("Exp", NowExp);
                PlayerPrefs.Save();

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
    #region LoadUserCredit() 유저 크레딧 불러오기 (LoadUserData() 와 통합)
    /*
    public void LoadUserCredit()
    {
        // StartCoroutine(LoadCredit());
    }*/

    /*IEnumerator LoadCredit()
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
    }*/
    #endregion

    #region SaveUserCredit() 유저 크레딧 저장
    private void SaveUserCredit()
    {
        StartCoroutine(SaveCreditCoroutine());
    }

    IEnumerator SaveCreditCoroutine()
    {
        string url = gm.path + "saveusercredit.php";
        string cuid = PlayerPrefs.GetString("characteruid");

        WWWForm form = new();
        form.AddField("cuid", useCuid);
        form.AddField("credit", Credit);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                PlayerPrefs.SetInt("Credit", Credit);
                PlayerPrefs.Save();
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
