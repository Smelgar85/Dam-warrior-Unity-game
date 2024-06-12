using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform[] spawnPoints;
    private bool isSpawning = false;

    void Start()
    {
        // Usa isSpawning aquí si es necesario
        if (isSpawning)
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
        SpawnBoss();
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private void SpawnBoss()
    {
        if (spawnPoints.Length == 0 || bossPrefab == null) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
