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
            if (pState.UnitState == UnitState.Interact)
                return;

            EscMenuOnOff();
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
    public void EscMenuOnOff()
    {
        if (escMenuUI.activeSelf)
        {
            escMenuUI.SetActive(false);
            return;
        }
        escMenuUI.SetActive(true);
    }

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
