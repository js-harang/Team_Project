using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState
{
    Intro,
    Start,
    Running,
    NowBattle,
    BossRound,
    Clear,
    Defeat,
    BattleEnd,
}

public class BattleController : MonoBehaviour
{
    [SerializeField]
    BattleState battleState;
    public BattleState BattleState 
    { 
        get 
        { 
            return battleState; 
        } 
        set 
        { 
            battleState = value;
            // 배틀 씬의 진행 상태에 따라 필요한 메서드를 호출하여 동작
            switch (value)                              
            {
                case BattleState.Intro:
                    StartCoroutine(BattleIntro());
                    break;
                case BattleState.Start:
                    StartCoroutine(BattleStart());
                    break;
                case BattleState.Running:
                    break;
                case BattleState.NowBattle:
                    break;
                case BattleState.BossRound:
                    break;
                case BattleState.Clear:
                    StartCoroutine(ClearProcess());
                    break;
                case BattleState.Defeat:
                    GameOver();
                    break;
                case BattleState.BattleEnd:
                    BattleEndProcess();
                    break;
                default:
                    break;
            }
        } 
    }

    PlayerState pS;
    [SerializeField]
    GameObject hyperMoveZone;

    // 현재 진행되는 전투에 스폰된 에너미의 수
    [SerializeField]
    int enemyCount;
    public int EnemyCount { get { return enemyCount; } set { enemyCount = value; } }

    // 플레이어가 조작을 멈춘 시간
    float playerDive;

    // 배틀 중 나타나는 UI들 변수
    [SerializeField] GameObject battleStartImg;
    [SerializeField] GameObject goArrowImg;
    [SerializeField] GameObject battleClearUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject battleEndUI;

    // 스테이지 클리어 후 나타나는 정산 NPC에 관련된 변수
    [SerializeField] GameObject battleShopNPC;
    [SerializeField] Transform endShopSpawn;


    private void Start()
    {
        pS = FindObjectOfType<PlayerState>().GetComponent<PlayerState>();
        BattleState = BattleState.Intro;
    }

    private void Update()
    {
        GoSignOnOff();
    }

    // 배틀 개시 전 잠시 대기하는 시간(여기에 시네머신 연출 재생한다거나)
    IEnumerator BattleIntro()
    {
        pS.UnitState = UnitState.Interact;

        yield return new WaitForSeconds(3f);

        BattleState = BattleState.Start;
        pS.UnitState = UnitState.Idle;
    }

    // 진행 가능 상태일 때, 플레이어가 대기상화이라면 UI 표시
    void GoSignOnOff()
    {
        if (battleState == BattleState.Running && pS.UnitState == UnitState.Idle)
        {
            playerDive += Time.deltaTime;
            if (playerDive >= 3)
                goArrowImg.SetActive(true);
        }
        else
        {
            playerDive = 0;
            goArrowImg.SetActive(false);
        }
    }

    // 배틀 시작시 Start 이미지 애니메이션 재생
    IEnumerator BattleStart()
    {
        battleStartImg.SetActive(true);

        yield return new WaitForSeconds(2f);

        BattleState = BattleState.Running;
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
        BattleState = BattleState.BattleEnd;
        pS.UnitState = UnitState.Idle;
    }

    // 버튼 누르면 마을로
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

    // 배틀 종료 시 시작되는 동작 메소드
    void BattleEndProcess()
    {
        Instantiate(battleShopNPC, endShopSpawn.position, Quaternion.identity);
        battleEndUI.SetActive(true);
    }

    // 배틀 정산 후 화면에서 재도전 버튼을 누르면 다시 배틀씬을 실행
    public void OnemoreTry()
    {
        GameManager.gm.sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("99_LoadingScene");
    }
}
