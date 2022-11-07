using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float walkSpeed = 3.0f;
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
    
    private bool IsGrounded() {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        const float distance = 0.5f;
    
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundMask);
        return hit.collider != null;
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var inputX = Input.GetAxisRaw("Horizontal");

        isGrounded = IsGrounded();

        rb.sharedMaterial = !isGrounded ? bounceMat : normalMat;

        if (jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(inputX * walkSpeed, rb.velocity.y);
        }

        if (Input.GetKeyDown("space") && isGrounded && canJump)
        {
            OnStartAimingToJump();
        }

        if (Input.GetKey("space") && isGrounded && canJump && _isAimingToJump)
        {
            OnAimingToJump();
        }

        if (jumpValue >= 10f || Input.GetKeyUp("space"))
        {
            OnJump();
        }
        _animator.SetBool(IsAimingToJump, _isAimingToJump);
        _animator.SetBool(IsWalking,inputX != 0);
    }

    private void OnStartAimingToJump()
    {
        rb.velocity = new Vector2(0.0f, rb.velocity.y);
        _isAimingToJump = true;
    }

    private void OnAimingToJump()
    {
        jumpValue += 10.0f * Time.deltaTime;
    }

    private void OnJump()
    {
        if (isGrounded)
        {
            var inputX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(inputX * walkSpeed, jumpValue);
            jumpValue = 0.0f;
            _isAimingToJump = false;
            canJump = true;
        }
    }
}
