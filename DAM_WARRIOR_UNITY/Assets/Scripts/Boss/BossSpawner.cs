using System;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab; // Prefab del jefe
    public Transform bossSpawnPoint; // Punto de aparición del jefe
    private bool isSpawning = false;

    public void StartSpawning()
    {
        isSpawning = true;
        // Instancia el jefe en el punto de aparición
        Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation, transform);
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
