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
    private bool isDead = false; // Nueva variable de control
    private GameController gameController;

    void Start()
    {
        explosionAudioSource = GameObject.Find("SFX_DEATH_ENEMY").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();

        if (campoDeFuerzaEnemigo != null)
        {
            SetFieldAlpha(0f);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; // Evitar aplicar daño si ya está muerto
        health -= damageAmount;
        ScoreManager.Instance.RegisterDamageDealt(damageAmount);
        Debug.Log("Enemy took damage. Current health: " + health);
        if (gameController != null)
        {
            gameController.RegistrarDanoCausado(damageAmount);
        }

        StartCoroutine(FadeFieldIn(duracionFadeInEn));

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; // Evitar ejecutar Die() más de una vez
        isDead = true;

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        if (explosionAudioSource != null)
        {
            float originalVolume = explosionAudioSource.volume;
            explosionAudioSource.volume = 0.5f;
            explosionAudioSource.PlayOneShot(dieSound);
            explosionAudioSource.volume = originalVolume;
        }

        ScoreManager.Instance.AddScore(50);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BulletEnemy"))
        {
            return;
        }

        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Rock"))
        {
            Debug.Log("Enemigo colisionó con: " + collision.gameObject.name);
            TakeDamage(1);

            if (collision.gameObject.CompareTag("Bullet"))
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void SetFieldAlpha(float alpha)
    {
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
