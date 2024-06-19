// Este script gestiona la vida y las colisiones de una nave enemiga, incluyendo sus efectos visuales y sonoros.
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffectPrefab;
    public AudioClip dieSound;
    public GameObject campoDeFuerzaEnemigo;
    public AudioClip campoDeFuerzaEnemigoSound;
    private AudioSource explosionAudioSource;
    private AudioSource audioSource;
    public float duracionFadeInEn = 1f;
    public float duracionFadeOutEn = 1f;
    public float esperaFadeOutEn = 1f;
    private bool isDead = false;

    private ScoreManager scoreManager;

    void Start()
    {
        // Inicializa referencias y verifica componentes.
        explosionAudioSource = GameObject.Find("SFX_DEATH_ENEMY").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        if (campoDeFuerzaEnemigo != null)
        {
            SetFieldAlpha(0f);
        }

        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager no encontrado en la escena.");
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // Aplica daño al enemigo y maneja su muerte si la salud llega a cero.
        if (isDead) return; // Evitar aplicar daño si ya está muerto.
        health -= damageAmount;
        Debug.Log("Enemy took damage. Current health: " + health);

        StartCoroutine(FadeFieldIn(duracionFadeInEn));

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Maneja la muerte del enemigo.
        if (isDead) return; // Evitar ejecutar Die() más de una vez.
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

        if (scoreManager != null)
        {
            scoreManager.AddScore(50);
        }
        else
        {
            Debug.LogWarning("ScoreManager no encontrado. No se pudo añadir la puntuación.");
        }

        Destroy(gameObject);
    }

    private void SetFieldAlpha(float alpha)
    {
        // Establece la transparencia del campo de fuerza del enemigo.
        if (campoDeFuerzaEnemigo != null)
        {
            Renderer renderer = campoDeFuerzaEnemigo.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color color = renderer.material.color;
                color.a = alpha;
                renderer.material.color = color;
                Debug.Log("Campo de fuerza actualizado con alpha: " + alpha);
            }
        }
    }

    IEnumerator FadeFieldIn(float duration)
    {
        // Realiza un fade in en el campo de fuerza.
        Debug.Log("Iniciando FadeFieldIn");
        if (campoDeFuerzaEnemigoSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(campoDeFuerzaEnemigoSound);
        }

        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, counter / duration);
            SetFieldAlpha(alpha);
            yield return null;
        }

        yield return new WaitForSeconds(esperaFadeOutEn);
        StartCoroutine(FadeFieldOut(duracionFadeOutEn));
    }

    IEnumerator FadeFieldOut(float duration)
    {
        // Realiza un fade out en el campo de fuerza.
        Debug.Log("Iniciando FadeFieldOut");
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, counter / duration);
            SetFieldAlpha(alpha);
            yield return null;
        }
    }
}
