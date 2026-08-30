using UnityEngine;

public class DropBoxScript : MonoBehaviour
{
    private GameOverScript _gameOverScript;
    void Start()
    {
        _gameOverScript = GameOverScript.Instance;   
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlushieScript>() != null)
        {
            var otherPlushie = other.GetComponent<PlushieScript>().GetPlushieType();
            var shouleBePlushie = PlushieOrganizerScript.Instance().GetPlushieTypeToGet();

            if (otherPlushie.Equals(shouleBePlushie))
            {
                SoundScriptGreifautomat.Instance().PlayRightPlushieSound();
                _gameOverScript.GameOver(true);
                print("Game Won");
            }
            else
            {
                _gameOverScript.GameOver(false);
                print("Game Over");
            }
        }
    }
}
