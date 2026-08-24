using UnityEngine;

public class BowlScript : MonoBehaviour
{
    public GameObject sampleBall;
    private static bool _isBallAlreadyThere = false;

    public void AddObject()
    {
        if (!_isBallAlreadyThere)
        {
            Instantiate(sampleBall, Vector3.zero, Quaternion.identity);
            gameObject.SetActive(false);
            _isBallAlreadyThere = true;
        }
    }

    public static void SetIsBallAlreadyThere(bool isBallAlreadyThere)
    {
        _isBallAlreadyThere = isBallAlreadyThere;
    }
}
