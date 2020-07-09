using System.Collections;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator anim;
    private GameObject player;

    private bool animation_Started;
    private bool animation_Finished;

    private int jumpTimes;
    private bool jumpLeft = true;

    private string coroutine_Name = "FrogJump";

    public LayerMask playerLayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(coroutine_Name);
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
    }

    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer))
        {
            player.GetComponent<PlayerDamage>().DealDamage();
        }
    }
    private void LateUpdate()
    {
        //Debug.Log("LateUpdate");
        if(animation_Finished && animation_Started)
        {
            //Debug.Log("Position updated");
            animation_Started = false;

            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }
    
    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        animation_Started = true;
        animation_Finished = false;

        jumpTimes++;

        if (jumpLeft)
        {
            anim.Play("FrogJumpLeft");
        }
        else
        {
            anim.Play("FrogJumpRight");
        }

        StartCoroutine(coroutine_Name);
    }

    void AnimationFinished()
    {
        //Debug.Log("Animation Finished");
        animation_Finished = true;

        if (jumpLeft)
        {
            anim.Play("FrogIdleLeft");
        }
        else
        {
            anim.Play("FrogIdleRight");
        }

        if (jumpTimes == 3)
        {
            jumpTimes = 0;

            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1f;
            transform.localScale = tempScale;

            jumpLeft = !jumpLeft;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.BULLET_TAG)
        {
            gameObject.SetActive(false);
        }
    }
}
