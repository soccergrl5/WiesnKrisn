using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    GameObject gameOverPanel;
    GameObject gameWonPanel;
    public static GameOverScript Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        gameWonPanel = GameObject.FindGameObjectWithTag("GameWonPanel");

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
