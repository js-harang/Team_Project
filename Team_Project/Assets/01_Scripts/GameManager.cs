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
            AtkPower = lv * lvPerPower;
            MaxHp = 50 + (50 * lv);
            MaxMp = 100 + (100 * lv);
            MaxExp = requiredExp * lv;

            StartCoroutine(SaveUserData(0, lv));
        }
    }

    private int exp;
    public int Exp
    {
        get { return exp; }
        set
        {
            exp = value;
            StartCoroutine(SaveUserData(1, exp));
        }
    }

    private int credit;
    public int Credit
    {
        get { return credit; }
        set
        {
            credit = value;
            StartCoroutine(SaveUserData(2, credit));
        }
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
    public int AtkPower
    {
        get { return atkPower; }
        set { atkPower = value; }
    }

    private int maxHp;
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    private int maxMp;
    public int MaxMp
    {
        get { return maxMp; }
        set { maxMp = value; }
    }

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
    public int lvPerPower = 1;
    #endregion

    #region 경험치 관련
    [SerializeField] private int maxExp;
    public int MaxExp
    {
        get { return maxExp; }
        set { maxExp = value; }
    }

    // 필요 경험치
    [SerializeField, Space(10)] private int requiredExp = 100;
    #endregion

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

    #region LoadUserData()   유저 정보 불러오기
    public void LaodUserData()
    {
        UI.SetHpSlider(player.MaxHp, player.MaxHp);
        UI.SetMpSlider(player.MaxMp, player.MaxMp);
        UI.SetExpSlider(Exp, MaxExp);
    }
    #endregion

    #region 유저 정보 저장
    IEnumerator SaveUserData(int type, int value)
    {
        string url = path + "saveuserdata.php";
        string cuid = PlayerPrefs.GetString("characteruid");

        WWWForm form = new();
        form.AddField("cuid", UnitUid);
        form.AddField("type", type);
        form.AddField("value", value);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
        }
    }
    #endregion
}
