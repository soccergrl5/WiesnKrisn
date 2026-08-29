using UnityEngine;

public class StartDosenwerfen : MonoBehaviour
{
    [SerializeField] private GameObject uiBowlOfBalls;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
            uiBowlOfBalls.SetActive(true);
        }
    }
    
}
    

