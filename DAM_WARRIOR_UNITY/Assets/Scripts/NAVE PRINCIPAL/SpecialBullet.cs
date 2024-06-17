/**
 * SpecialBullet.cs
 * Este script controla el comportamiento de las balas especiales en el juego.
 */

using UnityEngine;

public class SpecialBullet : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 5;
    public AudioClip hitSound;
    private AudioSource audioSource;

    void Start()
    {
        // Destruye la bala después de su vida útil y obtiene la referencia al audio source.
        Destroy(gameObject, lifetime);
        audioSource = GameObject.Find("SFX_SHOOT2").GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Aplica daño según el tipo de objeto con el que colisiona.
        if (collision.gameObject.CompareTag("Rock"))
        {
            DamageRock(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageEnemy(collision.gameObject);
        }
    }

    void DamageRock(GameObject rock)
    {
        // Aplica daño a una roca y reproduce el sonido de impacto.
        RockHealth rockHealth = rock.GetComponent<RockHealth>();

        if (rockHealth != null)
        {
            Debug.Log("Special bullet hit a rock and applied damage.");
            rockHealth.TakeDamage(damage);
            PlayHitSound();
            ScoreManager.Instance.AddScore(10);
        }

        Destroy(gameObject);
    }

    void DamageEnemy(GameObject enemy)
    {
        // Aplica daño a un enemigo o jefe y reproduce el sonido de impacto.
        BossHealth bossHealth = enemy.GetComponent<BossHealth>();

        if (bossHealth != null)
        {
            Debug.Log("Special bullet hit the boss and applied damage.");
            bossHealth.TakeDamage(damage);
            PlayHitSound();
            ScoreManager.Instance.AddScore(20);
        }
        else
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                Debug.Log("Special bullet hit an enemy and applied damage.");
                enemyHealth.TakeDamage(damage);
                PlayHitSound();
                ScoreManager.Instance.AddScore(20);
            }
        }

        Destroy(gameObject);
    }

    void PlayHitSound()
    {
        // Reproduce el sonido de impacto si está configurado.
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
