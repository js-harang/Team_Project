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

    // ��Ʋ ���� ���� ���¿� ���� �ʿ��� �޼��带 ȣ���Ͽ� ����
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

    // ��Ʋ ���� �� ��� ����ϴ� �ð�(���⿡ �ó׸ӽ� ���� ����Ѵٰų�)
    IEnumerator BattleIntro()
    {
        pS.UnitState = UnitState.Interact;
        yield return new WaitForSeconds(3f);
        pS.UnitState = UnitState.Idle;
        battleSate = BattleState.Start;
    }

    // ��Ʋ ���۽� Start �̹��� �ִϸ��̼� ���
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
