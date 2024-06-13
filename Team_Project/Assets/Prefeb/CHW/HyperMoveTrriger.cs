using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperMoveTrriger : MonoBehaviour
{
    [SerializeField]
    Transform landingSpot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = landingSpot.position;
        }
    }
}
