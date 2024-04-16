using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    public Rigidbody2D myBody;
    public Animator animator;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;

    private float jumpPower = 10f;


    void Awake()
    {
        animator = GetComponent<Animator>();
       myBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        CheckIfGrounded();
        PlayerJump();

    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal > 0) 
        { 
            myBody.velocity = new Vector2(speed,myBody.velocity.y);

            ChangeDirection(1);
        }
        else if(horizontal < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);

            ChangeDirection(-1);
        } else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        animator.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded) 
        {
            if(jumped)
            {
                jumped = false;

                animator.SetBool("Jump", false);
            }
        }
    }
    void PlayerJump()
    {
        if (isGrounded)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);

                animator.SetBool("Jump", true);
            }
        }
    }
}
