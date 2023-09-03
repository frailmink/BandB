using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerKeyboard : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputActions PlayerControls;

    public float MoveSpeed = 5.0f;
    public float JumpSpeed = 5f;
    public float DoubleJumpSpeedMultiplier = 1.2f;
    //private bool doubleJump = true;
    //private bool isGrounded;
    private float horizontal;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    public Transform groundCheck;
    public LayerMask groundLayer;

    //private Animator anim;

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction crouch;

    Vector2 moveDirection = Vector2.zero;
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
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        //rb.velocity = new Vector3(horizontal * MoveSpeed, rb.velocity.y);

        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //if (isGrounded)
        //{
        //Jump();
        //doubleJump = true;
        //}
        //else
        //{
        //if (doubleJump)
        //{
        //DoubleJump();
        //doubleJump = false;
        //}
        //}
        //}


        //if (!isFacingRight && horizontal > 0f)
        //{
            //Flip();
        //}
        //else if (isFacingRight && horizontal < 0f)
        //{
            //Flip();
        //}


        //anim.SetBool("isGrounded", isGrounded);
        //anim.SetFloat("MoveSpeed", Mathf.Abs(moveDirection.x));
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * MoveSpeed, rb.velocity.y);//, moveDirection.y * MoveSpeed);

        CheckDirection();
    }

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
}
