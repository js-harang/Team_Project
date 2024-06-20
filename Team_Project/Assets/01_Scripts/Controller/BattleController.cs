using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState
{
    Intro,
    Start,
    Running,
    StartRound,
    FinalRound,
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
            switch (value)                              // ��Ʋ ���� ���� ���¿� ���� �ʿ��� �޼��带 ȣ���Ͽ� ����
            {
                case BattleState.Intro:
                    StartCoroutine(BattleIntro());
                    break;
                case BattleState.Start:
                    StartCoroutine(BattleStart());
                    break;
                case BattleState.Running:
                    break;
                case BattleState.StartRound:
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
    public Transform endShopSpawn;


    private void Start()
    {
        pS = FindObjectOfType<PlayerState>().GetComponent<PlayerState>();
        BattleState = BattleState.Intro;
    }

    // ��Ʋ ���� �� ��� ����ϴ� �ð�(���⿡ �ó׸ӽ� ���� ����Ѵٰų�)
    IEnumerator BattleIntro()
    {
        pS.UnitState = UnitState.Interact;

        yield return new WaitForSeconds(3f);

        BattleState = BattleState.Start;
        pS.UnitState = UnitState.Idle;
    }

    // ��Ʋ ���۽� Start �̹��� �ִϸ��̼� ���
    IEnumerator BattleStart()
    {
        Animator startAnim = battleStartImg.GetComponent<Animator>();
        startAnim.SetTrigger("nowStart");
        BattleState = BattleState.Running;

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
        BattleState = BattleState.BattleEnd;
        pS.UnitState = UnitState.Idle;
    }

    // ��ư ������ ������
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

    // ��Ʋ ���� �� ���۵Ǵ� ���� �޼ҵ�
    void BattleEndProcess()
    {
        Instantiate(battleShopNPC, endShopSpawn.position, Quaternion.identity);
        battleEndUI.SetActive(true);
    }

    // ��Ʋ ���� �� ȭ�鿡�� �絵�� ��ư�� ������ �ٽ� ��Ʋ���� ����
    public void OnemoreTry()
    {
        GameManager.gm.sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("99_LoadingScene");
    }
}
