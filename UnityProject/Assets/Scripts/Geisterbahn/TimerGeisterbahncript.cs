using TMPro;
using UnityEngine;

public class TimerGeisterbahncript : MonoBehaviour
{
    private static float totalTime = 30f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject ghostSpawner;
    

    void Start()
    {
        timerText.text = "00:00:00";

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
        if (StoringPointsScript.GetPlayerPoints() > 800)
        {
            GameOverScript.Instance.GameOver(true);
        }
        else
        {
            GameOverScript.Instance.GameOver(false);
        }
    }
}
