using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    PlayerState pState;
    Animator anim;

    // �ٶ󺸴� ���� ����
    Vector3 lookdir;
    float lookInput;

    // �Է�
    float inputX;
    float inputZ;

    // �̵�
    [SerializeField]
    float speed;
    public float jumpForce;


    //�����Ÿ�
    public float checkDis;
    // ���� �� ���̾�
    public LayerMask grdLayer;
    [SerializeField] private bool isGround;

    // ����
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
        GrondCheck();

        InputKey();

        LookDirection();

    }

    private void InputKey()
    {
        // Ű�Է�
        if (pState.UnitState == UnitState.Interact)
            return;

        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        Move();

        Jump();
    }

    private void Move()
    {
        // �ƹ� �̵�Ű �Է½�
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (isGround)
            {
                pState.UnitState = UnitState.Run;
                anim.SetTrigger("IdleToMove");
            }
            else
            {
                anim.ResetTrigger("IdleToMove");
            }

            Vector3 inputMove = new Vector3(inputX, 0, inputZ).normalized;

            // �� �浹
            if (CheckHitWall(inputMove))
                return;

            Vector3 move = transform.position + (inputMove * speed * Time.deltaTime);
            transform.position = move;
        }
        else
        {
            if (isGround)
            {
                pState.UnitState = UnitState.Idle;
                anim.SetTrigger("MoveToIdle");
            }

            //speed = 0f;
        }
    }

    private void GrondCheck()
    {
        // �ٴ� üũ�� ����
        Ray ray = new(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, checkDis, grdLayer))
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
        // ����Ű �Է½�
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            anim.SetTrigger("TriggerJump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void LookDirection()
    {
        // ������ �����϶� �����̴� �������� ȸ��
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
        // Start() ���� ����� ��ɵ� ����
        rb = GetComponent<Rigidbody>();
        lookInput = 1f;
        transform.rotation = Quaternion.LookRotation(Vector3.right);

        anim = GetComponentInChildren<Animator>();

        pState = GetComponent<PlayerState>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDis);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 100f);
    }

    /// <summary>
    /// �� ��� ���� �޼���
    /// </summary>
    /// <param name="inputmove"></param>
    /// <returns></returns>
    private bool CheckHitWall(Vector3 inputmove)
    {
        float scope = 5f;

        // �÷��̾��� �Ӹ�, ����, �� �� 3�������� ray�� ���
        List<Vector3> rayPositions = new()
        {
            transform.position + Vector3.up * 0.1f,
            transform.position + Vector3.up * playerCollider.height * 0.5f,
            transform.position + Vector3.up * playerCollider.height
        };

        // �浹 üũ
        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, inputmove * scope);
            if (Physics.Raycast(pos, inputmove, out RaycastHit hit, scope))
            {
                if(hit.collider.gameObject.layer == 6)
                {
                    rb.AddForce(hit.normal.normalized * 0.05f);
                }
            }
        }

        return false;
    }
}
