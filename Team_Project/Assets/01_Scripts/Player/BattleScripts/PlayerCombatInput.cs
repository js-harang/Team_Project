using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatInput : MonoBehaviour
{
    enum ButtonNum
    {
        A = 0, S = 1, D = 2, F = 3, C = 4, V = 5,
        Q = 6, W = 7, E = 8, R = 9, T = 10, G = 11,
    }
    public PlayerCombat player;
    // public AttackSO defaultAttack;
    public Attack defaultAttack;

    public Button[] buttons;

    private void Update()
    {
        ButtonInput();
    }
    void ButtonInput()
    {
        // A = 0, S = 1, D = 2, F = 3, C = 4, V = 5,
        // Q = 6, W = 7, E = 8, R = 9, T = 10, G = 11
        switch (Input.inputString)
        {
            case "a":
            case "A":
                int a = (int)ButtonNum.A;
                buttons[a].onClick.Invoke(); break;

            case "s":
            case "S":
                int s = (int)ButtonNum.S;
                buttons[s].onClick.Invoke(); break;

            case "d":
            case "D":
                int d = (int)ButtonNum.D;
                buttons[d].onClick.Invoke(); break;

            case "f":
            case "F":
                int f = (int)ButtonNum.F;
                buttons[f].onClick.Invoke(); break;

            case "c":
            case "C":
                int c = (int)ButtonNum.C;
                buttons[c].onClick.Invoke(); break;

            case "v":
            case "V":
                int v = (int)ButtonNum.V;
                buttons[v].onClick.Invoke(); break;

            case "q":
            case "Q":
                int q = (int)ButtonNum.Q;
                buttons[q].onClick.Invoke(); break;

            case "w":
            case "W":
                int w = (int)ButtonNum.W;
                buttons[w].onClick.Invoke(); break;

            case "e":
            case "E":
                int e = (int)ButtonNum.E;
                buttons[e].onClick.Invoke(); break;

            case "r":
            case "R":
                int r = (int)ButtonNum.R;
                buttons[r].onClick.Invoke(); break;

            case "t":
            case "T":
                int t = (int)ButtonNum.T;
                buttons[t].onClick.Invoke(); break;

            case "g":
            case "G":
                int g = (int)ButtonNum.G;
                buttons[g].onClick.Invoke(); break;

            case "z":
            case "Z":
                player.attack = defaultAttack;
                player.Attack();
                break;
            default:
                break;
        }
    }
}
