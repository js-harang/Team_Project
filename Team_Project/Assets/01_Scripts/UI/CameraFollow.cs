using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;

    // ī�޶�� player�� y �Ÿ���
    public float yDistance = 4f;

    // ī�޶�� player�� z �Ÿ���
    public float zDistance = 8f;

    // �÷��̾�� ī�޶��� ���� ����
    float camFromPlayerY;

    // �÷��̾�� ī�޶��� Z�� ����
    float camFromPlayerZ;

    // ī�޶� �̵��� ��ǥ ��ġ
    Vector3 movePosition;

    // ī�޶� �����̴� �ӵ�
    public float camSpeed = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // ���� ���۽� ī�޶� ��ġ
        StartCameraPosition();
    }

    void Update()
    {
        CameraFollowPlayer();
    }

    // player �κ��� x�� player.x, y�� + 4, z�� -8 ��ŭ ��ġ�� ������  10�� �ӵ��� ����ٴ�
    void CameraFollowPlayer()
    {
        camFromPlayerY = player.transform.position.y + yDistance;
        camFromPlayerZ = player.transform.position.z - zDistance;
        movePosition = new Vector3(player.transform.position.x, camFromPlayerY, camFromPlayerZ);

        transform.position = Vector3.Lerp(transform.position, movePosition,  camSpeed * Time.deltaTime);
    }

    // ���۽� �÷��̾�κ��� ������ ���� ī�޶� ��ġ
    void StartCameraPosition()
    {
        camFromPlayerY = player.transform.position.y + yDistance;
        camFromPlayerZ = player.transform.position.z - zDistance;
        movePosition = new Vector3(player.transform.position.x, camFromPlayerY, camFromPlayerZ);

        transform.position = movePosition;
    }
}
