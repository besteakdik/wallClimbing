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
    public Transform groundCheck;
    public float checkRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private int extraJumps;
    public int extraJumpsValue;

    [Header("Wall Collision Variables")]
    [SerializeField] private float _wallRaycastLength;
    public bool _onWall;
    public bool _onRightWall;


    // Start is called before the first frame update
    void Start()
    {
        
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCollisions();
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


    private void CheckCollisions()
    {
        /* //Ground Collisions
        _onGround = Physics2D.Raycast(transform.position + _groundRaycastOffset, Vector2.down, _groundRaycastLength, _groundLayer) ||
                    Physics2D.Raycast(transform.position - _groundRaycastOffset, Vector2.down, _groundRaycastLength, _groundLayer);

        //Corner Collisions
        _canCornerCorrect = Physics2D.Raycast(transform.position + _edgeRaycastOffset, Vector2.up, _topRaycastLength, _cornerCorrectLayer) &&
                            !Physics2D.Raycast(transform.position + _innerRaycastOffset, Vector2.up, _topRaycastLength, _cornerCorrectLayer) ||
                            Physics2D.Raycast(transform.position - _edgeRaycastOffset, Vector2.up, _topRaycastLength, _cornerCorrectLayer) &&
                            !Physics2D.Raycast(transform.position - _innerRaycastOffset, Vector2.up, _topRaycastLength, _cornerCorrectLayer); */

        //Wall Collisions
        _onWall = Physics2D.Raycast(transform.position, Vector2.right, _wallRaycastLength, wallLayer) ||
                    Physics2D.Raycast(transform.position, Vector2.left, _wallRaycastLength, wallLayer);
        _onRightWall = Physics2D.Raycast(transform.position, Vector2.right, _wallRaycastLength, wallLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        /* //Ground Check
        Gizmos.DrawLine(transform.position + _groundRaycastOffset, transform.position + _groundRaycastOffset + Vector3.down * _groundRaycastLength);
        Gizmos.DrawLine(transform.position - _groundRaycastOffset, transform.position - _groundRaycastOffset + Vector3.down * _groundRaycastLength);

        //Corner Check
        Gizmos.DrawLine(transform.position + _edgeRaycastOffset, transform.position + _edgeRaycastOffset + Vector3.up * _topRaycastLength);
        Gizmos.DrawLine(transform.position - _edgeRaycastOffset, transform.position - _edgeRaycastOffset + Vector3.up * _topRaycastLength);
        Gizmos.DrawLine(transform.position + _innerRaycastOffset, transform.position + _innerRaycastOffset + Vector3.up * _topRaycastLength);
        Gizmos.DrawLine(transform.position - _innerRaycastOffset, transform.position - _innerRaycastOffset + Vector3.up * _topRaycastLength);

        //Corner Distance Check
        Gizmos.DrawLine(transform.position - _innerRaycastOffset + Vector3.up * _topRaycastLength,
                        transform.position - _innerRaycastOffset + Vector3.up * _topRaycastLength + Vector3.left * _topRaycastLength);
        Gizmos.DrawLine(transform.position + _innerRaycastOffset + Vector3.up * _topRaycastLength,
                        transform.position + _innerRaycastOffset + Vector3.up * _topRaycastLength + Vector3.right * _topRaycastLength);
 */
        //Wall Check
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * _wallRaycastLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * _wallRaycastLength);
    }



/*     void OnCollisionStay2D(Collision2D col) 
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
    } */

}
