using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossEnemy : MonoBehaviour
{
    BattleController bC;

    // ���� ü��
    public float currentHp;
    // �ִ�ü��
    public int maxHp;
    // ���ݷ�
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
