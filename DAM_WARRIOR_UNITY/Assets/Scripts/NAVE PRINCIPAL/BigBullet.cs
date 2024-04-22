using UnityEngine;

public class BigBullet : MonoBehaviour
{
    public float lifetime = 15f;
    public int damage = 5;
    public AudioClip hitSound;
    private AudioSource audioSource;
    private Transform escalador; // Referencia al objeto de la barra de poder
    private float maxScaleX = 1f; // Valor máximo de la escala en el eje X

    void Start()
    {
        Destroy(gameObject, lifetime);
        audioSource = GameObject.Find("SFX_SHOOT").GetComponent<AudioSource>(); // Obtener el AudioSource del GameObject "SFX"
        escalador = GameObject.Find("Escalador").transform; // Obtener la referencia al GameObject "Escalador"
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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
        RockHealth rockHealth = rock.GetComponent<RockHealth>();

        if (rockHealth != null)
        {
            rockHealth.TakeDamage(damage);
            PlayHitSound();
            IncrementPowerBarScale();
        }
    }

    void DamageEnemy(GameObject enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            PlayHitSound();
            IncrementPowerBarScale();
        }
    }

    void IncrementPowerBarScale()
    {
        if (escalador != null)
        {
            Vector3 newScale = escalador.localScale + new Vector3(0.1f, 0f, 0f); // Incrementar gradualmente la escala
            escalador.localScale = Vector3.Min(newScale, new Vector3(maxScaleX, 1f, 1f)); // Limitar la escala al máximo

            // Verificar si la barra de poder está completamente llena
            if (escalador.localScale.x >= maxScaleX)
            {
                GameManager.fullPower = true; // Establecer fullPower a true solo si la barra está completamente llena
            }
        }
    }

    void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
