using UnityEngine;

public class tmpDetermineWinnerScript : MonoBehaviour
{
    public static void DetermineWinner()
    {
        if (ScoreCounter.GetPlayerScore() >= ScoreCounter.GetOpponentScore())
        {
            GameOverScript.Instance.GameOver(true);
        }
        else
        {
            GameOverScript.Instance.GameOver(false);
        }
    }
}
