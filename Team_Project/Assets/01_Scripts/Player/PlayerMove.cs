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
    public float setSpeed;
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


    private void Start()
    {
        StartSetting();
    }

    private void Update()
    {
        GrondCheck();

        InputKey();

        LookDirection();

    }

    void InputKey()
    {   // Ű�Է�
        if (pState.UnitState == UnitState.Interact)
            return;
        
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        Move();

        Jump();
    }

    void Move()
    {   // �ƹ� �̵�Ű �Է½�
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (isGround)
            {
                pState.UnitState = UnitState.Run;
                anim.SetTrigger("IdleToMove");
            }

            speed = setSpeed;

            Vector3 inputMove = new Vector3(inputX, 0, inputZ).normalized;

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

            speed = 0f;
        }
    }

    void GrondCheck()
    {    // �ٴ� üũ�� ����
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, checkDis, grdLayer))
        {
            isGround = true;
            anim.SetBool("IsGround", isGround);
            pState.UnitState = UnitState.Idle;
        }
        else
        {
            isGround = false;
            anim.SetBool("IsGround", isGround);
            pState.UnitState = UnitState.Jump;
        }
        DrawArrowToGround();
    }

    void DrawArrowToGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, grdLayer))
        {
            if (!isGround)
            {
                playerPointer.SetActive(true);
                playerPointer.transform.position = hitInfo.point;
            }
            else
            {
                playerPointer.SetActive(false);
            }
        }
    }

    void Jump()
    {        // ����Ű �Է½�
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            pState.UnitState = UnitState.Jump;
            anim.SetTrigger("TriggerJump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void LookDirection()
    {   // ������ �����϶� �����̴� �������� ȸ��
        if (Input.GetButton("Horizontal"))
        {
            if (inputX != 0)
                lookInput = inputX;
        }

        lookdir = (Vector3.right * lookInput);
        transform.rotation = Quaternion.LookRotation(lookdir);
    }

    void StartSetting()
    {   // Start() ���� ����� ��ɵ� ����
        rb = GetComponent<Rigidbody>();
        lookInput = 1f;
        transform.rotation = Quaternion.LookRotation(Vector3.right);

        anim = GetComponentInChildren<Animator>();

        pState = GetComponent<PlayerState>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,transform.position + Vector3.down * checkDis);
    }
}
