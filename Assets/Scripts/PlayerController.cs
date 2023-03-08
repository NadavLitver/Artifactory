using System;
using System.Collections;
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
    [SerializeField] float horInput;
    [SerializeField] float startingGravityScale;
    [SerializeField] bool isLookingRight;
    [SerializeField] bool isFalling;
    [SerializeField] bool Jumping;
    [SerializeField] bool CoyoteAvailable;
    [SerializeField] float acceleration;
    [SerializeField] Animator m_animator;
    [SerializeField] AudioSource m_audioSource;
    private float currentSpeed;
    private float currentAccel;

    private GameObject jumpEffect;
    private GameObject landEffect;

    public UnityEvent OnFlip;

    private bool playingTraversal;
    [SerializeField] private float traversalTime;

    [SerializeField] private Transform jumpEffectPoint;
    [SerializeField] private Transform clawEffectPoint;
    [SerializeField] private ObjectPool jumpEffectPool;
    [SerializeField] private ObjectPool landEffectPool;

    [SerializeField] private SensorGroup OnsGroundCheck;


    [Header("Editable Properties"), Space(10)]
    [SerializeField, Range(0, 100)] float maxGravity;
    [SerializeField, Range(0, 100)] float GravityScale;
    [Range(0, 100), SerializeField, Tooltip("Increasing Acceleration Speed will Decrease the time the player takes to reach max speed")] float accelerationSpeed;
    [Range(0, 100), SerializeField, Tooltip("Increasing Acceleration Speed will Decrease the time the player takes to reach max speed")] float AirAccelerationSpeed;

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
    public Transform ClawEffectPoint { get => clawEffectPoint; }
    public Transform JumpEffectPoint { get => jumpEffectPoint; }
    public SensorGroup OnsGroundCheck1 { get => OnsGroundCheck; }

    private int FallingHash;
    private int GroundedHash;
    private int RunningHash;
    private int OnDandilionHash;
    private int DandTriggerHash;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponentInChildren<Animator>();
        StartingScale = transform.localScale;
        startingGravityScale = GravityScale;
        isLookingRight = true;
        OnsGroundCheck.OnNotGrounded.AddListener(TurnOnJumpEffect);
        OnsGroundCheck.OnGrounded.AddListener(TurnOnLandEffect);
        SetHashes();
    }
    private void SetHashes()
    {
        FallingHash = Animator.StringToHash("Falling");
        GroundedHash = Animator.StringToHash("Grounded");
        RunningHash = Animator.StringToHash("Running");
        OnDandilionHash = Animator.StringToHash("OnDandilion");
        DandTriggerHash = Animator.StringToHash("Dand");


    }
    void OnEnable()
    {
        canMove = true;
        velocity = Vector2.zero;


    }

    private void JumpPressed()
    {
        if ((OnsGroundCheck.IsGrounded() || CoyoteAvailable) && GameManager.Instance.inputManager.JumpDown() && canMove && !playingTraversal)
        {
            Jumping = true;
            velocity.y = jumpForce;
            m_animator.SetTrigger("Jump");
            StartCoroutine(JumpApexWait());
        }
        else if (isFalling)
        {
            Jumping = false;
        }
    }

    public IEnumerator TogglePlayingTraversal()
    {
        playingTraversal = true;
        yield return new WaitForSecondsRealtime(traversalTime);
        playingTraversal = false;
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

        GravityScale = apexGravityScale;
        yield return new WaitForSeconds(apexAirTimeGravityChange);

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

        m_animator.SetBool(GroundedHash, GetIsGrounded);
        m_animator.SetBool(RunningHash, (horInput != 0 && GetIsGrounded));
        isFalling = (!GetIsGrounded && velocity.y < -1f);
        m_animator.SetBool(FallingHash, isFalling);
    }
    private void SetVelocity()
    {
        horInput = GameManager.Instance.inputManager.GetMoveVector().x;
        if (canMove)
        {
            if (horInput > 0)
            {
                horInput = 1;
                if (!isLookingRight)
                {
                    isLookingRight = true;
                    Flip();
                    velocity.x *= -1;
                }

            }
            else if (horInput < 0)
            {
                horInput = -1;

                if (isLookingRight)
                {
                    isLookingRight = false;
                    Flip();
                    velocity.x *= -1;

                }
            }
            bool noInput = horInput == 0;
            float accelGoal = noInput ? 0 : 1;

            // this just sets maxSpeed;
            currentSpeed = GetIsGrounded ? speed : AirSpeed;

            if (GetIsGrounded)
            {
                currentAccel = noInput ? deaccelerationSpeed : accelerationSpeed;
            }
            else
            {
                currentAccel = noInput ? deaccelerationSpeed : AirAccelerationSpeed;

            }
         

            acceleration = Mathf.MoveTowards(acceleration, accelGoal, currentAccel * Time.deltaTime);

            //not good//y?
            velocity.x = Mathf.MoveTowards(velocity.x, noInput ? 0 : horInput * currentSpeed * acceleration, currentAccel * Time.deltaTime);
            externalForces = Vector2.MoveTowards(externalForces, Vector2.zero, accelerationSpeed * Time.deltaTime);

        }
    }
    public void Flip()
    {
        transform.localScale = new Vector3(isLookingRight ? StartingScale.x : -StartingScale.x, StartingScale.y, 1);
        OnFlip?.Invoke();
    }
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
        RaycastHit2D ceilingCheckRay = Physics2D.Raycast(transform.position, Vector2.up, ceilingCheckDistance, GroundLayerMask);
        if (ceilingCheckRay)
        {
            if (ceilingCheckRay.collider.CompareTag("Block"))
            {
                return true;
            }
        }

        return false;
    }
    private void FixedUpdate()
    {
        if (canMove)
            m_rb.velocity = velocity + externalForces;
    }

    public void SetOnDandilion(bool onDandilion)
    {
        if (onDandilion)
        {
            Animator.SetTrigger(DandTriggerHash);
            Animator.SetBool(OnDandilionHash, onDandilion);

        }
        else
        {
            Animator.SetBool(OnDandilionHash, onDandilion);
        }
    }
    public void RecieveForce(Vector2 force)
    {
        GetExternalForces += force;
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = CheckIsCeiling() ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, Vector2.up * groundCheckDistance);
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
