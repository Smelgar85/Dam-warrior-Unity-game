using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform[] spawnPoints;
    private bool isSpawning = false;

    void Start()
    {
        // No iniciar spawneo aquí, esperar a que el StageManager lo haga
    }

    public void StartSpawning()
    {
        if (!isSpawning) // Verificamos si no está ya spawneando
        {
            isSpawning = true;
            SpawnBoss();
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private void SpawnBoss()
    {
        if (spawnPoints.Length == 0 || bossPrefab == null || !isSpawning) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
