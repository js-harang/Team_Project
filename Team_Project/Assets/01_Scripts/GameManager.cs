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

    #region character/slot
    [HideInInspector] public GameObject selectObject;
    [HideInInspector] public int slotNum = -1;
    #endregion

    #region UIController 속성
    private UIController ui;
    public UIController UI { get { return ui; } set { ui = value; } }
    #endregion

    #region player info
    private string uid;
    public string Uid
    {
        get { return uid; }
        set { uid = value; }
    }

    private string unitUid;
    public string UnitUid
    {
        get { return unitUid; }
        set { unitUid = value; }
    }

    private string unitName;
    public string UnitName
    {
        get { return unitName; }
        set { unitName = value; }
    }

    private int lv;
    public int Lv
    {
        get { return lv; }
        set
        {
            lv = value;
            LevelUp();
        }
    }

    private int exp;
    public int Exp
    {
        get { return exp; }
        set
        {
            exp = value;
            SumEXP();
        }
    }

    private int credit;
    public int Credit
    {
        get { return credit; }
        set { credit = value; }
    }

    private string skill;
    public string Skill
    {
        get { return skill; }
        set
        {
            skill = value;
            StartCoroutine(SaveSkill());
        }
    }

    #region 스킬 저장
    IEnumerator SaveSkill()
    {
        string url = path + "saveskill.php";

        WWWForm form = new WWWForm();
        form.AddField("cuid", unitUid);
        form.AddField("num", skill);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
                Debug.Log(www.downloadHandler.text + "로 저장 완료");
            else
                Debug.Log(www.error);
        }
    }
    #endregion

    #endregion

    #region player stats

    private PlayerCombat player;
    public PlayerCombat Player
    {
        get { return player; }
        set { player = value; }
    }

    private int atkPower;
    public int AtkPower { get { return atkPower; } set { atkPower = value; } }

    private int maxHp;
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }

    private int maxMp;
    public int MaxMp { get { return maxMp; } set { maxMp = value; } }

    public float recoveryRate = 1f;

    private int hp;
    public int Hp
    {
        get { return hp; }
        set { hp = Mathf.Clamp(value, 0, maxHp); }
    }

    private int mp;
    public int Mp
    {
        get { return mp; }
        set { mp = Mathf.Clamp(value, 0, maxMp); }
    }

    // 레벨당 공격력 배수
    private int lvPerPower = 1;
    #endregion

    #region 경험치 관련

    #region NowExp; 현재 경험치
    [SerializeField] private int nowExp;
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
    [SerializeField] private int maxExp;
    public int MaxExp
    {
        get { return maxExp; }
        set { maxExp = value; }
    }
    #endregion

    // 필요 경험치
    [SerializeField, Space(10)] private int requiredExp = 100;
    #endregion

    private void Start()
    {
        hp = maxHp;
        mp = maxMp;

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
    public void SumEXP()
    {
        // 레벨업 가능한 경험치 인지 확인
        while (nowExp >= maxExp)
        {
            nowExp -= maxExp;
            Lv++;
        }

        // 데이터 베이스 및 플레이어 프리팹에 변경된 값 저장
        SaveUserData();
        UI.SetExpSlider(NowExp, MaxExp);
    }

    /// <summary>
    /// 레벨업
    /// </summary>
    private void LevelUp()
    {
        UI.SetLvText();

        SetPlayerState();

        SetMaxExp();
    }

    /// <summary>
    /// 플레이어 레벨별 스텟 설정 및 UI 슬라이더 설정
    /// </summary>
    public void SetPlayerState()
    {
        // player.AtkPower = lv * lvPerPower;
        AtkPower = lv * lvPerPower;

        // player.MaxHp = 50 + (50 * Lv);
        MaxHp = 50 + (50 * Lv);

        // Player.MaxMp = 100 + (100 * Lv);
        MaxMp = 100 + (100 * Lv);

    /*  player.CurrentHp = player.MaxHp;
        player.CurrentMp = player.MaxMp;  */
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
/*        SetPlayerState();
        SetMaxExp();*/

/*        UI.SetUnitName();
        UI.SetLvText();*/

        UI.SetHpSlider(player.MaxHp, player.MaxHp);
        UI.SetMpSlider(player.MaxMp, player.MaxMp);
        UI.SetExpSlider(NowExp, MaxExp);
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
