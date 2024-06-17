/**
 * SpaceShipSpawner.cs
 * Este script se utiliza para generar naves espaciales enemigas a intervalos aleatorios.
 */

using UnityEngine;

public class SpaceShipSpawner : MonoBehaviour
{
    public GameObject[] spaceShipPrefabs;
    public float minSpawnInterval = 1.5f; // Intervalo mínimo de spawn de las naves.
    public float maxSpawnInterval = 3f; // Intervalo máximo de spawn de las naves.
    public AudioSource spawnSound;

    private bool isSpawning = false;

    void Start()
    {
        // No iniciar spawneo aquí, esperar a que el StageManager lo haga.
    }

    public void StartSpawning()
    {
        // Inicia la generación de naves espaciales.
        isSpawning = true;
        SpawnSpaceShipWithRandomInterval();
    }

    public void StopSpawning()
    {
        // Detiene la generación de naves espaciales.
        isSpawning = false;
        CancelInvoke("SpawnSpaceShipWithRandomInterval");
    }

    void SpawnSpaceShipWithRandomInterval()
    {
        // Genera una nave solo si el spawner está activo.
        if (!isSpawning)
        {
            return;
        }

        InstantiateSpaceShip();

        // Llama a la función para spawnear la siguiente nave después de un tiempo aleatorio.
        Invoke("SpawnSpaceShipWithRandomInterval", Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    void InstantiateSpaceShip()
    {
        // Selecciona un prefab de nave aleatorio y la instancia en una posición inicial.
        GameObject randomSpaceShipPrefab = spaceShipPrefabs[Random.Range(0, spaceShipPrefabs.Length)];
        GameObject spaceShip = Instantiate(randomSpaceShipPrefab, transform.position, Quaternion.identity);

        // Reproduce el sonido de spawn si está configurado.
        PlaySpawnSound();
    }

    private void PlaySpawnSound()
    {
        // Reproduce el sonido de spawn si está configurado.
        if (spawnSound != null)
        {
            spawnSound.Play();
        }
    }
}
