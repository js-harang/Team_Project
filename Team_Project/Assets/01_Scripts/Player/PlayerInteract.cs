using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerState pState;
    bool isPlayeIn;

    private void Start()
    {
        pState = GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (isPlayeIn)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                pState.UnitStat = UnitState.Interact;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            isPlayeIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            isPlayeIn = false;
        }
    }
}
