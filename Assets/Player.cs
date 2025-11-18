using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }


    public PlayerInputSet input { get; private set; }
    public StateMachine stateMachine { get; private set; }


    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public  Player_WallJupmState wallJumpState { get; private set; }



    [Header("Movement details")]
    public float moveSpeed = 8f ;
    public float jumpForce = 5f;
    public Vector2 wallJumpForce;

    [Range(0,1)]
    public float wallSlidesMultiplier = 0.7f; // should be between 0 and 1
    [Range(0, 1)]
    public float inAirMoveMultiplier = 0.8f; // should be between 0 and 1
    private bool isFacingRight = true;
    public int facingDir { get; private set; } = 1;


    [Header("Collision detection")]
    public Vector2 moveInput {  get; private set; }
    [SerializeField] private float groundCheckDistance = 1.4f;
    [SerializeField] private float wallCheckDistance = 0.4f;
    [SerializeField] private LayerMask whatIsGround;
    public bool grounDetected { get; private set; }
    public bool wallDetected  { get; private set; }

    private void Awake()
    {

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine,"idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJupmState(this, stateMachine, "jumpFall");


    }
    private void OnEnable()
    {
        input.Enable();
        //InpurSystem.ActionMap.Action.AnyThings
        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }
    private void OnDisable()
    {
        input.Disable();
        
    }
    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandlerFlip(xVelocity);
    }



    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
        facingDir = facingDir * -1;
    }

    private void HandlerFlip(float xVelocity)
    {
        if(xVelocity > 0 && isFacingRight == false)
        {
            Flip();
        }
        else if(xVelocity < 0 && isFacingRight == true)
        {
            Flip();
        }
    }

    private void HandleCollisionDetection()
    {
        grounDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,  transform.position + new Vector3(0,-groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir,0 ));
    }
}
