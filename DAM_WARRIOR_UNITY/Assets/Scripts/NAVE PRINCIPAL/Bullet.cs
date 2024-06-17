/**
 * Bullet.cs
 * Este script controla el comportamiento de las balas normales en el juego.
 */

using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 1;
    public AudioClip hitSound;
    private AudioSource audioSource;
    private Transform escalador;
    private float maxScaleX = 1f;

    void Start()
    {
        // Destruye la bala después de su vida útil y obtiene la referencia al audio source.
        Destroy(gameObject, lifetime);
        audioSource = GameObject.Find("SFX_SHOOT").GetComponent<AudioSource>();
        escalador = GameObject.Find("Escalador").transform;
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
        else if (collision.gameObject.CompareTag("Boss"))
        {
            DamageBoss(collision.gameObject);
        }
    }

    void DamageRock(GameObject rock)
    {
        // Aplica daño a una roca y reproduce el sonido de impacto.
        RockHealth rockHealth = rock.GetComponent<RockHealth>();

        if (rockHealth != null)
        {
            rockHealth.TakeDamage(damage);
            ScoreManager.Instance.RegisterHit();
            PlayHitSound();
            ScoreManager.Instance.AddScore(10);

            // Incrementa la escala de la barra de poder.
            if (escalador != null)
            {
                Vector3 newScale = escalador.localScale + new Vector3(0.1f, 0f, 0f);
                escalador.localScale = Vector3.Min(newScale, new Vector3(maxScaleX, 1f, 1f));

                if (escalador.localScale.x >= maxScaleX)
                {
                    GameManager.fullPower = true;
                }
            }
        }

        Destroy(gameObject);
    }

    void DamageEnemy(GameObject enemy)
    {
        // Aplica daño a un enemigo y reproduce el sonido de impacto.
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            Debug.Log("Bullet hit an enemy and applied damage.");
            enemyHealth.TakeDamage(damage);
            ScoreManager.Instance.RegisterHit();
            PlayHitSound();
            ScoreManager.Instance.AddScore(20);
        }

        // Incrementa la escala de la barra de poder.
        if (escalador != null)
        {
            Vector3 newScale = escalador.localScale + new Vector3(0.1f, 0f, 0f);
            escalador.localScale = Vector3.Min(newScale, new Vector3(maxScaleX, 1f, 1f));

            if (escalador.localScale.x >= maxScaleX)
            {
                GameManager.fullPower = true;
            }
        }

        Destroy(gameObject);
    }

    void DamageBoss(GameObject boss)
    {
        // Aplica daño a un jefe y reproduce el sonido de impacto.
        BossHealth bossHealth = boss.GetComponent<BossHealth>();

        if (bossHealth != null)
        {
            bossHealth.TakeDamage(damage);
            ScoreManager.Instance.RegisterHit();
            PlayHitSound();
            ScoreManager.Instance.AddScore(50);
        }

        // Incrementa la escala de la barra de poder.
        if (escalador != null)
        {
            Vector3 newScale = escalador.localScale + new Vector3(0.1f, 0f, 0f);
            escalador.localScale = Vector3.Min(newScale, new Vector3(maxScaleX, 1f, 1f));

            if (escalador.localScale.x >= maxScaleX)
            {
                GameManager.fullPower = true;
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
