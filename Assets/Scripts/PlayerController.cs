using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool facingRight = true;

    private Rigidbody2D rb;

    public bool isGrounded;
    public bool isWall;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private int extraJumps;
    public int extraJumpsValue;


    // Start is called before the first frame update
    void Start()
    {
        
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, groundLayer);
        //isWall = Physics2D.OverlapBox(transform.position, )
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);


        if (facingRight == true && moveInput > 0) {
            Flip();
        }else if (facingRight == false && moveInput < 0){
            Flip();
        }
    }

    void Update() {

        if (isGrounded == true) {
            extraJumps = 2;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && extraJumps > 0) {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
              Debug.Log("Jump");
        }else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || 
        extraJumps == 0 && isGrounded == true){
            rb.velocity = Vector2.up * jumpForce;
              Debug.Log("Jump");
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }


    void OnCollisionStay2D(Collision2D col) 
    {   
        if (col.gameObject.layer == groundLayer) {
            Debug.Log("ground");
            isGrounded = true;
            rb.gravityScale = 5;
        }

        if (col.gameObject.layer == wallLayer) 
        {
            isWall = true;
            rb.gravityScale = 0;
        }

    }

    
    void OnCollisionExit2D(Collision2D col) 
    {
        if (col.gameObject.layer == groundLayer) {
            Debug.Log("Not ground");
            isGrounded = false;
            rb.gravityScale = 5;
        }

        if (col.gameObject.layer == wallLayer) {
            isWall = false;
            rb.gravityScale = 5;
        }
    }

}
