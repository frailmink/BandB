using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeMoveScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float UpSpeed = 500;
    public float Speed;
    float xInput;
    float yInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * UpSpeed);

        }

        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");


        rb.AddForce(xInput * Speed, 0, yInput * Speed);


        //if (Input.GetKeyDown(KeyCode.D))
        {
            //  rb.AddForce(Vector3.right * Speed);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
