using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerBattle playerBattle;
    bool isPlayeIn;

    private void Start()
    {
        playerBattle = GetComponent<PlayerBattle>();
    }
    private void Update()
    {
        if (isPlayeIn)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                playerBattle.UnitStat = UnitState.Interact;
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
