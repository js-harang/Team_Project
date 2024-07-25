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
    // ��üȭ�� / â��� ����
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

    #region ĳ����/���� ����
    [HideInInspector] public GameObject selectObject;
    [HideInInspector] public int slotNum = 0;
    #endregion

    #region UIController �Ӽ�
    UIController ui;
    public UIController UI { get { return ui; } set { ui = value; } }
    #endregion

    #region ����ġ ����
    #region LV ���� �Ӽ�
    int lv;
    public int LV
    {
        get { return lv; }
        set
        {
            lv = value;
            // ������������������
        }
    }
    #endregion

    #region NowExp; ���� ����ġ
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

    #region MaxExp; �ִ� ����ġ
    int maxExp;
    public int MaxExp
    { get { return maxExp; } set { maxExp = value; } }
    #endregion

    // �ʿ� ����ġ
    [SerializeField, Space(10)] int requiredExp = 100;
    #endregion

    #region ���� �̸�
    string userName;
    public string UserName { get { return userName; } set { userName = value; } }
    #endregion

    #region Credit ���� ũ���� ����

    int credit;
    public int Credit { get { return credit; } set { credit = value; SaveUserCredit(); } }

    #endregion

    #region �÷��̾� ����
    PlayerCombat player;
    public PlayerCombat Player { get { return player; } set { player = value; } }

    // ������ ���ݷ� ���
    int lvPerPower = 1;
    #endregion

    private void Start()
    {
        PlayerPrefs.DeleteKey("uid");
        PlayerPrefs.DeleteKey("characteruid");
    }

    #region MoveScene() ���̵� �޼ҵ�
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
    public void SumEXP(int exp)
    {
        nowExp += exp;
        ChkExp();

        SaveUserData();
        UI.SetEXPSlider(NowExp, MaxExp);
    }

    /// <summary>
    /// ������ ������ ����ġ ���� Ȯ��
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
    /// ������
    /// </summary>
    private void LevelUp()
    {
        lv++;
        UI.SetLvText(lv);

        SetPlayerState();

        SetMaxExp();
    }

    /// <summary>
    /// �÷��̾� ������ ���ݷ� ����
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
    /// �ִ� ����ġ �缳��
    /// </summary>
    private void SetMaxExp()
    {
        maxExp = requiredExp * LV;
    }
    #endregion

    #region LoadUserData()   ���� ���� �ҷ�����
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
                #region �ҷ��� ������ ���� ����
                string tmp = www.downloadHandler.text;
                string[] data = tmp.Split(",");
                #endregion

                UserName = data[0];
                LV = System.Convert.ToInt32(data[1]);
                NowExp = System.Convert.ToInt32(data[2]);

                // ������ ���� �÷��̾� ���ݷ� ����
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

    #region SaveUserData()   ���� ���� ����
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
    #region LoadUserCredit() ���� ũ���� �ҷ�����
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

                Debug.Log("���� ���� ���" + Credit);

                //UI ��Ʈ�ѷ��� ��� ���� ���� ���̰� �����Ұ�
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }
    #endregion

    #region SaveUserCredit() ���� ũ���� ����
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
