using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Ű���� �Է½� ������ UI��
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
            if (!gameUI.enabled)                // InteractController���� ��Ȱ��ȭ�� UI ĵ������ Ȯ���� ����
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

    // ĳ���� ����ȭ������ �̵�
    public void ToCharacterLobby()
    {
        GameManager.gm.sceneNumber = 1;
        SceneManager.LoadScene("99_LoadingScene");
    }

    // ĳ���� ��ֽ� ��ư ������ ������
    public void ToTown()
    {
        GameManager.gm.sceneNumber = 2;
        SceneManager.LoadScene("99_LoadingScene");
    }

    // ���ӿ����� ȣ��Ǵ� �޼ҵ�(���ӿ��� �Ŵ� Ȱ��ȭ)
    public void GameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    // ���� ����
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}
