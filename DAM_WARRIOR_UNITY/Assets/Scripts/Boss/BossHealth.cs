using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Asegúrate de añadir esta línea

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
        if (isDead) return;
        isDead = true;

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        if (explosionAudioSource != null && dieSound != null)
        {
            explosionAudioSource.PlayOneShot(dieSound);
        }

        ScoreManager.Instance.AddScore(1000); // Asigna una puntuación alta por derrotar al jefe

        StartCoroutine(ChangeToSummaryScene()); // Inicia la coroutine para cambiar de escena

        Destroy(gameObject);
    }

    IEnumerator ChangeToSummaryScene()
    {
        yield return new WaitForSeconds(5f); // Espera 5 segundos
        StageManager.Instance.FlyingFortressDestroyed(); // Informa al StageManager
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}
