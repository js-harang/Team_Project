using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    // CreateCharacter 버튼 관련 변수
    [SerializeField, Space(10)]
    Transform mainCamera;
    [SerializeField]
    GameObject lobbyCanvas;
    // Camera 위치 변수
    Vector3 originPos = new(0, 0, -10f);
    Vector3 createPos = new(-100f, 0, -10f);

    // Preference 버튼 관련 변수
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
    /// 캐릭터 생성 버튼
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
