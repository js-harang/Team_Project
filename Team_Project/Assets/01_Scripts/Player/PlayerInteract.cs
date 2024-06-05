using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerState pState;
    // player�� NPC ��ó�� �ִ����� bool ����
    bool isMeetNPC;

    private void Start()
    {
        pState = GetComponent<PlayerState>();
    }

    // NPC ��ó�� �ִ� ���¶�� X�� ���� ��ȣ�ۿ� ���°� �ȴ�.
    private void Update()
    {
        if (isMeetNPC)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                pState.UnitState = UnitState.Interact;
            }
        }
    }

    // NPC�� �������� true�� �ٲ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            isMeetNPC = true;
        }
    }

    // NPC�� �������� false�� �ٲ�
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            isMeetNPC = false;
        }
    }
}
