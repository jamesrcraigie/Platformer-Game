using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{
    public Transform collisionBottom;

    private Animator animator;

    public LayerMask playerLayer;

    private Vector3 moveDirection = Vector3.up;
    private Vector3 originPosition;
    private Vector3 animatorPosition;
    private bool startAnimation;

    private GameObject player;

    private bool canAnimate = true;

    void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }
    void Start()
    {
        originPosition = transform.position;
        animatorPosition = transform.position;
        animatorPosition.y += 0.15f;
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
            RaycastHit2D hit = Physics2D.Raycast(collisionBottom.position, Vector2.down, 0.1f, playerLayer);

            if (hit)
            {
                if (hit.collider.gameObject.tag == MyTags.PLAYER_TAG)
                {
                    animator.Play("Idle");
                    startAnimation = true;
                    player.GetComponent<AudioSource>().Play();
                    player.GetComponent<ScoreScript>().IncreaseScore(5);
                    canAnimate = false;
                }
            }
        }
    }

    void AnimateUpDown()
    {
        if(startAnimation)
        {
            transform.Translate(moveDirection * Time.smoothDeltaTime);

            if(transform.position.y >= animatorPosition.y)
            {
                moveDirection = Vector3.down;
            }
            else if(transform.position.y <= originPosition.y)
            {
                 startAnimation = false;
            }
        }
    }
}
