using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }




    private PlayerInputSet input;
    public StateMachine stateMachine { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Vector2 moveInput {  get; private set; }

    [Header("Movement details")]
    public float moveSpeed = 5f ;



    private bool isFacingRight = true;

    private void Awake()
    {

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();

        idleState = new Player_IdleState(this, stateMachine,"idle");
        moveState = new Player_MoveState(this, stateMachine, "move");

        input = new PlayerInputSet();
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
        stateMachine.UpdateActiveState();
    }
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandlerFlip(xVelocity);
    }



    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
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
}
