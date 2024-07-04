using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState
{
    Intro,
    Start,
    Running,
    NowBattle,
    BossAppear,
    Clear,
    Defeat,
    BattleEnd,
}

public class BattleController : MonoBehaviour
{
    [SerializeField] BattleState battleState;
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
                    StartCoroutine(StartIntro());
                    break;
                case BattleState.Start:
                    StartCoroutine(StageStart());
                    break;
                /*case BattleState.Running:
                    break;
                case BattleState.NowBattle:
                    break;*/
                case BattleState.BossAppear:
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
    [SerializeField] GameObject hyperMoveZone;
    [SerializeField] GameObject[] blockWall;

    // ���� ����Ǵ� ������ ������ ���ʹ��� ��
    [SerializeField]
    int enemyCount;
    public int EnemyCount
    {
        get
        {
            return enemyCount;
        }
        set
        {
            // ���� ���ڰ� 0�� �Ǹ� ���� �ܰ�� ����
            enemyCount = value;
            if (value == 0)
            {
                StartCoroutine(BlockWallOnOff(waveNum));
                BattleState = BattleState.Running;
                waveNum++;
            }
        }
    }

    int bossCount;
    public int BossCount
    {
        get
        {
            return bossCount;
        }
        set
        {
            bossCount = value;

        }
    }

    // ���� Ŭ������ ���̺��� ��
    public int waveNum;

    // �÷��̾ ������ ���� �ð� ����
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
        // �÷��̾� ����� ����
        if (pS.UnitBS == UnitBattleState.Die)
        {
            BattleState = BattleState.Defeat;
            return;
        }

        GoSignOnOff();
    }

    // �������� ���� �� ��� ����ϴ� �ð�(���⿡ �ó׸ӽ� ���� ����Ѵٰų�)
    IEnumerator StartIntro()
    {
        pS.UnitState = UnitState.Interact;

        yield return new WaitForSeconds(3f);

        BattleState = BattleState.Start;
        pS.UnitState = UnitState.Idle;
    }

    // ���� ���� ������ ��, �÷��̾ ����ȭ�̶�� UI ǥ��
    private void GoSignOnOff()
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

    // �������� ���۽� Start �̹��� �ִϸ��̼� ���
    IEnumerator StageStart()
    {
        battleStartImg.SetActive(true);

        yield return new WaitForSeconds(2f);

        BattleState = BattleState.Running;
        battleStartImg.SetActive(false);
    }

    // ��Ʋ ���� �� ������ ������ ���� �����ϴ� �޼ҵ�
    public IEnumerator BlockWallOnOff(int waveNum)
    {
        if (!blockWall[waveNum].activeSelf)
            blockWall[waveNum].SetActive(true);
        else
        {
            yield return new WaitForSeconds(2f);
            blockWall[waveNum].SetActive(false);
        }
    }

    // ��Ʋ Ŭ����� �۵��ϴ� ����
    IEnumerator ClearProcess()
    {
        yield return new WaitForSeconds(1f);

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
    public void ToTown(int sceneNumber)
    {
        GameManager.gm.MoveScene(sceneNumber);
    }

    // ���ӿ����� ȣ��Ǵ� �޼ҵ�(���ӿ��� �Ŵ� Ȱ��ȭ)
    private void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    // ��Ʋ ���� �� ���۵Ǵ� ���� �޼ҵ�
    private void BattleEndProcess()
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
