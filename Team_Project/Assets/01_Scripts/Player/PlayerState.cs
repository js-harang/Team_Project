using UnityEngine;

public enum UnitState
{
    Idle,
    Interact,
    Run,
    Jump,
    Attack,
    Hurt,
    Die,
}

public enum UnitBattleState
{
    Idle,
    Attack,
    Hurt,
    Die,
}

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    UnitState unitState;
    [SerializeField]
    UnitBattleState unitBattleState;

    public UnitState UnitState
    { get { return unitState; } set { unitState = value; } }

    public UnitBattleState UnitBS
    { get { return unitBattleState; } set { unitBattleState = value; } }
}
