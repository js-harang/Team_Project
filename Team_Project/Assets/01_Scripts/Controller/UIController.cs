using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region 키보드 입력시 동작할 UI들
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject statusUI;
    [SerializeField] GameObject escMenuUI;
    [SerializeField] Canvas gameUI;
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    #region 플레이어 슬라이더 바
    [Space(10)]
    [SerializeField] Slider hpSld;
    [SerializeField] Slider mpSld;
    [SerializeField] Slider expSld;
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    [SerializeField] TextMeshProUGUI lvText;
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

    // 슬라이더 세팅
    #region HP, MP, EXP 변경시 세팅
    public void SetHpSlider(float currentHp, int maxHp)
    {
        hpSld.value = currentHp / maxHp;
    }
    public void SetMpSlider(float currentMp, int maxMp)
    {
        mpSld.value = currentMp / maxMp;
    }
    public void SetEXPSlider(float currentExp, int maxExp)
    {
        expSld.value = currentExp / maxExp;
    }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // 텍스트 세팅
    #region LV, Credit 변경시 세팅
    public void SetLvText(int lv)
    {
        lvText.text = "Lv." + lv;
    }
    public void SetCreditText(int credit)
    {

    }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // UI 관련
    #region UI 활성, 비활성 관련
    // 스테이터스 UI 활성 / 비활성화
    public void StatusOnOff()
    {
        if (statusUI.activeSelf)
        {
            statusUI.SetActive(false);
            return;
        }
        statusUI.SetActive(true);
    }

    // 인벤토리 UI 활성 / 비활성화
    public void InventoryOnOff()
    {
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
            return;
        }
        inventoryUI.SetActive(true);
    }

    // ESC 메뉴 UI 활성 / 비활성화
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

    // 씬이동
    #region 캐릭터 선택, 마을 이동
    // 캐릭터 선택화면으로 이동
    public void ToCharacterLobby(int sceneNumber)
    {
        GameManager.gm.MoveScene(sceneNumber);
    }

    // 캐릭터 사망시 버튼 누르면 마을로
    public void ToTown(int sceneNumber)
    {
        GameManager.gm.MoveScene(sceneNumber);
    }
    #endregion
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // 게임오버시 호출되는 메소드(게임오버 매뉴 활성화)
    public void GameOverUI()
    {
        gameOverUI.SetActive(true);
    }
/*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    // 게임 종료
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
