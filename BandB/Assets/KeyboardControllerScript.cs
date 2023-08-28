using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardControllerScript : MonoBehaviour
{
    public InputAction input;
    public Rigidbody2D rb;
    public float velocity;

    private Vector2 move;

    void OnEnable()
    {
        input.Enable();
    }

    private void Awake()
    {
        // reads the value of the controller and saves it in the move variable
        input.performed += ctx => move = ctx.ReadValue<Vector2>();
        input.canceled += ctx => move = Vector2.zero;
    }

    private void FixedUpdate()
    {
        // follow the movement of the controller

        // moveDirection = input.ReadValue<Vector2>();
        // rb.velocity = new Vector2(moveDirection.x * velocity, moveDirection.y * velocity);
        // old code ^^

        Vector2 m = new Vector2(-move.x, move.y);
        transform.Translate(m, Space.World);
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
