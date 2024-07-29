using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region �̱��� (Awake �Լ� ����)
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

        // �׽�Ʈ��
        PlayerPrefs.SetString("UserName", "�׽�Ʈ");
        PlayerPrefs.SetInt("Lv", 1);
        PlayerPrefs.SetInt("Exp", 0);
        PlayerPrefs.SetInt("Credit", 0);
        PlayerPrefs.Save();
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
    #region Lv ���� �Ӽ�
    [SerializeField] int lv;
    public int Lv
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

    #region MaxExp �ִ� ����ġ
    [SerializeField] int maxExp;
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

    [SerializeField] int credit;
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
/*      �̱����� awake �Լ� �ȿ����� ���� ���� �ٶ�
        PlayerPrefs.DeleteKey("uid");
        PlayerPrefs.DeleteKey("characteruid");

        PlayerPrefs.DeleteKey("UserName");
        PlayerPrefs.DeleteKey("Lv");
        PlayerPrefs.DeleteKey("Exp");
        PlayerPrefs.DeleteKey("Credit");

        // �׽�Ʈ��
        PlayerPrefs.SetString("UserName", "��������");
        PlayerPrefs.SetInt("Lv", 230);
        PlayerPrefs.SetInt("Exp", 230);
        PlayerPrefs.SetInt("Credit", 232323);
        PlayerPrefs.Save();*/
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

        // ������ ������ ����ġ ���� Ȯ��
        while (nowExp >= maxExp)
        {
            nowExp -= maxExp;
            LevelUp();
        }

        // ������ ���̽� �� �÷��̾� �����տ� ����� �� ����
        SaveUserData();
        UI.SetEXPSlider(NowExp, MaxExp);
    }

    /// <summary>
    /// �׽�Ʈ�� ��ư�� �Ҵ��� �Լ�
    /// </summary>
    /// <param name="gold"></param>
    public void SumCredit(int gold)
    {
        Credit += gold;
    }

    /// <summary>
    /// ������
    /// </summary>
    private void LevelUp()
    {
        lv++;
        UI.SetLvText(Lv);

        SetPlayerState();

        SetMaxExp();
    }

    /// <summary>
    /// �÷��̾� ������ ���� ���� �� UI �����̴� ����
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
        userName = PlayerPrefs.GetString("UserName");
        lv = PlayerPrefs.GetInt("Lv");
        nowExp = PlayerPrefs.GetInt("Exp");
        credit = PlayerPrefs.GetInt("Credit");
        Debug.Log(PlayerPrefs.GetInt("Credit"));
        SetPlayerState();

        SetMaxExp();
        UI.SetCharacterName(UserName);
        UI.SetLvText(Lv);
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
                #region �ҷ��� ������ ���� ����
                string tmp = www.downloadHandler.text;
                string[] data = tmp.Split(",");
                #endregion

                UserName = data[0];
                Lv = System.Convert.ToInt32(data[1]);
                NowExp = System.Convert.ToInt32(data[2]);
                Credit = System.Convert.ToInt32(data[3]);

                // ������ ���� �÷��̾� ���ݷ� ����
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

        WWWForm form = new WWWForm();
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
