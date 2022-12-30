using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [Header("ReadOnly"), Space(10)]
    public bool canMove;
    [SerializeField] Rigidbody2D m_rb;
    [SerializeField] Vector2 velocity;
    [SerializeField] private Vector2 externalForces;
    [SerializeField] Vector2 StartingScale;
    [SerializeField] Vector2 BottomRightPoint;
    [SerializeField] Vector2 BottomLeftPoint;
    [SerializeField] private Vector2 topMidPoint;
    [SerializeField] float horInput;
    [SerializeField] float startingGravityScale;
    //[SerializeField] bool isGrounded;
    [SerializeField] bool isLookingRight;
    [SerializeField] bool isFalling;
    [SerializeField] Collider2D m_collider;
    [SerializeField] bool Jumping;
    [SerializeField] bool CoyoteAvailable;
    [SerializeField] float acceleration;
    [SerializeField] Animator m_animator;
    private float currentSpeed;
    private float currentAccel;

    private GameObject jumpEffect;
    private GameObject landEffect;

    [SerializeField] private Transform jumpEffectPoint;
    [SerializeField] private Transform clawEffectPoint;
    [SerializeField] private ObjectPool jumpEffectPool;
    [SerializeField] private ObjectPool landEffectPool;

    [SerializeField] private GroundCheckNew OnsGroundCheck;


    [Header("Editable Properties"), Space(10)]
    [SerializeField, Range(0, 100)] float maxGravity;
    [SerializeField, Range(0, 100)] float GravityScale;
    [Range(0, 100), SerializeField, Tooltip("Increasing Acceleration Speed will Decrease the time the player takes to reach max speed")] float accelerationSpeed;
    [Range(0, 100), SerializeField, Tooltip("Increasing deacceleration Speed will Decrease the time the player takes to reach zero speed on x")] float deaccelerationSpeed;

    [SerializeField, Tooltip("What is Ground?")] LayerMask GroundLayerMask;
    [Range(0, 100), SerializeField, Tooltip("Maximum Speed player can reach ")] float speed;
    [Range(0, 100), SerializeField, Tooltip("Maximum Speed player can reach ")] float AirSpeed;

    [Range(0, 1), SerializeField, Tooltip("how much time AFTER the player leaves a ground can the player jump, like a grace time ")] float CoyoteTime;

    [Range(0, 100), SerializeField, Tooltip("Increasing this number will increase the height the player jump to")] float jumpForce;
    [Range(0, 1), SerializeField, Tooltip("Raycast range to check ground probably no need to change anything")] float groundCheckDistance;
    [Range(0, 1), SerializeField, Tooltip("How much time does the gravity change apply after hitting apex in jump")] float apexAirTimeGravityChange;
    [Range(0, 100), SerializeField, Tooltip("What is the new gravity applied at apex")] float apexGravityScale;
    [Range(0, 1), SerializeField] float ceilingCheckDistance;


    public float GetHorInput { get => horInput; set => horInput = value; }
    public Rigidbody2D GetRb { get => m_rb; set => m_rb = value; }
    public Vector2 GetVelocity { get => velocity; set => velocity = value; }
    public bool GetIsLookingRight { get => isLookingRight; set => isLookingRight = value; }
    public bool GetIsGrounded { get => OnsGroundCheck.IsGrounded(); }
    public bool GetIsFalling { get => isFalling; set => isFalling = value; }
    public bool GetIsJumping { get => Jumping; set => Jumping = value; }
    public Vector2 GetExternalForces { get => externalForces; set => externalForces = value; }
    public bool GetCoyoteAvailable { get => CoyoteAvailable; set => CoyoteAvailable = value; }
    public Animator Animator { get => m_animator; set => m_animator = value; }
    public Transform ClawEffectPoint { get => clawEffectPoint; set => clawEffectPoint = value; }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        m_animator = GetComponentInChildren<Animator>();
        StartingScale = transform.localScale;
        startingGravityScale = GravityScale;
        isLookingRight = true;
        OnsGroundCheck.OnNotGrounded.AddListener(TurnOnJumpEffect);
        OnsGroundCheck.OnGrounded.AddListener(TurnOnLandEffect);
    }
    void OnEnable()
    {
        canMove = true;
        velocity = Vector2.zero;
     

    }
    public void GetColliderSizeInformation()
    {
        BottomRightPoint.x = m_collider.bounds.max.x - 0.6f;
        BottomRightPoint.y = m_collider.bounds.min.y + 0.1f;
        topMidPoint = (Vector2)m_collider.bounds.center + Vector2.up * (m_collider.bounds.size.y * 0.5f);
        BottomLeftPoint.y = m_collider.bounds.min.y + 0.1f;
        BottomLeftPoint.x = m_collider.bounds.min.x + 0.6f;

    }
    private void JumpPressed()
    {
        if ((OnsGroundCheck.IsGrounded()|| CoyoteAvailable) && GameManager.Instance.inputManager.JumpDown() && canMove)
        {
            Jumping = true;
            velocity.y = jumpForce;
            //isGrounded = false;
            m_animator.SetTrigger("Jump");
            StartCoroutine(JumpApexWait());
          //  TurnOnJumpEffect();
        }
        else if (isFalling)
        {
            Jumping = false;
        }
    }

    public void ExteriorJump()
    {
        Jumping = true;
        velocity.y = jumpForce;
        m_animator.SetTrigger("Jump");
    }
    public IEnumerator JumpApexWait()
    {
        yield return new WaitUntil(() => isFalling == true);
        Debug.Log("apexing");
        GravityScale = apexGravityScale;
        yield return new WaitForSeconds(apexAirTimeGravityChange);
        Debug.Log("apex done");
        GravityScale = startingGravityScale;
    }
    public IEnumerator JumpApexWait(float duration, float scale)
    {
        yield return new WaitUntil(() => isFalling == true);
        GravityScale = scale;
        yield return new WaitForSeconds(duration);
        GravityScale = startingGravityScale;
    }
    void Update()
    {
        SetVelocity();
        GetColliderSizeInformation();
        GetCoyoteAndSetGrounded();
        JumpPressed();
        Ceiling();
        SetAnimatorParameters();

    }

    private void Ceiling()
    {
        if (CheckIsCeiling())
        {
            if (m_rb.velocity.y > 0)
            {
               // ResetVelocity();
                velocity.y = 0;
                m_rb.velocity = new Vector2(m_rb.velocity.x, 0);
                externalForces.y = 0;

            }
            Debug.Log("Ceiling");
        }
    }

    private void GetCoyoteAndSetGrounded()
    {
        bool wasGrounded = GetIsGrounded;

        if (!OnsGroundCheck.IsGrounded())
        {
            velocity.y = Mathf.MoveTowards(velocity.y, -maxGravity, GravityScale * Time.deltaTime);
            if (wasGrounded && !Jumping)
            {

                StartCoroutine(SetCoyoteForTime());
            }
        }
        else
        {
            velocity.y = Mathf.MoveTowards(velocity.y, 0, GravityScale * Time.deltaTime);
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

        m_animator.SetBool("Grounded", GetIsGrounded);
        m_animator.SetBool("Running", (horInput != 0 && GetIsGrounded));
        isFalling = (!GetIsGrounded && velocity.y < -1f);
        m_animator.SetBool("Falling", isFalling);
    }
    private void SetVelocity()
    {
        horInput = GameManager.Instance.inputManager.GetMoveVector().x;
        if (canMove)
        {
            if (horInput > 0)
            {
                if (!isLookingRight)
                {
                    isLookingRight = true;
                    Flip();
                    velocity.x *= -1;
                }

            }
            else if (horInput < 0)
            {
                if (isLookingRight)
                {
                    isLookingRight = false;
                    Flip();
                    velocity.x *= -1;

                }
            }
            bool noInput = horInput == 0;
            float accelGoal = noInput ? 0 : 1;
            currentSpeed = GetIsGrounded ? speed : AirSpeed;
            currentAccel = noInput ? deaccelerationSpeed : accelerationSpeed;

            acceleration = Mathf.MoveTowards(acceleration, accelGoal, currentAccel * Time.deltaTime);
            velocity.x = Mathf.MoveTowards(velocity.x, horInput == 0 ? 0 : horInput * currentSpeed * acceleration, currentAccel * Time.deltaTime);
            externalForces = Vector2.MoveTowards(externalForces, Vector2.zero, accelerationSpeed * Time.deltaTime);

        }
    }
    public void Flip() => transform.localScale = new Vector3(isLookingRight ? StartingScale.x : -StartingScale.x, StartingScale.y, 1);
    public void ResetVelocity()
    {
        velocity = Vector2.zero;
        externalForces = Vector2.zero;
        m_rb.velocity = Vector3.zero;
    }
    public void ZeroGravity()
    {
        GravityScale = 0;
    }
    public void ResetGravity()
    {
        GravityScale = startingGravityScale;
    }
    public bool CheckIsCeiling()
    {
        RaycastHit2D ceilingCheckRay = Physics2D.Raycast(BottomRightPoint, Vector2.up, ceilingCheckDistance, GroundLayerMask);
        if (ceilingCheckRay)
        {
            if (ceilingCheckRay.collider.CompareTag("Block"))
            {
                return true;
            }
        }

        return false;
    }
   /* public bool CheckIsGround()
    {
        bool bottomRightRay = Physics2D.Raycast(BottomRightPoint, Vector2.down, groundCheckDistance, GroundLayerMask);
        bool bottomLeftRay = Physics2D.Raycast(BottomLeftPoint, Vector2.down, groundCheckDistance, GroundLayerMask);
       
        return bottomRightRay || bottomLeftRay;
    }*/
    private void FixedUpdate()
    {
        if (canMove)
            m_rb.velocity = velocity + externalForces;
    }
   
    public void SetOnDandilion(bool onDandilion)
    {
        if (onDandilion)
        {
            Animator.SetTrigger("Dand");
            Animator.SetBool("OnDandilion", onDandilion);

        }
        else
        {
            Animator.SetBool("OnDandilion", onDandilion);
        }
    }
    public void RecieveForce(Vector2 force)
    {
        GetExternalForces += force;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = GetIsGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(BottomRightPoint, Vector2.down * groundCheckDistance);

        Gizmos.DrawRay(BottomLeftPoint, Vector2.down * groundCheckDistance);
        Gizmos.color = CheckIsCeiling() ? Color.green : Color.red;
        Gizmos.DrawRay(topMidPoint, Vector2.up * groundCheckDistance);
    }

    public IEnumerator FreezePlayerForDuration(float time)
    {
        ResetVelocity();
        canMove = false;
        ZeroGravity();
        yield return new WaitForSecondsRealtime(time);
        canMove = true;
        ResetGravity();
    }

    public IEnumerator FreezePlayerForDuration(float time, float gravityScale)
    {
        ResetVelocity();
        canMove = false;
        GravityScale = gravityScale;
        yield return new WaitForSecondsRealtime(time);
        canMove = true;
        ResetGravity();
    }

    private void TurnOnJumpEffect()
    {
        jumpEffect = jumpEffectPool.GetPooledObject();
        jumpEffect.transform.position = jumpEffectPoint.position;
        jumpEffect.SetActive(true);
    }

    private void TurnOnLandEffect()
    {
        landEffect = landEffectPool.GetPooledObject();
        landEffect.transform.position = jumpEffectPoint.position;
        landEffect.SetActive(true);
    }
}
