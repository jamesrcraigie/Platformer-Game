using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    private TMP_Text lifeText;
    private int lifeScoreCount;

    private bool canDamage;

    void Awake()
    {
        lifeText = GameObject.Find("LivesText").GetComponent<TMP_Text>();
        lifeScoreCount = 3;
        lifeText.text = $"x{lifeScoreCount}";

        canDamage = true;
    }

    void Start()
    {
        Time.timeScale = 1.0f;
    }
    public void DealDamage()
    {
       if(canDamage)
        {
            lifeScoreCount--;

            if (lifeScoreCount >= 0)
            {
                lifeText.text = $"x{lifeScoreCount}";
            }

            if (lifeScoreCount == 0)
            {
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }

            canDamage = false;

            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);

        canDamage = true;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
