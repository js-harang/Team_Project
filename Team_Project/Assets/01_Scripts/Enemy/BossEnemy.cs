using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossEnemy : MonoBehaviour
{
    BattleController bC;

    // 현재 체력
    public float currentHp;
    // 최대체력
    public int maxHp;
    // 공격력
    public int atkPower;

    void Start() 
    {
        bC = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bC.BossCount++;
        AppearAction();
    }

    void Update() { }

    public abstract void AppearAction();
}
