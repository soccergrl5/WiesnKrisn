using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WiesnKrisn;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWonPanel;
    public static GameOverScript Instance { get; private set; }

    [SerializeField] private Button gameOverRetry;
    [SerializeField] private Button gameOverExit;
    
    [SerializeField] private Button gameWonRetry;
    [SerializeField] private Button gameWonExit;

    private bool _wonOnce;

    public void Awake()
    {
        Instance = this;
        
        gameOverRetry.onClick.AddListener(() =>
        {
            GameManager.Instance.ReplayMiniGame(Games.Dosenwerfen);
            SceneManager.LoadScene("DosenwerfenScene");
        });
        gameOverExit.onClick.AddListener(() =>
        {
            GameManager.Instance.ExitMiniGame(_wonOnce);
            Destroy(gameObject);
        });
        
        gameWonRetry.onClick.AddListener(() =>
        {
            GameManager.Instance.ReplayMiniGame(Games.Dosenwerfen);
            SceneManager.LoadScene("DosenwerfenScene");
        });
        gameWonExit.onClick.AddListener(() =>
        {
            GameManager.Instance.ExitMiniGame(_wonOnce);
            Destroy(gameObject);
        });
        
        DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
    }
    
    
    public void GameOver(bool isGameWon)
    {
        if (isGameWon)
        {
            gameWonPanel.SetActive(true);
            
            if (GameManager.Instance.GetPrizeOfGame(Games.Dosenwerfen) > GameManager.Instance.GetMoney())
                gameWonRetry.interactable = false;
            
            _wonOnce = true;
        }
        else
        {
            gameOverPanel.SetActive(true);
            
            if (GameManager.Instance.GetPrizeOfGame(Games.Dosenwerfen) > GameManager.Instance.GetMoney())
                gameOverRetry.interactable = false;
        }
    }
}
