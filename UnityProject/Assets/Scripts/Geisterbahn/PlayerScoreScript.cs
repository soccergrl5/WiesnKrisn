using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerScoreScript : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private static PlayerScoreScript _instance;

    public void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void SetScore(int score)
    {
        if (scoreText != null)
        {
            if (scoreText.text != null)
            {
                scoreText.SetText("Player Score: \n" + score);
            }
        }



    }

    public static PlayerScoreScript Instance()
    {
        return _instance;
    }
}
