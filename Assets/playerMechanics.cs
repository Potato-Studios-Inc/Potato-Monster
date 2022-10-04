using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playerMechanics : MonoBehaviour
{
    public float walkSpeed;
    private float moveInput;
    private Rigidbody2D rb;
    public bool isGrounded;
    public LayerMask groundMask;
    
    public bool canJump = true;
    public float jumpValue = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        
        rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.0f),
            new Vector2(0.9f, 0.4f), 0f, groundMask);
    }
}
