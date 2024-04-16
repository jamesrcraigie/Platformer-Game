using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float speed = 10f;
    private Animator animator;

    private bool canMove;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        canMove = true;
        StartCoroutine(DisableBullet(5f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    void Move()
    {
        if(canMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }

    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == MyTags.BEETLE_TAG || target.gameObject.tag == MyTags.SNAIL_TAG 
            || target.gameObject.tag == MyTags.SPIDER_TAG || target.gameObject.tag == MyTags.BOSS_TAG || target.gameObject.tag == MyTags.FROG_TAG)
        {
            animator.Play("Explode");
            canMove = false;
            StartCoroutine(DisableBullet(0.2f));

        }
    }
}
