using UnityEngine;

public class BowlScript : MonoBehaviour
{
    private static BowlScript _instance;
    public GameObject sampleBall;
    private bool _isBallAlreadyThere = false;

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        _isBallAlreadyThere = false;
    }

    public static BowlScript Instance()
    {
        return _instance;
    }
    
    public void AddObject()
    {
        if (!_isBallAlreadyThere)
        {
            Instantiate(sampleBall, Vector3.zero, Quaternion.identity);
            gameObject.SetActive(false);
            _isBallAlreadyThere = true;
        }
    }

    public void SetIsBallAlreadyThere(bool isBallAlreadyThere)
    {
        _isBallAlreadyThere = isBallAlreadyThere;
    }
}
