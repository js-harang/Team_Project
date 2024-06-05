using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitState
{
    Idle,
    Interact,
    Run,
    Attack,
    Hurt,
    Die,
}

public class PlayerState : MonoBehaviour
{
    

    [SerializeField] UnitState unitState;

    public UnitState UnitState
    {
        get
        {
            return unitState;
        }
        set
        {
            unitState = value;
        }
    }
}
