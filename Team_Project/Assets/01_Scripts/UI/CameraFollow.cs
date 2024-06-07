using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;
    // 플레이어로부터 카메라가 둘 거리
    Vector3 distanceFromPlayer;

    [SerializeField]
    float yDistance = 4f;
    [SerializeField]
    float zDistance = -8f;
    // 카메라가 움직이는 속도
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
