using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float walkSpeed = 2.0f;
    private Rigidbody2D rb;
    public float jumpValue = 0.0f;

    public PhysicsMaterial2D bounceMat, normalMat;
    public LayerMask groundMask;
    
    public bool canJump = true;
    public bool isGrounded;
    private Animator _animator;
    private bool _isAimingToJump = false;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int IsAimingToJump = Animator.StringToHash("IsAimingToJump");


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var inputX = Input.GetAxisRaw("Horizontal");

        rb.sharedMaterial = jumpValue > 0 ? bounceMat : normalMat;
        
        if (jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(inputX * walkSpeed, rb.velocity.y);
        }

        isGrounded = Physics2D.OverlapBox(
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), 
            new Vector2(0.9f, 0.0f), 0f, groundMask);

        if (Input.GetKeyDown("space") && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            _isAimingToJump = true;
        }

        if (Input.GetKey("space") && isGrounded && canJump)
        {
            jumpValue += 0.02f;
        }
        
        if (jumpValue >= 15f && isGrounded)
        {
            float tempx = inputX * walkSpeed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.1f);
            _isAimingToJump = false;
        }

        if (Input.GetKeyUp("space"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(inputX * walkSpeed, jumpValue);
                jumpValue = 0.0f;
            }
            canJump = true;
            _isAimingToJump = false;
        }
        _animator.SetBool(IsAimingToJump, _isAimingToJump);
        _animator.SetBool(IsWalking,inputX != 0);
    }

    void ResetJump()
    {
        canJump = false;
        jumpValue = 0.0f;
    }
}
