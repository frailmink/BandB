using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player2Controllers : MonoBehaviour
{
    Rigidbody2D rb;
    public float MoveSpeed = 1.0f;
    public PlayerInputActions PlayerControls;

    public float JumpSpeed = 5f;
    public float DoubleJumpSpeedMultiplier = 1.2f;
    bool doubleJump = true;
    float xInput;

    public Transform groundCheck;
    public LayerMask groundLayer;

    bool isGrounded;
    Animator anim;

    private InputAction move;
    private InputAction fire;

    Vector2 moveDirection = Vector2.zero;
    private void Awake()
    {
        PlayerControls = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        move = PlayerControls.Player.Move;
        move.Enable();
        fire.Enable();
        fire.performed += Fire;
        //PlayerControls.Enable();

    }
    private void OnDisable()
    {
        move.Disable();
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

        //xInput = Input.GetAxisRaw("Horizontal");
        //rb.velocity = new Vector2(xInput * MoveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

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
        anim.SetFloat("MoveSpeed", Mathf.Abs(moveDirection.x));
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * MoveSpeed, moveDirection.y);//, moveDirection.y * MoveSpeed);

        CheckDirection();
    }
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

    void Jump()
    {
        rb.velocity = new Vector2(moveDirection.x, JumpSpeed);
    }

    void DoubleJump()
    {
        rb.velocity = new Vector2(moveDirection.x, JumpSpeed * DoubleJumpSpeedMultiplier);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("We Fired");
    }
}
