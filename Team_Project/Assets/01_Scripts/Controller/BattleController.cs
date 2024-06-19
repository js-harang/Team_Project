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
                GameOver();
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

        battleSate = BattleState.Start;
        pS.UnitState = UnitState.Idle;
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

    // ��Ʋ Ŭ����� �۵��ϴ� ����
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

    // ĳ���� ��ֽ� ��ư ������ ������
    public void ToTown()
    {
        GameManager.gm.sceneNumber = 2;
        SceneManager.LoadScene("99_LoadingScene");
    }

    // ���ӿ����� ȣ��Ǵ� �޼ҵ�(���ӿ��� �Ŵ� Ȱ��ȭ)
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
