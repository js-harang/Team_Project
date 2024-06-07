using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    static bool nowInteracting;
    public static bool NowInteracting 
    { get { return nowInteracting; } set {nowInteracting = value; } }

}
