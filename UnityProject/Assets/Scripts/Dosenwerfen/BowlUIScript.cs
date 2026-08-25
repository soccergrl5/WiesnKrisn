using UnityEngine;

public class BowlScript1 : MonoBehaviour
{
    void Update()
    {
        if (AreAllChildrenInactive())
        {
            GameOverScript.Instance.GameOver(false);
        }
    }

    private bool AreAllChildrenInactive()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                return false;
            }
        }

        if (BallScript.AmountOfBallsThrown == 0)
        {
            return true;
            
        }
        return false;
    }
}
