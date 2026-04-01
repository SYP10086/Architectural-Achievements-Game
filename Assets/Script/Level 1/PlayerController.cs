using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private SpriteRenderer SpriteRenderer;
    public float JumpForce = 3f;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    Animator animator;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       
        CheckGrounded();

        float dir = Input.GetAxis("Horizontal");


       /* Vector2 velocity = rb.velocity;
        velocity.x = dir * speed;
        rb.velocity = velocity;*/


        if (isGrounded )
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

      
        if (dir < 0)
        {
            SpriteRenderer.flipX = false;
        }
        else if (dir > 0)
        {
            SpriteRenderer.flipX = true;
        }

    
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (!DioManager.OnDio)
            {
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                animator.SetBool("IsJumping", true);
            }
        }

    
        if (rb.velocity.y < -0.1f && !isGrounded)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
            
        }
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    void CheckGrounded()
    {
        

        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
            
        }
    }
    private void FixedUpdate()
    {
        float dir = 0;
        if (!DioManager.OnDio)
        {
            dir = Input.GetAxis("Horizontal");
        }
        Vector2 velocity = rb.velocity;
        velocity.x = dir * speed;
        rb.velocity = velocity;
    }

}

