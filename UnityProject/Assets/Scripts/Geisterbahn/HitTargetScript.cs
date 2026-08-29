using UnityEngine;

public class HitTargetScript : MonoBehaviour
{
    public TargetEnum targetType;
    private bool isHit = false;
    

    public void OnMouseDown()
    {
        if (!isHit)
        {
            isHit = true;
            StoringPointsScript.AddToPlayerPoints(AmountOfPointsForGhostType(targetType)); 
            PlayerScoreScript.Instance().SetScore(StoringPointsScript.GetPlayerPoints());
            Destroy(this.gameObject);
        }
    }
    
    private static int AmountOfPointsForGhostType(TargetEnum type)
    {
        switch (type)
        {
            case TargetEnum.BigGhost: return 20;
            case TargetEnum.SmallGhost: return 50;
            case TargetEnum.MiddleGhost: return 30;
            default: return 0;
        }
    }
}
