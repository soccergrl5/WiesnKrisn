using UnityEngine;
using UnityEngine.Serialization;

public class StartGeisterbahnScript : MonoBehaviour
{
    [SerializeField] private GameObject duringGameUI;
    [SerializeField] private GameObject ghostSpawner;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWonPanel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
            duringGameUI.SetActive(true);
            ghostSpawner.SetActive(true);
            ghostSpawner.GetComponent<GhostSpawnerScript>().StartSpawning();
        }
    }
}
