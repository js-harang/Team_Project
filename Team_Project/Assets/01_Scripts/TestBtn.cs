using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBtn : MonoBehaviour
{
    public void AddGold(int gold)
    {
        GameManager.gm.Credit += gold;
    }
    public void AddExp(int exp)
    {
        GameManager.gm.Exp += exp;
    }
}
