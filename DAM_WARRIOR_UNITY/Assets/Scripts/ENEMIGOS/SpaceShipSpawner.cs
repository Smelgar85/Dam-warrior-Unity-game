using UnityEngine;

public class SpaceShipSpawner : MonoBehaviour
{
    public GameObject[] spaceShipPrefabs;
    public float minSpawnInterval = 1.5f; // Intervalo m�nimo de spawn de las naves
    public float maxSpawnInterval = 3f; // Intervalo m�ximo de spawn de las naves
    public AudioSource spawnSound;

    private bool isSpawning = false;

    void Start()
    {
        // No iniciar spawneo aqu�, esperar a que el StageManager lo haga
        // StartSpawning();
    }

    public void StartSpawning()
    {
        isSpawning = true;
        SpawnSpaceShipWithRandomInterval();
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("SpawnSpaceShipWithRandomInterval");
    }

    void SpawnSpaceShipWithRandomInterval()
    {
        if (!isSpawning)
        {
            return;
        }

        // Genera una nave solo si el spawner est� activo
        InstantiateSpaceShip();

        // Llama a la funci�n para spawnear la siguiente nave despu�s de un tiempo aleatorio
        Invoke("SpawnSpaceShipWithRandomInterval", Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    void InstantiateSpaceShip()
    {
        // Selecciona un prefab de nave aleatorio
        GameObject randomSpaceShipPrefab = spaceShipPrefabs[Random.Range(0, spaceShipPrefabs.Length)];

        // Genera la nave en una posici�n inicial
        GameObject spaceShip = Instantiate(randomSpaceShipPrefab, transform.position, Quaternion.identity);

        // Reproduce el sonido de spawn si est� configurado
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
