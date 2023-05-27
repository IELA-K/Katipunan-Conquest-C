using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float jumpingPower = 16f;
    public Animator anim;
    private float Move;
    private Rigidbody2D rb;
    public float speed;
    public float jump;
    public bool isFacingRight;

    void Start()
    {
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    
    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(Move * speed, rb.velocity.y);


        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        if(Move >= 0.1f || Move <= -0.1f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if(!isFacingRight && Move > 0f)
        {
            Flip();
        }
        else if(isFacingRight && Move< 0f)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
        }
    }

   

    private void Flip()
    {
       
        
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        
    }
}
