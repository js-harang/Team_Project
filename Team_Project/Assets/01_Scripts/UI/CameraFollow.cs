using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;
    // 플레이어로부터 카메라가 둘 거리
    Vector3 distanceFromPlayer;

    [SerializeField]
    float yDistance;
    [SerializeField]
    float zDistance;
    // 카메라가 움직이는 속도
    [SerializeField]
    float camSpeed;

    private void Start()
    {
        distanceFromPlayer = new Vector3(0, yDistance, zDistance);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + distanceFromPlayer, camSpeed * Time.deltaTime);
    }
}
