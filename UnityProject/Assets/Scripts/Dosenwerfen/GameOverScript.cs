using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameWonPanel;
    public static GameOverScript Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
        
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
    }
    
    
    public void GameOver(bool isGameWon)
    {
        if (isGameWon)
        {
            gameWonPanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(true);
        }
    }
}
