using UnityEngine;

public class StoringPointsScript : MonoBehaviour
{
    private static int _playerPoints;

    public static int GetPlayerPoints()
    {
        return _playerPoints;
    }

    public static void AddToPlayerPoints(int value)
    {
        _playerPoints += value;
    }
}
