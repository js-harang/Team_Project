using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    Intro,
    Start,
    Running,
    LoadNext,
    FinalRound,
    Clear,
    Defeat,
    BattleEnd,
}

public class BattleController : MonoBehaviour
{
    [SerializeField]
    BattleState battleSate;
    public BattleState BattleState { get { return battleSate; } set { battleSate = value; } }

    PlayerState pS;
    [SerializeField]
    GameObject hyperMoveZone;
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    GameObject[] blockWall;

    int enemyCount;
    public int EnemyCount { get { return enemyCount; } set { enemyCount = value; } }

    [SerializeField]
    int totalRound;
    int nowRound;

    public GameObject battleStartImg;
    public GameObject battleClearUI;

    private void Start()
    {
        pS = FindObjectOfType<PlayerState>().GetComponent<PlayerState>();
    }

    private void Update()
    {
        BattleStageProcess(battleSate);
    }

    // 배틀 씬의 진행 상태에 따라 필요한 메서드를 호출하여 동작
    void BattleStageProcess(BattleState battleState)
    {
        switch (battleSate)
        {
            case BattleState.Intro:
                StartCoroutine(BattleIntro());
                break;
            case BattleState.Start:
                StartCoroutine(BattleStart());
                break;
            case BattleState.Running:
                break;
            case BattleState.LoadNext:
                break;
            case BattleState.FinalRound:
                break;
            case BattleState.Clear:
                StartCoroutine(ClearProcess());
                break;
            case BattleState.Defeat:
                break;
            case BattleState.BattleEnd:
                break;
            default:
                break;
        }
    }

    // 배틀 개시 전 잠시 대기하는 시간(여기에 시네머신 연출 재생한다거나)
    IEnumerator BattleIntro()
    {
        pS.UnitState = UnitState.Interact;
        yield return new WaitForSeconds(3f);
        pS.UnitState = UnitState.Idle;
        battleSate = BattleState.Start;
    }

    // 배틀 시작시 Start 이미지 애니메이션 재생
    IEnumerator BattleStart()
    {
        Animator startAnim = battleStartImg.GetComponent<Animator>();
        startAnim.SetTrigger("nowStart");
        battleSate = BattleState.Running;
        yield return new WaitForSeconds(2f);
        battleStartImg.SetActive(false);
    }

    IEnumerator ClearProcess()
    {
        pS.UnitState = UnitState.Interact;
        battleClearUI.SetActive(true);
        Animator clearAnim = battleClearUI.GetComponent<Animator>();
        clearAnim.SetTrigger("stageClear");
        yield return new WaitForSeconds(3f);
        battleClearUI.SetActive(false);
        battleSate = BattleState.BattleEnd;
        pS.UnitState = UnitState.Idle;
    }
}
