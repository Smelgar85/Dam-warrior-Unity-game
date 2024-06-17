/**
 * BossSpawner.cs
 * Este script maneja la generación del jefe en el juego.
 */

using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    private GameObject currentBoss;

    public void StartSpawning()
    {
        // Genera el jefe si no hay uno actualmente.
        if (currentBoss == null)
        {
            SpawnBoss();
        }
    }

    public void StopSpawning()
    {
        // Aquí puedes detener cualquier lógica de spawneo continuo si la tienes.
    }

    void SpawnBoss()
    {
        // Genera el jefe en la posición del spawner.
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
