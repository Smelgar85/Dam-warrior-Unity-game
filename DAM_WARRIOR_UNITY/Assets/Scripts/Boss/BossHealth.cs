/**
 * BossHealth.cs
 * Este script maneja multitud básicamente la gestión de energía del boss y su muerte.
 */
using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffectPrefab;
    public AudioClip dieSound;
    private AudioSource explosionAudioSource;
    private bool isDead = false;
    private StageManager stageManager; 
    private ScoreManager scoreManager; 

    void Start()
    {
        // Inicializa referencias y verifica componentes.
        explosionAudioSource = GameObject.Find("SFX_DEATH_ENEMY").GetComponent<AudioSource>();

        if (explosionAudioSource == null)
        {
            Debug.LogWarning("SFX_DEATH_ENEMY no encontrado en la escena.");
        }

        // Asigna la referencia al StageManager automáticamente
        stageManager = GameObject.Find("MANAGER_DE_FASE").GetComponent<StageManager>();

        if (stageManager == null)
        {
            Debug.LogError("StageManager no encontrado en la escena.");
        }

        // Busca y asigna el ScoreManager
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager no encontrado en la escena.");
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // Aplica daño al jefe y llama a su muerte si la salud llega a cero.
        if (isDead) return;

        health -= damageAmount;
        Debug.Log("Boss took damage. Current health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Maneja la muerte del jefe.
        if (isDead) return;
        isDead = true;

        // Genera un efecto de muerte y reproduce un sonido de explosión.
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        if (explosionAudioSource != null && dieSound != null)
        {
            explosionAudioSource.PlayOneShot(dieSound);
        }

        // Añade puntuación al ScoreManager
        if (scoreManager != null)
        {
            scoreManager.AddScore(1000);
        }
        else
        {
            Debug.LogError("ScoreManager no asignado en BossHealth.");
        }

        // Llamar al método FlyingFortressDestroyed del StageManager.
        if (stageManager != null)
        {
            stageManager.FlyingFortressDestroyed();
        }
        else
        {
            Debug.LogError("StageManager no asignado en BossHealth.");
        }

        Destroy(gameObject);
    }
}
