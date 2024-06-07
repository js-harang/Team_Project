using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;
    Vector3 originPos;
    // ī�޶� �����̴� �ӵ�
    public float camSpeed = 5f;

    private void Start()
    {
        originPos = new Vector3(0, 4f, -8f);
    }

    private void Update()
    {
            transform.position = Vector3.Lerp(transform.position, player.position + originPos, camSpeed * Time.deltaTime);
    }
}
