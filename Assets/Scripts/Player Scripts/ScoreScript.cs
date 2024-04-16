using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private TMP_Text coinScoreText;
    public AudioSource audioManager;
    private int score = 0;

    void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }

    void Start()
    {
        coinScoreText = GameObject.Find("CoinText").GetComponent<TMP_Text>(); 
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.COIN_TAG)
        {
            target.gameObject.SetActive(false);
            score++;

            DisplayScore();

            audioManager.Play();
        }
    }

    public void IncreaseScore(int value)
    {
        score += value;
        DisplayScore();
    }

    private void DisplayScore()
    {
        coinScoreText.text = $"x{score}";
    }
}
