using UnityEngine;
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

    #region ����ġ ����
    [SerializeField] float currentExp;
    public float CurrentExp { get { return currentExp; } set { currentExp = value; } }

    [SerializeField] int maxExp;
    public int MaxExp { get { return maxExp; } set { maxExp = value; } }

    // �ʿ� ����ġ
    [SerializeField] float requiredExp;

    [SerializeField] int lv;
    public int LV{ get { return lv; } set { lv = value; } }
    #endregion

    private void Start()
    {
        ui = FindObjectOfType<UIController>().GetComponent<UIController>();
        PlayerPrefs.DeleteKey("uid");
        PlayerPrefs.DeleteKey("characteruid");
        maxExp = 100;
        SetMaxExp();
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

    void ChkExp()
    {
        if (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            LV++;

            SetMaxExp();
        }
        ui.SetEXPSlider(currentExp, maxExp);
    }
    void SetMaxExp()
    {
        float tmp = 0f;
        int i = LV - 1;
        tmp = maxExp * (requiredExp * i);
        maxExp += (int)tmp;

        Debug.Log("maxExp : " + maxExp);
    }
}
