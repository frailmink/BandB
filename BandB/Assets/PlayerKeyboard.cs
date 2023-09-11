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
    private bool CanWallSlide;
    private bool isWallSliding;
    private bool doubleJump;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallcheckDistance;
    private bool isWalled;

    
    
    private void Awake()
    {
        PlayerControls = new PlayerInputActions();

        rb = GetComponent<Rigidbody2D>();

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

        moveDirection = move.ReadValue<Vector2>();
        //rb.velocity = new Vector3(horizontal * MoveSpeed, rb.velocity.y);

        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        //if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //if (isGrounded)
            {
                //Jump();

            }
            //else
            {
                //if (doubleJump)
                {

                    //DoubleJump();
                    //doubleJump = false;
                }
            }
        }
        //if (isGrounded)
        {

            //doubleJump = true;
        }

        //anim.SetBool("isGrounded", isGrounded);
        //anim.SetFloat("MoveSpeed", Mathf.Abs(moveDirection.x));
    }

    private void FixedUpdate()
    {
        if (isWalled && CanWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }
        else
        {
            isWallSliding = false;
            rb.velocity = new Vector2(moveDirection.x * MoveSpeed, rb.velocity.y);//, moveDirection.y * MoveSpeed);
        }


        CheckDirection();
    }

    //private bool isFacingRight = true;

    //private void Flip() //This is another way of flipping make a 
    //{
    //isFacingRight = !isFacingRight;
    //Vector3 localScale = transform.localScale;
    //localScale.x *= -1f;
    //transform.localcale = localScale;
    //}

    void CheckDirection()
    {
        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;

        }
        else if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void Jump(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector2(moveDirection.x, JumpSpeed);
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        isWalled = Physics2D.Raycast(wallCheck.position, Vector2.right, wallcheckDistance, groundLayer);

        if (!isGrounded && rb.velocity.y < 0)
        {
            CanWallSlide = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, 0.1f);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallcheckDistance,
                                                        wallCheck.position.y,
                                                        wallCheck.position.z));
    }
}
