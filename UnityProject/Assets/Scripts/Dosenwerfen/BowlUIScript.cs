using UnityEngine;

public class BowlScript1 : MonoBehaviour
{
    void Update()
    {
        if (AreAllChildrenInactive())
        {
            gameObject.SetActive(false);
            if (CanScript.Counter >= 18)
            {
                GameOverScript.Instance.GameOver(true);
            }
            else
            {
                GameOverScript.Instance.GameOver(false);
            }
        }
    }

    private bool AreAllChildrenInactive()
    {
        if (BallScript.AmountOfBallsThrown == 0)
        {
            return true;
            
        }
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf & child.gameObject.tag.Equals("Ball"))
            { 
                return false;
            }
        }
        return false;
    }
}
