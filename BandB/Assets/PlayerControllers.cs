using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerControllers : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputActions PlayerControls;

    [Header("Move info")]
    [SerializeField] private float MoveSpeed = 5.0f;
    [SerializeField] private float JumpSpeed = 5f;
    private float moveDirection;
    //private bool CanMove = true;

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction crouch;


    public float DoubleJumpSpeedMultiplier = 1.2f;
    private bool CanDoubleJump;
    private bool isWallSliding;
    private bool CanWallSlide;
    public float isWallSlidingSpeed;

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
        move = PlayerControls.PlayerController.Move;
        move.Enable();

        jump = PlayerControls.PlayerController.Jump;
        jump.Enable();
        jump.performed += Jump;

        fire = PlayerControls.PlayerController.Fire;
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

        moveDirection = move.ReadValue<float>();      

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
            rb.velocity = new Vector2(moveDirection * MoveSpeed, rb.velocity.y);//, moveDirection.y * MoveSpeed);
        }

        //CheckDirection();
        // Check if the horizontal velocity has changed its sign.
        //if ((isFacingRight && rb.velocity.x < 0) || (!isFacingRight && rb.velocity.x > 0))
        //{
        //Flip();
        //}

    }

    //private void Flip() //This is another way of flipping make a 
    //{
    //isFacingRight = !isFacingRight;
    //Vector3 localScale = transform.localScale;
    //localScale.x *= -1f;
    //transform.localScale = localScale;
    //}

    private void Flip()
    {
        FacingDirection = FacingDirection * -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180,0);
    }

    private void FlipController()
    {
        if (moveDirection > 0 && !isFacingRight)
            Flip();
        else if (moveDirection < 0 && isFacingRight)
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
            rb.velocity = new Vector2(moveDirection, JumpSpeed);
        }
        else if (isWalled)
        {
            rb.velocity = new Vector2(10 * (-FacingDirection), JumpSpeed);
        }
        else if (CanDoubleJump)
        {
            CanDoubleJump = false;
            rb.velocity = new Vector2(moveDirection, JumpSpeed);
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
        isGrounded = Physics2D.Raycast(transform.position,Vector2.down, groundCheckDistance, groundLayer);
        isWalled = Physics2D.Raycast(transform.position,Vector2.right * FacingDirection, wallcheckDistance, groundLayer);

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
