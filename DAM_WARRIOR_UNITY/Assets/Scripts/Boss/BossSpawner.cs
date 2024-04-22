using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab; // Prefab del jefe
    public Transform bossSpawnPoint; // Punto de aparición del jefe

    public void StartSpawning()
    {
        // Instancia el jefe en el punto de aparición
        Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation, transform);
    }
}
