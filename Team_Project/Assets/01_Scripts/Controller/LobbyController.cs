using UnityEngine;

public class LobbyController : MonoBehaviour
{
    // CreateCharacter 버튼 관련 변수
    [SerializeField]
    Transform mainCamera;
    [SerializeField]
    GameObject lobbyCanvas;
    bool isLooby = true;
    // Camera 위치 변수
    Vector3 originPos = new(0, 0, -10f);
    Vector3 createPos = new(-100f, 0, -10f);

    // Preference 버튼 관련 변수
    [SerializeField, Space(10)]
    GameObject preferencePopup;
    bool isPreferencePopup = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPreferencePopup)
            ClickPreferencesBtn();

        if (Input.GetKeyDown(KeyCode.Escape) && !isLooby)
            ReturnLobby();
    }

    /// <summary>
    /// 캐릭터 생성
    /// </summary>
    public void ClickCreateCharacterBtn()
    {
        isLooby = false;
        lobbyCanvas.SetActive(isLooby);
        mainCamera.position = createPos;
    }

    /// <summary>
    /// 캐릭터 선택창으로 되돌아가기
    /// </summary>
    public void ReturnLobby()
    {
        isLooby = true;
        lobbyCanvas.SetActive(isLooby);
        mainCamera.position = originPos;
    }

    /// <summary>
    /// 캐릭터 삭제
    /// </summary>
    public void DeleteCharacterBtn()
    {

    }

    /// <summary>
    /// 환경설정 버튼
    /// </summary>
    public void ClickPreferencesBtn()
    {
        isPreferencePopup = !isPreferencePopup;
        preferencePopup.SetActive(isPreferencePopup);
    }

    /// <summary>
    /// Scene 이동
    /// </summary>
    public void MoveScene(int sceneNumber)
    {
        GameManager.gm.MoveScene(sceneNumber);
    }
}
