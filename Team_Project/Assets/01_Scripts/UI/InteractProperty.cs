using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType
{
    Object,
    NPC,
}

public class InteractProperty : MonoBehaviour
{
    [SerializeField]
    InteractType interactType;

    public InteractType InteractType { get { return interactType; } }

    [SerializeField]
    int interactId;
    public int InteractId { get { return interactId; } }

    [SerializeField]
    string interactName;
    public string InteractName { get { return interactName; } }
}
