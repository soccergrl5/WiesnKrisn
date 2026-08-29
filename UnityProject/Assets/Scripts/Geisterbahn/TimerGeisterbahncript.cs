using TMPro;
using UnityEngine;

public class TimerGeisterbahncript : MonoBehaviour
{
    private float totalTime = 30f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject ghostSpawner;
    private bool _isAllowedToCheck = true;

    public void Awake()
    {
        totalTime = 30f;
    }

    void Start()
    {
        timerText.text = "00:00:00";
        totalTime = 30f;
        _isAllowedToCheck = true;

    }
    void Update()
    {
        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
            
            var minutes = Mathf.FloorToInt(totalTime / 60);
            var seconds = Mathf.FloorToInt(totalTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = "Time's up";
            totalTime = 0;
            ghostSpawner.gameObject.SetActive(false);

            DetermineIfWon();
        }
    }

    private void DetermineIfWon()
    {
        if (_isAllowedToCheck)
        {
            _isAllowedToCheck = false;
            if (StoringPointsScript.Instance().GetPlayerPoints() > 800)
            {
                print("Hihi");
                GameOverScript.Instance.GameOver(true);
            }
            else
            {
                print("hihi");
                GameOverScript.Instance.GameOver(false);
            }
        }
        
    }
}
