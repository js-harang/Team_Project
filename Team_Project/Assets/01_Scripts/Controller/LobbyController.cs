using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    // CreateCharacter 버튼 관련 변수
    [SerializeField, Space(10)]
    Transform mainCamera;
    [SerializeField]
    GameObject lobbyCanvas;
    bool isLooby = true;
    // Camera 위치 변수
    Vector3 originPos = new Vector3(0, 0, -10f);
    Vector3 createPos = new Vector3(-100f, 0, -10f);

    // Preference 버튼 관련 변수
    [SerializeField, Space(10)]
    GameObject preferencePopup;
    bool isPreferencePopup = false;

    private void Update()
    {
        // 환경설정 탈출
        if (Input.GetKeyDown(KeyCode.Escape) && isPreferencePopup)
            ClickPreferencesBtn();

        // 캐릭터 생성 창 탈출
        if (Input.GetKeyDown(KeyCode.Escape) && !isLooby)
            ReturnLobby();
    }

    public void ClickGameStartBtn()
    {
        GameManager.gm.sceneNumber = 3;
        SceneManager.LoadScene("99_LoadingScene");
    }

    /// <summary>
    /// 캐릭터 생성 버튼
    /// </summary>
    public void ClickCreateCharacterBtn()
    {
        isLooby = false;
        lobbyCanvas.SetActive(isLooby);
        mainCamera.position = createPos;
    }

    /// <summary>
    /// Lobby로 되돌아가기
    /// </summary>
    public void ReturnLobby()
    {
        isLooby = true;
        lobbyCanvas.SetActive(isLooby);
        mainCamera.position = originPos;
    }

    /// <summary>
    /// 환경설정 버튼
    /// </summary>
    public void ClickPreferencesBtn()
    {
        isPreferencePopup = !isPreferencePopup;
        preferencePopup.SetActive(isPreferencePopup);
    }
}
