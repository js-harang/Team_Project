using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;

    // 카메라와 player의 y 거리차
    public float yDistance = 4f;

    // 카메라와 player의 z 거리차
    public float zDistance = 8f;

    // 플레이어와 카메라의 높이 차이
    float camFromPlayerY;

    // 플레이어와 카메라의 Z축 차이
    float camFromPlayerZ;

    // 카메라가 이동할 목표 위치
    Vector3 movePosition;

    // 카메라가 움직이는 속도
    public float camSpeed = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // 게임 시작시 카메라 배치
        StartCameraPosition();
    }

    void Update()
    {
        CameraFollowPlayer();
    }

    // player 로부터 x는 player.x, y는 + 4, z는 -8 만큼 위치한 곳으로  10의 속도로 따라다님
    void CameraFollowPlayer()
    {
        camFromPlayerY = player.transform.position.y + yDistance;
        camFromPlayerZ = player.transform.position.z - zDistance;
        movePosition = new Vector3(player.transform.position.x, camFromPlayerY, camFromPlayerZ);

        transform.position = Vector3.Lerp(transform.position, movePosition,  camSpeed * Time.deltaTime);
    }

    // 시작시 플레이어로부터 지정된 곳에 카메라 위치
    void StartCameraPosition()
    {
        camFromPlayerY = player.transform.position.y + yDistance;
        camFromPlayerZ = player.transform.position.z - zDistance;
        movePosition = new Vector3(player.transform.position.x, camFromPlayerY, camFromPlayerZ);

        transform.position = movePosition;
    }
}
