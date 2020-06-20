using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform down_Collision;

    private Rigidbody2D myBody;
    private Animator anim;

    private bool moveLeft;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        moveLeft = true;
    }
    // Update is called once per frame
    void Update()
    {
        MoveSnail();
        CheckCollision();
    }
    void MoveSnail()
    {
        if (moveLeft)
        {
            myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
        }
        else
        {
            myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
        }
    }
    void CheckCollision()
    {
        //If we don't detect collision anymore
        if(!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }
    void ChangeDirection()
    {
        moveLeft = !moveLeft;

        Vector3 tempScale = transform.localScale;
        
        if(moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }

        transform.localScale = tempScale;
    }
}
