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
    Vector3 originPos = new Vector3(0, 0, -10f);
    Vector3 createPos = new Vector3(-100f, 0, -10f);

    // Preference ��ư ���� ����
    [SerializeField, Space(10)]
    GameObject preferencePopup;
    bool isPreferencePopup = false;

    private void Update()
    {
        // ȯ�漳�� Ż��
        if (Input.GetKeyDown(KeyCode.Escape) && isPreferencePopup)
            ClickPreferencesBtn();

        // ĳ���� ���� â Ż��
        if (Input.GetKeyDown(KeyCode.Escape) && !isLooby)
            ReturnLobby();
    }

    public void ClickGameStartBtn()
    {
        GameManager.gm.sceneNumber = 3;
        SceneManager.LoadScene("99_LoadingScene");
    }

    /// <summary>
    /// ĳ���� ���� ��ư
    /// </summary>
    public void ClickCreateCharacterBtn()
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

    /// <summary>
    /// ȯ�漳�� ��ư
    /// </summary>
    public void ClickPreferencesBtn()
    {
        isPreferencePopup = !isPreferencePopup;
        preferencePopup.SetActive(isPreferencePopup);
    }
}
