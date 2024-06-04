using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    Animator anim;

    // Translate 테스트
    public float setSpeed;
    float speed;
    public float jumpForce;

    Vector3 lookdir;
    float lookInput;

    float inputX;
    float inputZ;

    Rigidbody rb;

    UnitState unitState;

    private void Start()
    {
        StartSetting();
    }

    private void Update()
    {
        InputKey();

        LookDirection();

    }

    void InputKey()
    {   // 키입력
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        Move();

        Jump();
    }
    void Move()
    {   // 아무 이동키 입력시
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            anim.SetTrigger("IdleToMove");

            unitState = UnitState.Run;
            speed = setSpeed;

            Vector3 inputMove = new Vector3(inputX, 0, inputZ).normalized;

            Vector3 move = transform.position + (inputMove * speed * Time.deltaTime);
            transform.position = move;
        }
        else
        {
            anim.SetTrigger("MoveToIdle");
            
            unitState = UnitState.Idle;
            speed = 0f;
        }
    }
    
    void LookDirection()
    {   // 움직임 상태일때 움직이는 방향으로 회전
        if (Input.GetButton("Horizontal"))
        {
            if (inputX != 0)
                lookInput = inputX;
        }

        lookdir = (Vector3.right * lookInput);
        transform.rotation = Quaternion.LookRotation(lookdir);
        
    }

    void Jump()
    {        // 점프키 입력시
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void StartSetting()
    {   // Start() 에서 사용할 기능들 모음
        rb = GetComponent<Rigidbody>();
        lookInput = 1f;
        transform.rotation = Quaternion.LookRotation(Vector3.right);

        anim = GetComponentInChildren<Animator>();
    }
}
