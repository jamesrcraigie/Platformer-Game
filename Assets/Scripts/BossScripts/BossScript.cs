using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject bossStone;
    public Transform attackInstantiate;

    private Animator animator;

    private string coroutine_Name = "StartAttack";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(coroutine_Name);
    }

    void Attack()
    {
        print("attack");
        GameObject stone = Instantiate(bossStone, attackInstantiate.position, Quaternion.identity);
        stone.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700), 0f));
    }

    void BackToIdle()
    {
        animator.Play("Idle");
    }

    public void DeactivateBossScript()
    {
        StopCoroutine(coroutine_Name);
        enabled = false;
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        animator.Play("Attack");
        StartCoroutine(coroutine_Name);
    }
}
