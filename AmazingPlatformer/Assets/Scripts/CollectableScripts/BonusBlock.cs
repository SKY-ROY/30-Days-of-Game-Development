using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{
    public enum BlockType
    {
        coin,
        powerUp,
    }

    private Animator anim;
    private Vector3 moveDirection = Vector3.up;
    private Vector3 originPosition;
    private Vector3 animPosition;
    private GameObject player;

    private bool startAnim;
    private bool canAnimate = true;

    public LayerMask playerLayer;
    public Transform bottomCollision;

    public BlockType thisBlock;

    private void Awake()
    {
        anim = GetComponent<Animator>();    
    }

    void Start()
    {
        originPosition = transform.position;
        animPosition = transform.position;

        animPosition.y += 0.15f;

        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
    }

    void Update()
    {
        CheckForCollision();
        AnimateUpDown();
    }
    
    void CheckForCollision()
    {
        if(canAnimate)
        {
            RaycastHit2D hit = Physics2D.Raycast(bottomCollision.position, Vector2.down, 0.1f, playerLayer);

            if (hit)
            {
                if (hit.collider.gameObject.tag == MyTags.PLAYER_TAG)
                {
                    if (thisBlock == BlockType.coin)
                    {
                        //increase coins
                        player.GetComponent<PlayerProgress>().IncrementCoinCount();
                    }
                    else if(thisBlock == BlockType.powerUp)
                    {
                        //change Player state
                    }
                    anim.Play("BonusBlockIdle");
                    startAnim = true;
                    canAnimate = false;
                }
            }
        }
    }

    void AnimateUpDown()
    {
        if(startAnim)
        {
            transform.Translate(moveDirection * Time.smoothDeltaTime);

            if(transform.position.y >= animPosition.y)
            {
                moveDirection = Vector3.down;
            }
            else if(transform.position.y <= originPosition.y)
            {
                startAnim = false; 
            }
        }
    }
}
