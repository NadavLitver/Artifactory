using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [Header("ReadOnly"), Space(10)]
    public bool canMove;
    [SerializeField] Rigidbody2D m_rb;
    [SerializeField] Vector2 velocity;
    [SerializeField] Vector2 StartingScale;
    [SerializeField] Vector2 BottomRightPoint;
    [SerializeField] Vector2 BottomLeftPoint;
    [SerializeField] float horInput;
    [SerializeField] float startingGravityScale;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isLookingRight;
    [SerializeField] bool isFalling;
    [SerializeField] Collider2D m_collider;
    [SerializeField] bool Jumping;
    [SerializeField] bool CoyoteAvailable;
    [SerializeField] float acceleration;
    [SerializeField] Animator m_animator;

    [Header("Editable Properties"),Space(10)]
    [Range(0, 100), SerializeField, Tooltip("Increasing Acceleration Speed will Decrease the time the player takes to reach max speed")]
    float accelerationSpeed;
    [SerializeField, Tooltip("What is Ground?")] LayerMask GroundLayerMask;
    [Range(0, 100), SerializeField, Tooltip("Maximum Speed player can reach ")] float speed;
    [Range(0, 1), SerializeField, Tooltip("how much time AFTER the player leaves a ground can the player jump, like a grace time ")]
    float CoyoteTime;
    [Range(0, 100), SerializeField, Tooltip("Increasing this number will increase the height the player jump to")] float jumpForce;
    [Range(0, 1), SerializeField, Tooltip("Raycast range to check ground probably no need to change anything")] float groundCheckDistance;
    [Range(0, 1), SerializeField, Tooltip("How much time does the gravity change apply after hitting apex in jump")] float apexAirTimeGravityChange;
    [Range(0, 100), SerializeField, Tooltip("What is the new gravity applied at apex")] float apexGravityScale;


    public float GetHorInput { get => horInput; set => horInput = value; }
    public Rigidbody2D GetRb { get => m_rb; set => m_rb = value; }
    public Vector2 GetVelocity { get => velocity; set => velocity = value; }
    public bool GetIsLookingRight { get => isLookingRight; set => isLookingRight = value; }
    public bool GetIsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool GetIsFalling { get => isFalling; set => isFalling = value; }
    public bool GetIsJumping { get => Jumping; set => Jumping = value; }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
      //  m_livebody = GetComponent<LiveBody>();
        m_collider = GetComponent<Collider2D>();
        m_animator = GetComponentInChildren<Animator>();
        StartingScale = transform.localScale;

        startingGravityScale = m_rb.gravityScale;
    }
    void OnEnable()
    {
        canMove = true;
        m_rb.gravityScale = startingGravityScale;
    }
    public void GetColliderSizeInformation()
    {
        BottomRightPoint.x = m_collider.bounds.max.x - 0.2f;
        BottomRightPoint.y = m_collider.bounds.min.y + 0.1f;

        BottomLeftPoint.y = m_collider.bounds.min.y + 0.1f;
        BottomLeftPoint.x = m_collider.bounds.min.x + 0.2f;

    }
    private void JumpPressed()
    {
        if ((isGrounded || CoyoteAvailable) && GameManager.Instance.inputManager.JumpDown() && canMove)
        {
            Jumping = true;
            m_rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            //m_livebody.animator.SetBool("Falling", false);
            //m_livebody.animator.SetBool("Jumping", true);
            StartCoroutine(JumpApexWait());
        }
        else if (isFalling)
        {
            Jumping = false;
            //m_livebody.animator.SetBool("Jumping", false);
        }
    }
    IEnumerator JumpApexWait()
    {
        yield return new WaitUntil(() => isFalling == true);
        m_rb.gravityScale = apexGravityScale;
        yield return new WaitForSeconds(apexAirTimeGravityChange);
        m_rb.gravityScale = startingGravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        SetVelocity();
        GetColliderSizeInformation();
        GetCoyoteAndSetGrounded();
        JumpPressed();
        SetAnimatorParameters();


    }

    private void GetCoyoteAndSetGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = CheckIsGround();
      
        if (wasGrounded && !isGrounded && !Jumping)
        {
            StartCoroutine(SetCoyoteForTime());
        }
        else if (isGrounded)
        {
            Jumping = false;
        }
    }
    IEnumerator SetCoyoteForTime()
    {
        CoyoteAvailable = true;
        yield return new WaitForSeconds(CoyoteTime);
        CoyoteAvailable = false;
    }
    void SetAnimatorParameters()
    {

        m_animator.SetBool("Grounded", isGrounded);
        m_animator.SetBool("Running", ((m_rb.velocity.x != 0 || horInput != 0)&& isGrounded));
        isFalling = (!isGrounded && m_rb.velocity.y < -1f);
        //m_livebody.animator.SetBool("Falling", isFalling);
    }
    private void SetVelocity()
    {
        horInput = GameManager.Instance.inputManager.GetMoveVector().x;
        if (canMove)
        {
            //check flip
            if (horInput > 0)
            {
                isLookingRight = true;
                Flip();
            }
            else if (horInput < 0)
            {
                isLookingRight = false;
                Flip();
            }

            acceleration = Mathf.MoveTowards(acceleration, horInput == 0 ? 0 : 1, accelerationSpeed * Time.deltaTime);
            velocity = (horInput * speed * acceleration) * Vector2.right;

        }
    }
    public void Flip()
    {

        transform.localScale = new Vector3(isLookingRight ? StartingScale.x : -StartingScale.x, StartingScale.y, 1);

    }
    public void StopPlayer(float time)
    {
        StartCoroutine(StopPlayerRoutine(time));
    }
    IEnumerator StopPlayerRoutine(float time)
    {
        if (canMove == false)
        {
            yield break;
        }
        ResetVelocity();
        canMove = false;
        ZeroGravity();
        yield return new WaitForSeconds(time);
        ResetGravity();
        canMove = true;
    }
    public void ResetVelocity()
    {
        velocity = Vector3.zero;
        m_rb.velocity = Vector3.zero;
    }
    public void ZeroGravity()
    {
        m_rb.gravityScale = 0;
    }
    public void ResetGravity()
    {
        m_rb.gravityScale = startingGravityScale;
    }
    public bool CheckIsGround()
    {
        bool bottomRightRay = Physics2D.Raycast(BottomRightPoint, Vector2.down, groundCheckDistance, GroundLayerMask);
        bool bottomLeftRay = Physics2D.Raycast(BottomLeftPoint, Vector2.down, groundCheckDistance, GroundLayerMask);

        return bottomRightRay || bottomLeftRay;
    }
    private void FixedUpdate()
    {
        if (canMove)
            m_rb.velocity = new Vector2(velocity.x, m_rb.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(BottomRightPoint, Vector2.down * groundCheckDistance);
        Gizmos.DrawRay(BottomLeftPoint, Vector2.down * groundCheckDistance);

    }
}
