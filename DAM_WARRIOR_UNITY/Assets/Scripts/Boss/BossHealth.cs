/**
 * BossHealth.cs
 * Este script maneja la salud del jefe y su destrucción.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffectPrefab;
    public AudioClip dieSound;
    private AudioSource explosionAudioSource;
    private GameController gameController;
    private bool isDead = false;

    void Start()
    {
        // Inicializa referencias y verifica componentes.
        explosionAudioSource = GameObject.Find("SFX_DEATH_ENEMY").GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();

        if (explosionAudioSource == null)
        {
            Debug.LogWarning("SFX_DEATH_ENEMY no encontrado en la escena.");
        }

        if (gameController == null)
        {
            Debug.LogWarning("GameController no encontrado en la escena.");
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // Aplica daño al jefe y maneja su muerte si la salud llega a cero.
        if (isDead) return;

        health -= damageAmount;
        ScoreManager.Instance.RegisterDamageDealt(damageAmount);
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

        ScoreManager.Instance.AddScore(1000); // Asigna una puntuación alta por derrotar al jefe.

        StartCoroutine(ChangeToSummaryScene()); // Inicia la coroutine para cambiar de escena.

        Destroy(gameObject);
    }

    IEnumerator ChangeToSummaryScene()
    {
        // Espera unos segundos y luego notifica al StageManager para cambiar de escena.
        yield return new WaitForSeconds(5f); // Espera 5 segundos.
        StageManager.Instance.FlyingFortressDestroyed(); // Informa al StageManager.
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Aplica daño al jefe si colisiona con una bala.
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}
