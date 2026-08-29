using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class StoringPointsScript : MonoBehaviour
{
    private static StoringPointsScript _instance;
    private int _playerPoints = 0;
    
    public void Start(){
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public static StoringPointsScript Instance()
    {
        return _instance;
    }

    public int GetPlayerPoints()
    {
        return _playerPoints;
    }

    public void AddToPlayerPoints(int value)
    {
        _playerPoints += value;
    }
}
