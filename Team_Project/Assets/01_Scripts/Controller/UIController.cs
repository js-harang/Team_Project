using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // 키보드 입력시 동작할 UI들
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject statusUI;
    [SerializeField] GameObject escMenuUI;
    [SerializeField] Canvas gameUI;

    [SerializeField]
    GameObject gameOverUI;

    PlayerState pState;

    private void Start()
    {
        pState = FindObjectOfType<PlayerState>().GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameUI.enabled)                // InteractController에서 비활성화한 UI 캔버스를 확인후 동작
            {
                gameUI.enabled = true;
                return;
            }

            EscButtonOnOff();
        }

        switch (Input.inputString)
        {
            case "u":
                StatusOnOff();
                break;

            case "i":
                InventoryOnOff();
                break;
        }
    }

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
    public void EscButtonOnOff()
    {
        if (escMenuUI.activeSelf)
        {
            escMenuUI.SetActive(false);
            pState.UnitState = UnitState.Idle;
            return;
        }
        escMenuUI.SetActive(true);
        pState.UnitState = UnitState.Interact;
    }

    // 캐릭터 선택화면으로 이동
    public void ToCharacterLobby()
    {
        GameManager.gm.sceneNumber = 1;
        SceneManager.LoadScene("99_LoadingScene");
    }

    // 캐릭터 사밍시 버튼 누르면 마을로
    public void ToTown()
    {
        GameManager.gm.sceneNumber = 2;
        SceneManager.LoadScene("99_LoadingScene");
    }

    // 게임오버시 호출되는 메소드(게임오버 매뉴 활성화)
    public void GameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    // 게임 종료
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}
