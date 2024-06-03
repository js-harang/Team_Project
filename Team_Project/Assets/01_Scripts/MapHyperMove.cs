using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHyperMove : MonoBehaviour
{
    public GameObject jumpStart;
    public GameObject jumpEnd;
    float playerY;

    private void Start()
    {
        playerY = transform.position.y;
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
        playerY += 1000 * Time.deltaTime;
    }
}
