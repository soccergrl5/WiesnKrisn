using UnityEngine;

public class CountingScript : MonoBehaviour
{
    //Keeps count of cans and balls thrown. Can or Ball Script can't do that, because they are attached to every object
    private static CountingScript _instance;
    private int _amountOfBallsThrown = 5;
    private int _playerScore = 0;
    
    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        _amountOfBallsThrown = 5;
        _playerScore = 0;
    }

    public static CountingScript Instance()
    {
        return _instance;
    }

    public int GetAmountOfBallsThrown()
    {
        return _amountOfBallsThrown;
    }

    public void AddToAmountOfBallsThrown(int amount)
    {
        _amountOfBallsThrown -= amount;
    }

    public int GetPlayerScore()
    {
        return _playerScore;
    }

    public void AddToPlayerScore(int score)
    {
        _playerScore += score;
    }
}
