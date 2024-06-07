using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractProperty : MonoBehaviour
{
    [SerializeField]
    string interactType;
    public string InteractType { get { return interactType; } }

    [SerializeField]
    int interactId;
    public int InteractId { get { return interactId; } }

    [SerializeField]
    string interactName;
    public string InteractName { get { return interactName; } }
}
