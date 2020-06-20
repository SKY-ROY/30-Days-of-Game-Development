using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 10f;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;
    
    private Rigidbody2D myBody;
    private Animator anim;

    private bool isGrounded;
    private bool jumped;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        /*
        if (Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundLayer))
        {
            Debug.Log("Collided with ground");
        }
        */
        CheckIfGrounded();
        PlayerJump();
        
    }

    private void FixedUpdate()
    {
        PlayerWalk();
    }

    private void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if(h>0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);

            ChangeDirection(1);
        }
        else if(h<0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));//Walk animation
    }
    private void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if(isGrounded)
        {
            //player jumped before but now is on ground therefore reset the jumped bool
            if(jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);//back to previous animation-state
            }
        }
    }

    void PlayerJump()
    {
        if(isGrounded)
        {
            if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                
                anim.SetBool("Jump", true);//Jump animation
            }
        }
    }
}