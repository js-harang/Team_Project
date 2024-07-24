using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : MonoBehaviour
{
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region GM �̱��� (Awake �޼ҵ� ���� �������)
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

    // ȯ�漳�� ���õǾ� �ִ� �ɼ�
    [HideInInspector] public GameObject selectObject;
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region ��ũ�� ��� ����

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

    [HideInInspector] public bool isPreferencePopup = false;

    public string path;

    [HideInInspector] public int slotNum = 0;
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // UIController �Ӽ�
    #region UIController �Ӽ�
    UIController ui;
    public UIController UI { get { return ui; } set { ui = value; } }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // ���� ����, ����ġ �Ӽ�
    #region ����ġ ����

    #region LV ���� �Ӽ�
    [SerializeField] int lv;
    public int LV 
    {
        get { return lv; } 
        set { lv = value; }
    }
    #endregion

    // �ʿ� ����ġ
    [SerializeField] int requiredExp;

    #region NowExp; ���� ����ġ
    [SerializeField] int nowExp;
    public int NowExp
    { 
        get { return nowExp; } 
        set { nowExp = value; }
    }
    #endregion

    #region MaxExp; �ִ� ����ġ
    [SerializeField] int maxExp;
    public int MaxExp
    { get { return maxExp; } set { maxExp = value; } }
    #endregion

    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // ���� ũ���� �Ӽ�
    #region Credit ���� ũ���� ����
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
        // UI ��Ʈ�ѷ��� �����ɶ� �ڵ����� ������
        // ui = FindObjectOfType<UIController>().GetComponent<UIController>();
        PlayerPrefs.DeleteKey("uid");
        PlayerPrefs.DeleteKey("characteruid");

        // ���߿� UI ��Ʈ�ѷ� �� �ű��
        GameManager.gm.LaodUserData();
        GameManager.gm.LoadUserCredit();
    }
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // ���̵�
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
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // ���� ����, ����ġ ����
    #region ���� ���� ����
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

            Debug.Log("������");
            Debug.Log("���� ���� : " + LV);

            SetMaxExp();
        }
    }

    private void SetMaxExp()
    {
        maxExp = requiredExp * LV;

        Debug.Log("���� �ʿ� ����ġ : " + maxExp);
    }

    #endregion

    #region SaveUserData() ���� ���� ����
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
                Debug.Log("���� ���� ���� �Ϸ�");
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }
    #endregion

    #region LoadUserData() ���� ���� �ҷ�����
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

                Debug.Log("���� ����" + LV);
                Debug.Log("���� ����ġ" + NowExp);

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
        form.AddField("cuid", 0000000018);

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
        form.AddField("cuid", 0000000018);
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
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
}