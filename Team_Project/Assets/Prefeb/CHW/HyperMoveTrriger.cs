using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperMoveTrriger : MonoBehaviour
{
    [SerializeField]
    Transform startPosition;
    [SerializeField]
    Transform landingPosition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HyperMove(other));
        }
    }

    IEnumerator HyperMove(Collider other)
    {
        yield return new WaitForSeconds(2f);
        other.transform.position = landingPosition.position;
    }
}
