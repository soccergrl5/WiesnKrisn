using TMPro;
using UnityEngine;

public class tmpScoreCounter : MonoBehaviour
{
    private static int _playerScore = 0;
    private static int _opponentScore = 0;
    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private TMP_Text opponentScoreText;


    public void Start()
    {
        playerScoreText.text = "Player Score : \n 0";
        opponentScoreText.text = "Opponent Score : \n 0";
    }

    public void Update()
    {
        playerScoreText.text = "Player Score : \n " + _playerScore;
        opponentScoreText.text = "Opponent Score : \n " + _opponentScore;

    }
    public static int GetPlayerScore()
    {
        return _playerScore;
    }

    public static int GetOpponentScore()
    {
        return _opponentScore;
    }

    public static void AddToPlayerScore(int newScore)
    {
        _playerScore += newScore;
    }

    public static void AddToOpponentScore(int newScore)
    {
        _opponentScore += newScore;
    }
}
