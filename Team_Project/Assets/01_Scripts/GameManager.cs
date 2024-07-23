using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

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

    // ȯ�漳�� ���õǾ� �ִ� �ɼ�
    [HideInInspector] public GameObject selectObject;

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

    [HideInInspector] public bool isPreferencePopup = false;

    public string path;

    UIController ui;
    public UIController UI { get { return ui; } set { ui = value; } }

    [HideInInspector] public int slotNum = 0;

    #region ����ġ ����
    [SerializeField] int nowExp;
    public int NowExp
    { get { return nowExp; } set { nowExp = value; SetNowExp(); } }

    [SerializeField] int maxExp;
    public int MaxExp
    { get { return maxExp; } set { maxExp = value; } }

    // �ʿ� ����ġ
    [SerializeField] int requiredExp;

    [SerializeField] int lv;
    public int LV 
    { get { return lv; } set { lv = value; SetLevel(); } }
    #endregion

    private void Start()
    {
        // UI ��Ʈ�ѷ��� �����ɶ� �ڵ����� ������
        // ui = FindObjectOfType<UIController>().GetComponent<UIController>();
        PlayerPrefs.DeleteKey("uid");
        PlayerPrefs.DeleteKey("characteruid");

        LaodUserData();
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

    #region ���� ���� ����
    public void SumEXP(int exp)
    {
        NowExp += exp;
        ChkExp();

        PlayerPrefs.Save();
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

    IEnumerator SaveUserData()
    {
        string url = gm.path + "saveuserdata";
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

    #region ���� ���� ����
    private void LaodUserData()
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
    private void SetLevel()
    {
        PlayerPrefs.SetInt("LV", LV);
    }
    private void SetNowExp()
    {
        PlayerPrefs.SetInt("NowExp", NowExp);
    }
    #endregion
}