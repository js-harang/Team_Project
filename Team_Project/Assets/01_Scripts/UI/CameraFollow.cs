using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;
    // �÷��̾�κ��� ī�޶� �� �Ÿ�
    Vector3 distanceFromPlayer;

    [SerializeField]
    float yDistance = 4f;
    [SerializeField]
    float zDistance = -8f;
    // ī�޶� �����̴� �ӵ�
    [SerializeField]
    float camSpeed = 5f;

    private void Start()
    {
        distanceFromPlayer = new Vector3(0, yDistance, zDistance);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + distanceFromPlayer, camSpeed * Time.deltaTime);
    }
}
