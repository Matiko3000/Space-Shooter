using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
   

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        scoreText.text = "You scored:\n" + AddZeros(scoreKeeper.GetCurrentScore().ToString());
    }

    private string AddZeros(string score)
    {
        while (score.Length < 8)
        {
            score = "0" + score;
        }

        return score;
    }
}
