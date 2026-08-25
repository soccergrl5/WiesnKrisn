using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WiesnKrisn;

public class GameOverScript : MonoBehaviour
{
    private GameObject _gameOverPanel;
    private GameObject _gameWonPanel;
    
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
        _gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        _gameWonPanel = GameObject.FindGameObjectWithTag("GameWonPanel");

        _gameOverPanel.SetActive(false);
        _gameWonPanel.SetActive(false);
    }
    
    public void GameOver(bool isGameWon)
    {
        if (isGameWon)
        {
            _gameWonPanel.SetActive(true);
            
            if (GameManager.Instance.GetPrizeOfGame(Games.Dosenwerfen) > GameManager.Instance.GetMoney())
                gameWonRetry.interactable = false;
            
            _wonOnce = true;
        }
        else
        {
            _gameOverPanel.SetActive(true);
            
            if (GameManager.Instance.GetPrizeOfGame(Games.Dosenwerfen) > GameManager.Instance.GetMoney())
                gameOverRetry.interactable = false;
        }
    }
}
