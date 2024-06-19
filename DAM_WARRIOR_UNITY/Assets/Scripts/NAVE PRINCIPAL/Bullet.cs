// Este script gestiona el comportamiento de una bala y el daño que inflige a diferentes tipos de enemigos, además de la puntuación.

using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 1;
    public AudioClip hitSound;
    private AudioSource audioSource;
    private Transform escalador;
    private float maxScaleX = 1f;
    private ScoreManager scoreManager; // Referencia al ScoreManager

    // Método llamado al inicio del juego.
    void Start()
    {
        // Destruye la bala después de su vida útil y obtiene la referencia al audio source.
        Destroy(gameObject, lifetime);
        audioSource = GameObject.Find("SFX_SHOOT").GetComponent<AudioSource>();
        escalador = GameObject.Find("Escalador").transform;

        // Busca dinámicamente una instancia de ScoreManager en la escena
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("No se encontró ScoreManager en la escena. Asegúrate de que esté presente.");
        }
    }

    // Método llamado cuando la bala colisiona con otro objeto.
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

    // Aplica daño a una roca.
    void DamageRock(GameObject rock)
    {
        RockHealth rockHealth = rock.GetComponent<RockHealth>();

        if (rockHealth != null)
        {
            rockHealth.TakeDamage(damage);
            PlayHitSound();
            if (scoreManager != null)
            {
                scoreManager.RegisterHit();
                scoreManager.AddScore(10);
                scoreManager.RegisterDamageDealt(damage);
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
        }

        Destroy(gameObject);
    }

    // Aplica daño a un enemigo.
    void DamageEnemy(GameObject enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            PlayHitSound();
            if (scoreManager != null)
            {
                scoreManager.RegisterHit();
                scoreManager.AddScore(20);
                scoreManager.RegisterDamageDealt(damage);
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
        }

        Destroy(gameObject);
    }

    // Aplica daño a un jefe.
    void DamageBoss(GameObject boss)
    {
        BossHealth bossHealth = boss.GetComponent<BossHealth>();

        if (bossHealth != null)
        {
            bossHealth.TakeDamage(damage);
            PlayHitSound();
            if (scoreManager != null)
            {
                scoreManager.RegisterHit();
                scoreManager.AddScore(50);
                scoreManager.RegisterDamageDealt(damage);
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
        }

        Destroy(gameObject);
    }

    // Reproduce el sonido de impacto si está configurado.
    void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
