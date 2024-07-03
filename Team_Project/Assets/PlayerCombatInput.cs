using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatInput : MonoBehaviour
{
    public Button[] buttons;
    public AttackSO defaultAttack;
    public PlayerCombatTest player;

    private void Update()
    {
        switch (Input.inputString)
        {
            case "a":
                buttons[0].onClick.Invoke();
                break;
            case "s":
                buttons[1].onClick.Invoke();
                break;
            case "z":
                player.attack = defaultAttack;
                player.Attack();
                break;
            default:
                break;
        }
    }
}
