using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    // 플레이어로부터 카메라가 둘 거리
    Vector3 distanceFromPlayer;

    [SerializeField] float yDistance;
    [SerializeField] float zDistance;
    // 카메라가 움직이는 속도
    [SerializeField] float camSpeed;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        distanceFromPlayer = new Vector3(0, yDistance, zDistance);
    }

    private void Update()
    {
        if (target.gameObject.CompareTag("Player"))
            transform.position = Vector3.Lerp(transform.position, target.position + distanceFromPlayer, camSpeed * Time.deltaTime);
        else
            transform.position = Vector3.Lerp(transform.position, target.position, camSpeed * Time.deltaTime);
    }
}
