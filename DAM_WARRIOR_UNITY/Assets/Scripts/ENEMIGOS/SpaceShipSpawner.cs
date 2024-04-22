using UnityEngine;

public class SpaceShipSpawner : MonoBehaviour
{
    public GameObject[] spaceShipPrefabs;
    public float minSpawnInterval = 1.5f; // Intervalo mínimo de spawn de las naves
    public float maxSpawnInterval = 3f; // Intervalo máximo de spawn de las naves
    public AudioSource spawnSound;

    private bool isSpawning = false;

    void Start()
    {
        // Comienza la generación de naves solo cuando el spawner esté activo
        if (isSpawning)
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
        SpawnSpaceShipWithRandomInterval();
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    void SpawnSpaceShipWithRandomInterval()
    {
        if (!isSpawning)
        {
            return;
        }

        // Genera una nave solo si el spawner está activo
        InstantiateSpaceShip();

        // Llama a la función para spawnear la siguiente nave después de un tiempo aleatorio
        Invoke("SpawnSpaceShipWithRandomInterval", Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    void InstantiateSpaceShip()
    {
        // Selecciona un prefab de nave aleatorio
        GameObject randomSpaceShipPrefab = spaceShipPrefabs[Random.Range(0, spaceShipPrefabs.Length)];

        // Genera la nave en una posición inicial
        GameObject spaceShip = Instantiate(randomSpaceShipPrefab, transform.position, Quaternion.identity);

        // Reproduce el sonido de spawn si está configurado
        PlaySpawnSound();
    }

    private void PlaySpawnSound()
    {
        if (spawnSound != null)
        {
            spawnSound.Play();
        }
    }
}
