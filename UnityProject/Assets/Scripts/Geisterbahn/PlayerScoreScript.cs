using System;
using TMPro;
using UnityEngine;

public class PlayerScoreScript : MonoBehaviour
{
    private static TMP_Text _scoreText;

    public void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
    }

    public static void SetScore(int score)
    {
        _scoreText.text = "Player Score: \n" + score;
    }
}
