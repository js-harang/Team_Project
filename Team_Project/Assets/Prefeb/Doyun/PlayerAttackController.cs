using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    // ���ݵ� �迭
    public PlayerAttack pAttack;

    // ���� ���� ��ġ ����
    public Transform atkPos;


    void Start()
    {
        pAttack.InitSetting();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pAttack.Attack(atkPos);
        }
    }
}
