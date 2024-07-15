using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperMoveTrriger : MonoBehaviour
{
    [SerializeField]
    Transform startPosition;
    [SerializeField]
    Transform landingPosition;

    PlayerState pState;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pState = other.GetComponent<PlayerState>();
            pState.UnitState = UnitState.Wait;
            other.transform.position = startPosition.position;
            StartCoroutine(HyperMove(other));
        }
    }

    IEnumerator HyperMove(Collider other)
    {
        yield return new WaitForSeconds(2f);
        other.transform.position = landingPosition.position;
        pState.UnitState = UnitState.Idle;
    }
}
