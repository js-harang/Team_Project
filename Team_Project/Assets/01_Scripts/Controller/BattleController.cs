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
            // ��Ʋ ���� ���� ���¿� ���� �ʿ��� �޼��带 ȣ���Ͽ� ����
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

    // ���� ����Ǵ� ������ ������ ���ʹ��� ��
    [SerializeField]
    int enemyCount;
    public int EnemyCount { get { return enemyCount; } set { enemyCount = value; } }

    // �÷��̾ ������ ���� �ð�
    float playerDive;

    // ��Ʋ �� ��Ÿ���� UI�� ����
    [SerializeField] GameObject battleStartImg;
    [SerializeField] GameObject goArrowImg;
    [SerializeField] GameObject battleClearUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject battleEndUI;

    // �������� Ŭ���� �� ��Ÿ���� ���� NPC�� ���õ� ����
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

    // ��Ʋ ���� �� ��� ����ϴ� �ð�(���⿡ �ó׸ӽ� ���� ����Ѵٰų�)
    IEnumerator BattleIntro()
    {
        pS.UnitState = UnitState.Interact;

        yield return new WaitForSeconds(3f);

        BattleState = BattleState.Start;
        pS.UnitState = UnitState.Idle;
    }

    // ���� ���� ������ ��, �÷��̾ ����ȭ�̶�� UI ǥ��
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

    // ��Ʋ ���۽� Start �̹��� �ִϸ��̼� ���
    IEnumerator BattleStart()
    {
        battleStartImg.SetActive(true);

        yield return new WaitForSeconds(2f);

        BattleState = BattleState.Running;
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
