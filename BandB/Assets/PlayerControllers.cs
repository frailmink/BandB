using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerControllers : MonoBehaviour
{
    Rigidbody2D rb;
    public float MoveSpeed = 1.0f;
    public InputAction PlayerControls;

    public float JumpSpeed = 5f;
    public float DoubleJumpSpeedMultiplier = 1.2f;
    bool doubleJump = true;
    float xInput;

    public Transform groundCheck;
    public LayerMask groundLayer;

    bool isGrounded;
    Animator anim;


    Vector2 moveDirection = Vector2.zero;
    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        PlayerControls.Enable();
       
    }
    private void OnDisable()
    {
        PlayerControls.Disable();
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = PlayerControls.ReadValue<Vector2>();

        //xInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xInput * MoveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                Jump();

            }
            else
            {
                if (doubleJump)
                {

                    DoubleJump();
                    doubleJump = false;
                }
            }
        }
        if (isGrounded)
        {

            doubleJump = true;
        }

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("MoveSpeed", Mathf.Abs(rb.velocity.x));
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * MoveSpeed, rb.velocity.y);//, moveDirection.y * MoveSpeed);

        CheckDirection();
    }
    void CheckDirection()
    {
        if (rb.velocity.x< 0)
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
        rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
    }

    void DoubleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, JumpSpeed * DoubleJumpSpeedMultiplier);
    }
}
