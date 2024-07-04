using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    PlayerState pState;
    PlayerCombat playerCombat;
    Animator anim;

    // 바라보는 방향 변수
    Vector3 lookdir;
    float lookInput;

    // 입력
    float inputX;
    float inputZ;
    Vector3 inputMove;

    // 이동
    [SerializeField] float speed;
    public float jumpForce = 25;


    //감지거리
    public float checkDis;
    // 감지 할 레이어
    public LayerMask grdLayer;
    public LayerMask wallLayer;

    [SerializeField] private bool isGround;

    // 물리
    Rigidbody rb;

    public GameObject playerPointer;

    CapsuleCollider playerCollider;

    private void Start()
    {
        StartSetting();

        playerCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (pState.UnitBS == UnitBattleState.Die)
            return;

        GrondCheck();

        if (pState.UnitState == UnitState.Interact || pState.UnitState == UnitState.Wait ||
            (pState.UnitBS != UnitBattleState.Idle && !playerCombat.canMoveAtk))
            return;

        InputKey();

        LookDirection();
    }

    private void InputKey()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        Move();

        Jump();
    }

    private void Move()
    {
        // 아무 이동키 입력시
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (isGround)
            {
                pState.UnitState = UnitState.Run;
                anim.SetTrigger("IdleToMove");
            }
            else
                anim.ResetTrigger("IdleToMove");

            // 벽 충돌
            WallCheck();

            inputMove = new Vector3(inputX, 0, inputZ).normalized;

            Vector3 move = transform.position + (speed * Time.deltaTime * inputMove);
            transform.position = move;
        }
        else
        {
            if (isGround)
            {
                pState.UnitState = UnitState.Idle;
                anim.SetTrigger("MoveToIdle");
            }
        }
    }

    private void WallCheck()
    {
        Ray rayX = new(transform.position, new Vector3(inputX, 0, 0));
        Ray rayZ = new(transform.position, new Vector3(0, 0, inputZ));
        if (Physics.Raycast(rayX, out _, checkDis, wallLayer))
            inputX = 0;
        if (Physics.Raycast(rayZ, out _, checkDis, wallLayer))
            inputZ = 0;
    }

    private void GrondCheck()
    {
        // 바닥 체크용 레이
        Ray ray = new(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out _, checkDis, grdLayer))
        {
            isGround = true;
            anim.SetBool("IsGround", isGround);
        }
        else
        {
            pState.UnitState = UnitState.Jump;
            isGround = false;
            anim.SetBool("IsGround", isGround);
        }
        DrawArrowToGround();
    }

    private void DrawArrowToGround()
    {
        Ray ray = new(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, grdLayer))
        {
            if (!isGround)
            {
                playerPointer.SetActive(true);

                playerPointer.transform.position = hitInfo.point;
                /*        new Vector3(
                        hitInfo.transform.position.x,
                        hitInfo.transform.position.y + 1,
                        hitInfo.transform.position.z);
                */
            }
            else
            {
                playerPointer.SetActive(false);
            }
        }
    }

    private void Jump()
    {
        // 점프키 입력시
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (pState.UnitBS != UnitBattleState.Attack &&
                pState.UnitBS != UnitBattleState.Hurt)
            {
                anim.SetTrigger("TriggerJump");
            }
        }
    }

    private void LookDirection()
    {
        if (pState.UnitBS == UnitBattleState.Attack)
            return;

        // 움직임 상태일때 움직이는 방향으로 회전
        if (Input.GetButton("Horizontal"))
        {
            if (inputX != 0)
                lookInput = inputX;
        }

        lookdir = (Vector3.right * lookInput);
        transform.rotation = Quaternion.LookRotation(lookdir);
    }

    private void StartSetting()
    {
        // Start() 에서 사용할 기능들 모음
        rb = GetComponent<Rigidbody>();
        lookInput = 1f;
        transform.rotation = Quaternion.LookRotation(Vector3.right);

        anim = GetComponentInChildren<Animator>();

        pState = GetComponent<PlayerState>();

        playerCombat = GetComponent<PlayerCombat>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDis);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 100f);
    }
}