using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject[] rockPrefabs;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;
    public AudioSource spawnSound;
    public float minScale = 0.5f; // Mínimo tamaño de la escala de las rocas
    public float maxScale = 2.0f; // Máximo tamaño de la escala de las rocas

    private bool isSpawning = false;

    void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        isSpawning = true;
        InvokeRepeating("SpawnRock", Random.Range(minSpawnInterval, maxSpawnInterval), Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("SpawnRock");
    }

    void SpawnRock()
    {
        if (!isSpawning)
        {
            return;
        }

        Vector3 spawnPosition = new Vector3(20f, Random.Range(-4f, 4f), 0f);
        InstantiateRock(spawnPosition);
    }

    public void SpawnRocksAtPosition(Vector3 position, int numberOfRocks)
    {
        for (int i = 0; i < numberOfRocks; i++)
        {
            InstantiateRock(position);
        }
    }

    void InstantiateRock(Vector3 position)
    {
        GameObject randomRockPrefab = rockPrefabs[Random.Range(0, rockPrefabs.Length)];
        GameObject rock = Instantiate(randomRockPrefab, position, Quaternion.identity);

        // Aplica una escala aleatoria dentro de los márgenes definidos
        float randomScale = Random.Range(minScale, maxScale);
        rock.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        RockMovement rockMovement = rock.GetComponent<RockMovement>();
        if (rockMovement != null)
        {
            rockMovement.speed = 5f; // Ajusta la velocidad según tus necesidades
        }

        PlaySpawnSound(); // Llama a la función para reproducir el sonido de spawn
    }

    private void PlaySpawnSound()
    {
        if (spawnSound != null)
        {
            spawnSound.Play();
        }
    }
}
