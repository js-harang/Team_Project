using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Ű���� �Է½� ������ UI��
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject statusUI;
    [SerializeField] GameObject escMenuUI;
    [SerializeField] Canvas gameUI;
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region �÷��̾� �����̴� ��
    [Space(10)]
    [SerializeField] Slider hpSld;
    [SerializeField] Slider mpSld;
    [SerializeField] Slider expSld;
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region �ؽ�Ʈ ����
    [Space(10)]
    [SerializeField] TextMeshProUGUI lv_Text;

    [Space (10)]
    [SerializeField] TextMeshProUGUI currentHp_Text;
    [SerializeField] TextMeshProUGUI maxHp_Text;

    [Space (10)]
    [SerializeField] TextMeshProUGUI currentMp_Text;
    [SerializeField] TextMeshProUGUI maxMp_Text;

    [Space (10)]
    [SerializeField] TextMeshProUGUI currentExp_Text;
    [SerializeField] TextMeshProUGUI maxExp_Text;
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    [Space(10)]
    [SerializeField] GameObject gameOverUI;
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    PlayerState pState;
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    [SerializeField, Space(10)] GameObject preferencePopup;
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    private void Start()
    {
        pState = FindObjectOfType<PlayerState>().GetComponent<PlayerState>();

        GameManager.gm.UI = this.GetComponent<UIController>();
        GameManager.gm.LaodUserData();
        GameManager.gm.LoadUserCredit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pState.UnitState == UnitState.Interact || GameManager.gm.isPreferencePopup)
                return;

            EscMenuOnOff();
        }

        switch (Input.inputString)
        {
            case "u":
            case "U":
                StatusOnOff();
                break;

            case "i":
            case "I":
                InventoryOnOff();
                break;
        }
    }
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // �����̴� ����
    #region HP, MP, EXP ����� ����
    public void SetHpSlider(float currentHp, int maxHp)
    {
        currentHp_Text.text = "" + currentHp;
        maxHp_Text.text = " / " + maxHp;
        hpSld.value = currentHp / maxHp;
    }
    public void SetMpSlider(float currentMp, int maxMp)
    {
        currentMp_Text.text = "" + currentMp;
        maxMp_Text.text = " / " + maxMp;
        mpSld.value = currentMp / maxMp;
    }
    public void SetEXPSlider(float currentExp, int maxExp)
    {
        currentExp_Text.text = "" + currentExp;
        maxExp_Text.text = " / " + maxExp;
        expSld.value = currentExp / maxExp;
    }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // �ؽ�Ʈ ����
    #region LV, Credit ����� ����
    public void SetLvText(int lv)
    {
        lv_Text.text = "Lv." + lv;
    }
    public void SetCreditText(int credit)
    {

    }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // UI ����
    #region UI Ȱ��, ��Ȱ�� ����
    // �������ͽ� UI Ȱ�� / ��Ȱ��ȭ
    public void StatusOnOff()
    {
        if (statusUI.activeSelf)
        {
            statusUI.SetActive(false);
            return;
        }
        statusUI.SetActive(true);
    }

    // �κ��丮 UI Ȱ�� / ��Ȱ��ȭ
    public void InventoryOnOff()
    {
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
            return;
        }
        inventoryUI.SetActive(true);
    }

    // ESC �޴� UI Ȱ�� / ��Ȱ��ȭ
    public void EscMenuOnOff()
    {
        if (escMenuUI.activeSelf)
        {
            escMenuUI.SetActive(false);
            pState.UnitState = UnitState.Idle;
            return;
        }
        escMenuUI.SetActive(true);
        pState.UnitState = UnitState.Wait;
    }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // ���̵�
    #region ĳ���� ����, ���� �̵�
    // ĳ���� ����ȭ������ �̵�
    public void ToCharacterLobby(int sceneNumber)
    {
        GameManager.gm.MoveScene(sceneNumber);
    }

    // ĳ���� ����� ��ư ������ ������
    public void ToTown(int sceneNumber)
    {
        GameManager.gm.MoveScene(sceneNumber);
    }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // ���ӿ����� ȣ��Ǵ� �޼ҵ�(���ӿ��� �Ŵ� Ȱ��ȭ)
    public void GameOverUI()
    {
        gameOverUI.SetActive(true);
    }
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // ���� ����
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    public void PreferencesBtn()
    {
        EscMenuOnOff();

        GameManager.gm.isPreferencePopup = true;
        preferencePopup.SetActive(GameManager.gm.isPreferencePopup);
    }
}
