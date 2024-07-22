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
    public Attack defaultAttack;
    private PlayerCombat player;

    [SerializeField] SkillButton[] skillBtn;
    [SerializeField] Button[] btns;

    private void Start()
    {
        player = GetComponent<PlayerCombat>();

        skillBtn = GameObject.FindWithTag("PlayerBtn").GetComponent<SkillBtnManager>().btns;
        btns = new Button[skillBtn.Length];

        for (int i = 0; i < skillBtn.Length; i++)
        {
            btns[i] = skillBtn[i].GetComponent<Button>();
        }

/*        StartSetting();*/
    }
    private void Update()
    {
        if (player.pbs.UnitState == UnitState.Interact || player.pbs.UnitState == UnitState.Wait)
            return;

        ButtonInput();
    }
    void ButtonInput()
    {
        // A = 0, S = 1, D = 2, F = 3, C = 4, V = 5,
        // Q = 6, W = 7, E = 8, R = 9, T = 10, G = 11
        switch (Input.inputString.ToUpper())
        {
            case "A":
                int a = (int)ButtonNum.A;
                btns[a].onClick.Invoke(); break;

            case "S":
                int s = (int)ButtonNum.S;
                btns[s].onClick.Invoke(); break;

            case "D":
                int d = (int)ButtonNum.D;
                btns[d].onClick.Invoke(); break;

            case "F":
                int f = (int)ButtonNum.F;
                btns[f].onClick.Invoke(); break;

            case "C":
                int c = (int)ButtonNum.C;
                btns[c].onClick.Invoke(); break;

            case "V":
                int v = (int)ButtonNum.V;
                btns[v].onClick.Invoke(); break;

            case "Q":
                int q = (int)ButtonNum.Q;
                btns[q].onClick.Invoke(); break;

            case "W":
                int w = (int)ButtonNum.W;
                btns[w].onClick.Invoke(); break;

            case "E":
                int e = (int)ButtonNum.E;
                btns[e].onClick.Invoke(); break;

            case "R":
                int r = (int)ButtonNum.R;
                btns[r].onClick.Invoke(); break;

            case "T":
                int t = (int)ButtonNum.T;
                btns[t].onClick.Invoke(); break;

            case "G":
                int g = (int)ButtonNum.G;
                btns[g].onClick.Invoke(); break;

            case "Z":
                player.attack = defaultAttack;
                player.Attack();
                break;
            
            default:    break;
        }
    }

    void StartSetting()
    {
        player = GetComponent<PlayerCombat>();
        skillBtn = FindObjectOfType<SkillBtnManager>().btns;

        for (int i = 0; i < skillBtn.Length; i++)
        {
            btns[i] = skillBtn[i].GetComponent<Button>();
        }
    }
}
