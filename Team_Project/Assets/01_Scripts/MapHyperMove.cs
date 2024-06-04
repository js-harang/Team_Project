using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHyperMove : MonoBehaviour
{
    public GameObject jumpStart;
    public GameObject jumpEnd;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Sphere")
        {
            StartCoroutine(HyperMove());
        }
    }

    IEnumerator HyperMove()
    {
        yield return new WaitForSeconds(1f);
        rb.AddForce(Vector3.up * 20, ForceMode.Impulse);
        
    }
}
