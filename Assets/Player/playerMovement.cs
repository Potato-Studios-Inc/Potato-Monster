using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float walkSpeed = 1.3f;
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
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    public GameObject camerasParent;
    private Camera[] _cameras;
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip jumpLandingSound;
    public AudioClip bounceSound;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _cameras = camerasParent.GetComponentsInChildren<Camera>();
        audioSource = GetComponent<AudioSource>();
        jumpSound = Resources.Load("PlayerSounds/jump") as AudioClip;
        jumpLandingSound = Resources.Load("PlayerSounds/jump landing") as AudioClip;
        bounceSound = Resources.Load("PlayerSounds/bounce") as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateActiveCamera();
        var isDead = _animator.GetBool(IsDead);
        if (isDead)
        {
            return;
        }

        var inputX = Input.GetAxisRaw("Horizontal");
        
        //bool that checks if the player is falling
        var isFalling = rb.velocity.y < 0;

        //Check if player is grounded with raycast 
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, groundMask);

        //Make player bounce when hitting wall but can't bounce when hitting the ground
        if (inputX != 0 && !isGrounded)
        {
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }

        //When player jumps, player cant control arrow keys
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _isAimingToJump = true;
            _animator.SetBool(IsAimingToJump, true);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            _isAimingToJump = false;
            _animator.SetBool(IsAimingToJump, false);
            rb.velocity = new Vector2(rb.velocity.x, jumpValue);
        }

        //Player can walk only if jumpValue is 0 and isGrounded
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

        if (jumpValue >= 6f || Input.GetKeyUp("space") && isGrounded)
        {
            OnJump();
        }

        _animator.SetBool(IsAimingToJump, _isAimingToJump);
        _animator.SetBool(IsWalking, inputX != 0);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("on platform detected");
        audioSource.PlayOneShot(jumpLandingSound, 0.3f);
    }

    private void OnStartAimingToJump()
    {
        rb.velocity = new Vector2(0.0f, rb.velocity.y);
        _isAimingToJump = true;
    }

    private void OnAimingToJump()
    {
        //if quick press space, jumpValue will be 0.5f and if hold space it will increase
        if (jumpValue == 0.0f)
        {
            jumpValue = 2.5f;
        }
        else
        {
            jumpValue += 4.0f * Time.deltaTime;
        }
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
            audioSource.PlayOneShot(jumpSound, 0.7f);
        }
    }

    private void UpdateActiveCamera()
    {
        for (int i = 0; i < _cameras.Length; i++)
        {
            // Check if player is in camera view
            var playerPos = rb.position;
            var cameraPos = _cameras[i].transform.position;
            var cameraSize = _cameras[i].orthographicSize;
            var cameraWidth = cameraSize * _cameras[i].aspect;
            var cameraHeight = cameraSize;
            var cameraMin = new Vector2(cameraPos.x - cameraWidth, cameraPos.y - cameraHeight);
            var cameraMax = new Vector2(cameraPos.x + cameraWidth, cameraPos.y + cameraHeight);
            var inCamera = playerPos.x > cameraMin.x && playerPos.x < cameraMax.x && playerPos.y > cameraMin.y &&
                           playerPos.y < cameraMax.y;
            _cameras[i].gameObject.SetActive(inCamera);
        }
    }
}