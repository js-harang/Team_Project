using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNearCheck : MonoBehaviour
{
    public Canvas interactUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactUI.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactUI.enabled = false;
        }
    }
}
