/**
 * BigBullet.cs
 * Este script controla el comportamiento de las balas grandes en el juego.
 */

using UnityEngine;

public class BigBullet : MonoBehaviour
{
    public float lifetime = 15f;
    public int damage = 5;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        // Aplica daño según el tipo de objeto con el que colisiona.
        if (other.gameObject.CompareTag("Rock"))
        {
            DamageRock(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            DamageEnemy(other.gameObject);
        }
    }

    void DamageRock(GameObject rock)
    {
        // Aplica daño a una roca y reproduce el sonido de impacto.
        RockHealth rockHealth = rock.GetComponent<RockHealth>();

        if (rockHealth != null)
        {
            rockHealth.TakeDamage(damage);
            PlayHitSound();
            ScoreManager.Instance.AddScore(15);
            IncrementPowerBarScale();
        }
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
            ScoreManager.Instance.AddScore(25);
        }
        else
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                Debug.Log("Special bullet hit an enemy and applied damage.");
                enemyHealth.TakeDamage(damage);
                PlayHitSound();
                ScoreManager.Instance.AddScore(25);
            }
        }

        IncrementPowerBarScale();
    }

    void IncrementPowerBarScale()
    {
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

    void PlayHitSound()
    {
        // Reproduce el sonido de impacto si está configurado.
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
