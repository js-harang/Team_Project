using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComeNear : MonoBehaviour
{
    public Canvas playerCome;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerCome.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerCome.enabled = false;
    }
}
