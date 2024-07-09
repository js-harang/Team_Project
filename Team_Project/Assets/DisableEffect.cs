using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEffect : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1);
    }
}
