using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float walkSpeed = 6.0f;
    private float moveInput;
    private Rigidbody2D rb;
    public float jumpValue = 0.0f;

    public PhysicsMaterial2D bounceMat, normalMat;
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
        
        
        rb.sharedMaterial = jumpValue > 0 ? bounceMat : normalMat;
        
        if (jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
        }

        isGrounded = Physics2D.OverlapBox(
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), 
            new Vector2(0.9f, 0.0f), 0f, groundMask);

        if (Input.GetKeyDown("space") && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        if (Input.GetKey("space") && isGrounded && canJump)
        {
            jumpValue += 0.02f;
        }
        
        if (jumpValue >= 15f && isGrounded)
        {
            float tempx = moveInput * walkSpeed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.1f);
        }

        if (Input.GetKeyUp("space"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(moveInput * walkSpeed, jumpValue);
                jumpValue = 0.0f;
            }
            canJump = true;
        }
    }

    void ResetJump()
    {
        canJump = false;
        jumpValue = 0.0f;
    }
}
