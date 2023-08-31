using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerControllers : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputActions PlayerControls;

    public float MoveSpeed = 5.0f;
    public float JumpSpeed = 5f;
    public float DoubleJumpSpeedMultiplier = 1.2f;
    //private bool doubleJump = true;
    //private bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundLayer;

    //private Animator anim;

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction crouch;

    float moveDirection;
    private void Awake()
    {
        PlayerControls = new PlayerInputActions();

        rb = GetComponent<Rigidbody2D>();

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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<float>();
        


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
        rb.velocity = new Vector2(moveDirection * MoveSpeed, rb.velocity.y);//, moveDirection.y * MoveSpeed);

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

    void Jump(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector2(moveDirection, JumpSpeed);
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
