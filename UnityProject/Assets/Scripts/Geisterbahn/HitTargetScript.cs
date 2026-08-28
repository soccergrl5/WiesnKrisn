using UnityEngine;

public class HitTargetScript : MonoBehaviour
{
    public TargetEnum targetType;
    private bool isHit = false;
    

    public void OnMouseDown()
    {
        if (!isHit)
        {
            print("hihi");
            isHit = true;
            StoringPointsScript.AddToPlayerPoints(AmountOfPointsForGhostType(targetType)); 
            PlayerScoreScript.SetScore(StoringPointsScript.GetPlayerPoints());
        }
        
    }
    
    private static int AmountOfPointsForGhostType(TargetEnum type)
    {
        switch (type)
        {
            case TargetEnum.BigGhost: return 10;
            case TargetEnum.SmallGhost: return 30;
            case TargetEnum.MiddleGhost: return 20;
            default: return 0;
        }
    }
}
