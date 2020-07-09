using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f, enemyDetectionRange = 0.1f, topHitDetectionRange = 0.2f, bounceStrength = 7f;
    public Transform left_Collision, right_Collision, top_Collision, bottom_Collision;
    public LayerMask playerLayer;

    private Rigidbody2D myBody;
    private Animator anim;
    private Vector3 LCP, RCP;

    private bool moveLeft;
    private bool canMove;
    private bool stunned;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        LCP = left_Collision.localPosition;
        RCP = right_Collision.localPosition;
        
    }
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("LCP:" + LCP + " RCP:" + RCP);
        //Debug.Log("LDP:" + left_Collision.position + " RDP:" + right_Collision.position);
        if (canMove)
        {
            MoveSnail();
        }
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
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, enemyDetectionRange, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, enemyDetectionRange, playerLayer);

        //Circular raycast for detection in a circular area around the top_Collision.position
        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, topHitDetectionRange, playerLayer);

        if(topHit != null)
        {
            if(topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if(!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounceStrength);

                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                    
                    anim.Play("Stunned");
                    stunned = true;

                    //Beetle specific code
                    if(tag == MyTags.BEETLE_TAG)
                    {
                        anim.Play("Stunned");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }
        
        if(leftHit)
        {
            if(leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if(!stunned)
                {
                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                    //Apply damage to player
                    Debug.Log("Left Hit");
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                    //Apply damage to player
                    Debug.Log("Right Hit");
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }
            
        //If we don't detect collision anymore
        if (!Physics2D.Raycast(bottom_Collision.position, Vector2.down, 0.1f))
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

            //RCP = right_Collision.position;
            //LCP = left_Collision.position;
            left_Collision.localPosition = LCP;
            right_Collision.localPosition = RCP;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);

            //LCP = right_Collision.position;
            //RCP = left_Collision.position;
            left_Collision.localPosition = RCP;
            right_Collision.localPosition = LCP;
        }

        transform.localScale = tempScale;
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == MyTags.BULLET_TAG)
        {
            if(tag == MyTags.BEETLE_TAG)
            {
                anim.Play("Stunned");

                canMove = false;
                myBody.velocity = new Vector2(0, 0);

                StartCoroutine(Dead(0.4f));
            }
            if(tag == MyTags.SNAIL_TAG)
            {
                if(!stunned)
                {
                    anim.Play("Stunned");
                    stunned = true;
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
