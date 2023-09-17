using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerKeyboard : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputActions PlayerControls;

    [Header("Move info")]
    [SerializeField] private float MoveSpeed = 5.0f;
    [SerializeField] private float JumpSpeed = 5f;
    Vector2 moveDirection = Vector2.zero;

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction crouch;

   
    public float DoubleJumpSpeedMultiplier = 1.2f;
    public float isWallSlidingSpeed; 
    private bool CanDoubleJump;
    private bool CanWallSlide;
    private bool isWallSliding;

    private bool isFacingRight = true;
    private float FacingDirection = 1;
    [SerializeField] private Vector2 wallJumpDirection;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;
    private bool isGrounded;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallcheckDistance;
    private bool isWalled;



    private void Awake()
    {
        PlayerControls = new PlayerInputActions();

        rb = GetComponent<Rigidbody2D>();

        isGrounded = true;

        //anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        move = PlayerControls.PlayerKeyboard.Move;
        move.Enable();

        jump = PlayerControls.PlayerKeyboard.Jump;
        jump.Enable();
        jump.performed += Jump;

        fire = PlayerControls.PlayerKeyboard.Fire;
        fire.Enable();
        fire.performed += Fire;
        //PlayerControls.Enable();

    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        fire.Disable();
        //PlayerControls.Disable();
    }

    void Update()
    {
        CollicionCheck();
        FlipController();

        if (isGrounded)
        {
            //CanMove = true;
            CanDoubleJump = true;
        }

        if (isWalled)
        {
            isWallSliding = true;
            CanDoubleJump = true;

        }

        moveDirection = move.ReadValue<Vector2>();
        
        //anim.SetBool("isGrounded", isGrounded);
        //anim.SetFloat("MoveSpeed", Mathf.Abs(moveDirection.x));
    }

    private void FixedUpdate()
    {
        if (isWalled && CanWallSlide)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -isWallSlidingSpeed, float.MaxValue));
        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x * MoveSpeed, rb.velocity.y);//, moveDirection.y * MoveSpeed);
        }

        // Check if the horizontal velocity has changed its sign.
        if ((isFacingRight && rb.velocity.x < 0) || (!isFacingRight && rb.velocity.x > 0))
        {
            Flip();
        }

    }

    //private bool isFacingRight = true;

    //private void Flip() //This is another way of flipping make a 
    //{
    //isFacingRight = !isFacingRight;
    //Vector3 localScale = transform.localScale;
    //localScale.x *= -1f;
    //transform.localcale = localScale;
    //}

    private void Flip()
    {
        FacingDirection = FacingDirection * -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (moveDirection.x > 0 && !isFacingRight)
            Flip();
        else if (moveDirection.x < 0 && isFacingRight)
            Flip();
    }

    //void CheckDirection()
    //{
    //if (rb.velocity.x < 0)
    //{
    //Flip(); //GetComponent<SpriteRenderer>().flipX = true;

    //}
    //else if (rb.velocity.x > 0)
    //{
    //Flip(); //GetComponent<SpriteRenderer>().flipX = false;
    //}
    //}

    void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(moveDirection.x, JumpSpeed);
        }
        else if (isWalled)
        {
            rb.velocity = new Vector2(10 * (-FacingDirection), JumpSpeed);
        }
        else if (CanDoubleJump)
        {
            CanDoubleJump = false;
            rb.velocity = new Vector2(moveDirection.x, JumpSpeed);
        }
    }

    //void DoubleJump()
    //{
        //rb.velocity = new Vector2(moveDirection.x, JumpSpeed * DoubleJumpSpeedMultiplier);
    //}

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("We Fired");
    }

    private void CollicionCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isWalled = Physics2D.Raycast(transform.position, Vector2.right * FacingDirection, wallcheckDistance, groundLayer);

        if (!isGrounded && rb.velocity.y < 0)
        {
            CanWallSlide = true;
        }
        if (!isWalled)
        {
            CanWallSlide = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallcheckDistance * FacingDirection, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
