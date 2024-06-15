using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    private GameObject currentBoss;

    public void StartSpawning()
    {
        if (currentBoss == null)
        {
            SpawnBoss();
        }
    }

    public void StopSpawning()
    {
        // Aquí puedes detener cualquier lógica de spawneo continuo si la tienes
    }

    void SpawnBoss()
    {
        if (bossPrefab != null)
        {
            currentBoss = Instantiate(bossPrefab, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogWarning("BossPrefab no asignado en BossSpawner.");
        }
    }
}
