using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    // CreateCharacter ��ư ���� ����
    [SerializeField, Space(10)]
    Transform mainCamera;
    [SerializeField]
    GameObject lobbyCanvas;
    bool isLooby = true;
    // Camera ��ġ ����
    Vector3 originPos = new(0, 0, -10f);
    Vector3 createPos = new(-100f, 0, -10f);

    // Preference ��ư ���� ����
    [SerializeField, Space(10)]
    GameObject preferencePopup;
    bool isPreferencePopup = false;

    private void Update()
    {
        // ĳ���� ���� â Ż��
        if (Input.GetKeyDown(KeyCode.Escape) && !isLooby)
            ReturnLobby();
    }

    public void GameStartBtn()
    {
        GameManager.gm.sceneNumber = 2;
        SceneManager.LoadScene("99_LoadingScene");
    }

    /// <summary>
    /// ĳ���� ���� ��ư
    /// </summary>
    public void CreateCharacterBtn()
    {
        isLooby = false;
        lobbyCanvas.SetActive(isLooby);
        mainCamera.position = createPos;
    }

    /// <summary>
    /// Lobby�� �ǵ��ư���
    /// </summary>
    public void ReturnLobby()
    {
        isLooby = true;
        lobbyCanvas.SetActive(isLooby);
        mainCamera.position = originPos;
    }

    public void BackBtn()
    {
        GameManager.gm.sceneNumber = 0;
        SceneManager.LoadScene("99_LoadingScene");
    }
}
