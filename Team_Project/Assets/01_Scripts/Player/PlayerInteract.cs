using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerState pState;
    // player가 NPC 근처에 있는지의 bool 변수
    bool isMeetNPC;

    private void Start()
    {
        pState = GetComponent<PlayerState>();
    }

    // NPC 근처에 있는 상태라면 X를 눌러 상호작용 상태가 된다.
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

    // NPC를 만났음을 true로 바꿈
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            isMeetNPC = true;
        }
    }

    // NPC를 만났음을 false로 바꿈
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            isMeetNPC = false;
        }
    }
}
