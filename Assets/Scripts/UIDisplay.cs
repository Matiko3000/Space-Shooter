using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider HealthBar;

    Health health;
    ScoreKeeper scoreKeeper;

    int startingHealth;

    void Awake()
    {
        health = FindObjectOfType<Health>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        startingHealth = health.GetCurrentHealth();
    }

    void Update()
    {
        //Debug.Log(health.GetCurrentHealth());
        scoreText.text = AddZeros(scoreKeeper.GetCurrentScore().ToString());
        HealthBar.value = (float)health.GetCurrentHealth() / (float)startingHealth;
    }

    private string AddZeros(string score)
    {
        while(score.Length < 8)
        {
            score = "0" + score;
        }

        return score;
    }
}
