using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator animator;

    private bool animation_Started;
    private bool animation_Finished;

    private int jumpedTimes;
    private bool jumpLeft = true;

    private string coroutine_Name = "FrogJump";

    private GameObject player;

    public LayerMask playerLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(coroutine_Name);
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
    }

    void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer))
        {
            player.GetComponent<PlayerDamage>().DealDamage();
        }
    }

    void LateUpdate()
    {
        if (animation_Finished && animation_Started)
        {
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

        jumpedTimes++;

        if (jumpLeft)
        {
            animator.Play("FrogJumpLeft");
        } else
        {
            animator.Play("FrogJumpRight");
        }

        StartCoroutine(coroutine_Name);
    }

    void AnimationFinshed()
    {
        animation_Finished = true;
        
        if(jumpLeft)
        {
            animator.Play("FrogIdleLeft");
        }
        else
        {
            animator.Play("FrogIdleRight");
        }
        

        if(jumpedTimes == 3)
        {
            jumpedTimes = 0;
            Vector3 tempScale = transform.localScale;
            tempScale *= -1f;
            transform.localScale = tempScale;

            jumpLeft = !jumpLeft;
        }
    }
}
