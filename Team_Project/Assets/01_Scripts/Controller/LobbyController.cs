using UnityEngine;

public class LobbyController : MonoBehaviour
{
    // CreateCharacter ��ư ���� ����
    [SerializeField]
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
        if (Input.GetKeyDown(KeyCode.Escape) && isPreferencePopup)
            ClickPreferencesBtn();

        if (Input.GetKeyDown(KeyCode.Escape) && !isLooby)
            ReturnLobby();
    }

    /// <summary>
    /// ĳ���� ����
    /// </summary>
    public void ClickCreateCharacterBtn()
    {
        isLooby = false;
        lobbyCanvas.SetActive(isLooby);
        mainCamera.position = createPos;
    }

    /// <summary>
    /// ĳ���� ����â���� �ǵ��ư���
    /// </summary>
    public void ReturnLobby()
    {
        isLooby = true;
        lobbyCanvas.SetActive(isLooby);
        mainCamera.position = originPos;
    }

    /// <summary>
    /// ĳ���� ����
    /// </summary>
    public void DeleteCharacterBtn()
    {

    }

    /// <summary>
    /// ȯ�漳�� ��ư
    /// </summary>
    public void ClickPreferencesBtn()
    {
        isPreferencePopup = !isPreferencePopup;
        preferencePopup.SetActive(isPreferencePopup);
    }

    /// <summary>
    /// Scene �̵�
    /// </summary>
    public void MoveScene(int sceneNumber)
    {
        GameManager.gm.MoveScene(sceneNumber);
    }
}
