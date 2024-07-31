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

    #region UIController �Ӽ�
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

    #region ��ų ����
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
                Debug.Log(www.downloadHandler.text + "�� ���� �Ϸ�");
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

    // ������ ���ݷ� ���
    private int lvPerPower = 1;
    #endregion

    #region ����ġ ����

    #region NowExp; ���� ����ġ
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

    #region MaxExp �ִ� ����ġ
    [SerializeField] private int maxExp;
    public int MaxExp
    {
        get { return maxExp; }
        set { maxExp = value; }
    }
    #endregion

    // �ʿ� ����ġ
    [SerializeField, Space(10)] private int requiredExp = 100;
    #endregion

    private void Start()
    {
        hp = maxHp;
        mp = maxMp;

        StartCoroutine(RecoverStats()); // �ڷ�ƾ ����
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

    #region ���̵� �޼ҵ�
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

    #region ���� ���� ����
    /// <summary>
    /// ����ġ �߰�
    /// </summary>
    /// <param name="exp"></param>
    public void SumEXP()
    {
        // ������ ������ ����ġ ���� Ȯ��
        while (nowExp >= maxExp)
        {
            nowExp -= maxExp;
            Lv++;
        }

        // ������ ���̽� �� �÷��̾� �����տ� ����� �� ����
        SaveUserData();
        UI.SetExpSlider(NowExp, MaxExp);
    }

    /// <summary>
    /// ������
    /// </summary>
    private void LevelUp()
    {
        UI.SetLvText();

        SetPlayerState();

        SetMaxExp();
    }

    /// <summary>
    /// �÷��̾� ������ ���� ���� �� UI �����̴� ����
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
    /// �ִ� ����ġ �缳��
    /// </summary>
    private void SetMaxExp()
    {
        maxExp = requiredExp * Lv;
    }
    #endregion

    #region LoadUserData()   ���� ���� �ҷ�����
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

    #region SaveUserData()   ���� ���� ����
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

                Debug.Log("���� ���� ���� �Ϸ�");
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }
    #endregion

    // ũ���� ���� ����
    #region LoadUserCredit() ���� ũ���� �ҷ����� (LoadUserData() �� ����)
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

                Debug.Log("���� ���� ���" + Credit);

                //UI ��Ʈ�ѷ��� ��� ���� ���� ���̰� �����Ұ�
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }*/
    #endregion

    #region SaveUserCredit() ���� ũ���� ����
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
                Debug.Log("���� ũ���� ���� �Ϸ�");
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }
    #endregion

}
