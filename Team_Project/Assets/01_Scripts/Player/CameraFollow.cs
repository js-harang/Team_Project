using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    // �÷��̾�κ��� ī�޶� �� �Ÿ�
    Vector3 distanceFromPlayer;

    [SerializeField] float yDistance;
    [SerializeField] float zDistance;
    // ī�޶� �����̴� �ӵ�
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
