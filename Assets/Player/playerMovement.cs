using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class playerMovement : MonoBehaviour
{
    private float walkSpeed = 1.3f;
    private const float JetPackSpeed = 2f;
    private const float BounceSpeed = 1.3f;
    private Rigidbody2D rb;
    public float jumpValue;

    public bool canJump = true;
    public bool isGrounded;
    private Animator _animator;
    private bool _isAimingToJump;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int IsAimingToJump = Animator.StringToHash("IsAimingToJump");
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip jumpLandingSound;
    public AudioClip bounceSound;
    public bool jetpackMode;
    private List<Collision2D> _collidedGroundObjects = new();

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        jumpSound = Resources.Load("PlayerSounds/jump") as AudioClip;
        jumpLandingSound = Resources.Load("PlayerSounds/jump landing") as AudioClip;
        bounceSound = Resources.Load("PlayerSounds/bounce") as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        var isDead = _animator.GetBool(IsDead);
        if (isDead)
        {
            return;
        }

        var inputX = Input.GetAxisRaw("Horizontal");
        var inputY = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.J))
        {
            jetpackMode = true;
        }

        var jetpackOn = jetpackMode && inputY > 0;

        //Player can walk only if jumpValue is 0 and isGrounded
        if (jumpValue == 0 && isGrounded || jetpackOn)
        {
            var x = inputX * walkSpeed;
            var y = rb.velocity.y;
            if (jetpackOn)
            {
                x = inputX * JetPackSpeed;
                y = inputY * JetPackSpeed;
            }

            rb.velocity = new Vector2(x, y);
        }

        if (Input.GetKeyUp("space"))
        {
            OnSpaceUp();
        }
        else if (Input.GetKeyDown("space"))
        {
            OnSpaceDown();
        }

        if (Input.GetKey("space"))
        {
            OnSpace();
        }

        if (jumpValue >= 6f)
        {
            OnJump();
        }

        _animator.SetBool(IsAimingToJump, _isAimingToJump);
        _animator.SetBool(IsWalking, inputX != 0);
    }

    private void OnSpaceDown()
    {
        if (isGrounded)
        {
            _isAimingToJump = true;
            _animator.SetBool(IsAimingToJump, true);
            if (canJump)
            {
                OnStartAimingToJump();
            }
        }
    }

    private void OnSpace()
    {
        if (isGrounded && canJump && _isAimingToJump)
        {
            OnAimingToJump();
        }
    }
    
    private void OnSpaceUp()
    {
        if (isGrounded)
        {
            _isAimingToJump = false;
            _animator.SetBool(IsAimingToJump, false);
            rb.velocity = new Vector2(rb.velocity.x, jumpValue);
            OnJump();
        } else if (jumpValue > 0)
        {
            jumpValue = 0;
        }
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

    //play jumpSound only when player jumps
    private void OnCollisionEnter2D(Collision2D other)
    {
        var isGround = other.gameObject.CompareTag("Ground");
        var isWall = other.gameObject.CompareTag("Wall");
        if (isGround)
        {
            _collidedGroundObjects.Add(other);
            if (!isGrounded)
            {
                OnGroundEnter();
                isGrounded = true;
            }
        }
        else if (isWall)
        {
            OnWallCollision(other);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        var isGround = other.gameObject.CompareTag("Ground");
        if (isGround)
        {
            _collidedGroundObjects.Remove(other);
            isGrounded = _collidedGroundObjects.Count > 0;
            if (!isGrounded)
            {
                OnGroundExit();
            }
        }
    }

    private void OnGroundEnter()
    {
        audioSource.PlayOneShot(jumpLandingSound, 0.7f);
    }

    private void OnGroundExit()
    {
    }

    private void OnWallCollision(Collision2D wall)
    {
        var wallOnTheRightSide = wall.transform.position.x > transform.position.x;
        audioSource.PlayOneShot(bounceSound, 0.7f);
        rb.velocity = new Vector2(wallOnTheRightSide ? -BounceSpeed : BounceSpeed, rb.velocity.y);
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
}