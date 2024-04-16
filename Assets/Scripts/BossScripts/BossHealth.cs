using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    private Animator animator;
    private int health = 10;

    private bool canDamage;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        canDamage = true;
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);

        canDamage = true;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(canDamage)
        {
            if (target.tag == MyTags.BULLET_TAG)
            {
                health--;
                canDamage = false;

                if (health == 0)
                {
                    GetComponent<BossScript>().DeactivateBossScript();
                    animator.Play("Dead");
                    StartCoroutine(DeactivateObject());
                }

                StartCoroutine(WaitForDamage());
            }
        }
    }

    IEnumerator DeactivateObject()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);

        SceneManager.LoadScene("MainMenu");
    }
}
