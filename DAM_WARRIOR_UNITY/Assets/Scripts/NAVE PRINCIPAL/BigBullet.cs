// Este script gestiona el comportamiento de una bala grande, incluyendo su vida útil, daño, y las interacciones con diferentes tipos de objetos.

using UnityEngine;

public class BigBullet : MonoBehaviour
{
    public float lifetime = 15f;
    public int damage = 5;
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
    void OnTriggerEnter2D(Collider2D collision)
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

    // Aplica daño a una roca y reproduce el sonido de impacto.
    void DamageRock(GameObject rock)
    {
        RockHealth rockHealth = rock.GetComponent<RockHealth>();

        if (rockHealth != null)
        {
            rockHealth.TakeDamage(damage);
            if (scoreManager != null)
            {
                scoreManager.RegisterHit();
                scoreManager.AddScore(15);
                scoreManager.RegisterDamageDealt(damage);
            }
            PlayHitSound();

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
    }

    // Aplica daño a un enemigo y reproduce el sonido de impacto.
    void DamageEnemy(GameObject enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            Debug.Log("Special bullet hit an enemy and applied damage.");
            enemyHealth.TakeDamage(damage);
            if (scoreManager != null)
            {
                scoreManager.RegisterHit();
                scoreManager.AddScore(25);
                scoreManager.RegisterDamageDealt(damage);
            }
            PlayHitSound();

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
    }

    // Aplica daño a un jefe y reproduce el sonido de impacto.
    void DamageBoss(GameObject boss)
    {
        BossHealth bossHealth = boss.GetComponent<BossHealth>();

        if (bossHealth != null)
        {
            bossHealth.TakeDamage(damage);
            if (scoreManager != null)
            {
                scoreManager.RegisterHit();
                scoreManager.AddScore(50);
                scoreManager.RegisterDamageDealt(damage);
            }
            PlayHitSound();

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
