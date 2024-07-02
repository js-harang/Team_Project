using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public enum BossState
{
    Idle,
    Move,
    Attack,
    Die,
}

public abstract class BossEnemy : MonoBehaviour
{
    public BattleController bCon;

    BossState bState;
    public BossState BState 
    { 
        get 
        { 
            return bState; 
        } 
        set 
        {
            bState = value;
            switch (bState)
            {
                case BossState.Idle:
                    Idle();
                    break;
                case BossState.Attack:
                    Attack();
                    break;
                case BossState.Die:
                    Die();
                    break;
                default:
                    break;
            }
        } 
    }

    // 보스에게 필요한 변수들
    public NavMeshAgent bossNav;
    public Animator bossAnim;
    public GameObject bossStateUI;
    public TMP_Text bossName_text;
    public Slider bossHpBar;
    public Vector3 playerPosition;

    // 보스의 이름
    public string bossName;
    // 현재 체력
    public float currentHp;
    // 최대체력
    public int maxHp;
    // 공격력
    public int atkPower;


    void Start() 
    {
        // 등장 시 배틀 컨트롤러의 보스 개체수 증가시킴
        bCon = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bCon.BattleState = BattleState.BossAppear;
        bCon.BossCount++;

        //필요한 컴포넌트들 가져옴
        bossNav = GetComponent<NavMeshAgent>();
        bossAnim = GetComponentInChildren<Animator>();
        playerPosition = GameObject.FindWithTag("Player").transform.position;

        Appear();
    }

    void Update() 
    {
        Move();
    }

    public abstract void Appear();
    public abstract void Idle();
    public abstract void Move();
    public abstract void Attack();
    public abstract void Hit();
    public abstract void Die();
}
