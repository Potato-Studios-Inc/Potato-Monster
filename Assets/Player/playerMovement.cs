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
    public Camera[] cameras;
    
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
        UpdateActiveCamera();
        
        var inputX = Input.GetAxisRaw("Horizontal");

        isGrounded = IsGrounded();
        
        //Make player bounce when hitting wall but can't bounce when hitting the ground
        if (inputX != 0 && !isGrounded)
        {
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }
       

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

    private void UpdateActiveCamera()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            // Check if player is in camera view
            var playerPos = rb.position;
            var cameraPos = cameras[i].transform.position;
            var cameraSize = cameras[i].orthographicSize;
            var cameraWidth = cameraSize * cameras[i].aspect;
            var cameraHeight = cameraSize;
            var cameraMin = new Vector2(cameraPos.x - cameraWidth, cameraPos.y - cameraHeight);
            var cameraMax = new Vector2(cameraPos.x + cameraWidth, cameraPos.y + cameraHeight);
            var inCamera = playerPos.x > cameraMin.x && playerPos.x < cameraMax.x && playerPos.y > cameraMin.y && playerPos.y < cameraMax.y;                            
            cameras[i].gameObject.SetActive(inCamera);
        }
    }
}
