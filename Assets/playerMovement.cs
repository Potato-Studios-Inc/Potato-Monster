using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float walkSpeed;
    private float moveInput;
    private Rigidbody2D rb;
    public float jumpValue = 0.0f;
    
    public LayerMask groundMask;
    
    public bool canJump = true;
    public bool isGrounded;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
        
        isGrounded = Physics2D.OverlapBox(
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), 
            new Vector2(0.9f, 0.4f), 0f, groundMask);
        
        if (Input.GetKey("space") && isGrounded && canJump)
        {
            jumpValue += 0.1f;
        }
        
        if (jumpValue >= 20f && isGrounded)
        {
            float tempx = moveInput * walkSpeed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
        }
        
        
        //Flip();
    }

    /*
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
    }
    */

    /*
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    */
}
