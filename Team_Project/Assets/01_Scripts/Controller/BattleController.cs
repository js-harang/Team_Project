using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public BattleState BattleState 
    { 
        get 
        { 
            return battleSate; 
        } 
        set 
        { 
            battleSate = value;
            if (value == BattleState.BattleEnd)
                BattleEndProcess();
        } 
    }

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
    public GameObject gameOverUI;
    public GameObject battleEndUI;

    public GameObject battleShopNPC;
    public Transform EndShopSpawnPosition;


    private void Start()
    {
        pS = FindObjectOfType<PlayerState>().GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (pS.UnitState == UnitState.Die)
            battleSate = BattleState.Defeat;

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
                GameOver();
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

        battleSate = BattleState.Start;
        pS.UnitState = UnitState.Idle;
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

    // 배틀 클리어시 작동하는 과정
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

    // 캐릭터 사밍시 버튼 누르면 마을로
    public void ToTown()
    {
        GameManager.gm.sceneNumber = 2;
        SceneManager.LoadScene("99_LoadingScene");
    }

    // 게임오버시 호출되는 메소드(게임오버 매뉴 활성화)
    void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    void BattleEndProcess()
    {
        Instantiate(battleShopNPC, EndShopSpawnPosition);
        battleEndUI.SetActive(true);
    }

    public void OnemoreTry()
    {
        GameManager.gm.sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("99_LoadingScene");
    }
}
