using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform maincamera;

    void Start()
    {
        maincamera = Camera.main.transform;
    }

    void Update()
    {
        BillboardFunc();
    }

    void BillboardFunc()
    {
        transform.rotation = Quaternion.LookRotation(maincamera.forward, maincamera.up);
    }
}
