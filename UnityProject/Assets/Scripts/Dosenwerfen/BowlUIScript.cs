using UnityEngine;

public class BowlScript1 : MonoBehaviour
{
    private bool _hasActivatedGameOverAlready = false;
    public void Awake()
    {
        _hasActivatedGameOverAlready = false;
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (!_hasActivatedGameOverAlready)
        {
            if (AreAllBallsThrown())
            {
                if (CountingScript.Instance().GetPlayerScore() >= 18)
                { 
                    _hasActivatedGameOverAlready = true;
                    GameOverScript.Instance.GameOver(true);
                }
                else
                {
                    _hasActivatedGameOverAlready = true;
                    GameOverScript.Instance.GameOver(false);
                }
                gameObject.SetActive(false);
            }
        }
        
    }

    private bool AreAllBallsThrown()
    {
        if (CountingScript.Instance().GetAmountOfBallsThrown() == 0)
        {
            return true;
        }
        return false;
    }
}
