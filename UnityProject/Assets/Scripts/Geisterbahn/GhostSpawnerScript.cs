using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject smallGhostPrefab;
    [SerializeField] private GameObject middleGhostPrefab;
    [SerializeField] private GameObject bigGhostPrefab;
    private List<GameObject> _ghostList = new List<GameObject>();
    private int amountOfGhostsSpawned = 0;
    

    public void Start()
    {
        InvokeRepeating(nameof(SpawnGhost), 0, 1);
        InvokeRepeating(nameof(MoveGhosts), 0, 0.1f);
    }

    private void SpawnGhost()
    {
        if (amountOfGhostsSpawned <= 10)
        {
            InstantiateGhost(bigGhostPrefab);
        } else if (amountOfGhostsSpawned <= 20)
        {
            InstantiateGhost(middleGhostPrefab);
        } else if (amountOfGhostsSpawned <= 30)
        {
            InstantiateGhost(smallGhostPrefab);
        }
        amountOfGhostsSpawned++;
    }

    private void InstantiateGhost(GameObject ghostPrefab)
    {
        GameObject ghost = Instantiate(ghostPrefab, transform);
        ghost.transform.position = Random.insideUnitSphere * 10;
        ghost.transform.position = new Vector3(ghost.transform.position.x, this.transform.position.y, this.transform.position.z);
        _ghostList.Add(ghost);
        
    }

    private void MoveGhosts()
    {
        foreach (GameObject ghost in _ghostList)
        {
            if (ghost != null)
            {
                ghost.transform.position += new Vector3(0, 0, -2);
            }
        }
    }
}
