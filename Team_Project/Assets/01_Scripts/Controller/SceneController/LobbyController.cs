using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    // CreateCharacter ��ư ���� ����
    [SerializeField, Space(10)]
    Transform mainCamera;
    [SerializeField]
    GameObject lobbyCanvas;
    // Camera ��ġ ����
    Vector3 originPos = new(0, 0, -10f);
    Vector3 createPos = new(-100f, 0, -10f);

    // Preference ��ư ���� ����
    [SerializeField, Space(10)]
    GameObject preferencePopup;
    bool isPreferencePopup = false;

    private void Update()
    {

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
        SceneManager.LoadScene("11_CreateCharacter");
    }

    public void BackBtn()
    {
        GameManager.gm.sceneNumber = 0;
        SceneManager.LoadScene("99_LoadingScene");
    }
}
