using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardMouseSript : MonoBehaviour
{
    public Rigidbody2D rb;

    private Vector2 mWorldPos;
    private Vector2 mPos;

    private void FixedUpdate()
    {
        // follow the mouse
        mPos = Mouse.current.position.ReadValue();
        mWorldPos = Camera.main.ScreenToWorldPoint(mPos);
        rb.position = mWorldPos;
    }
}
