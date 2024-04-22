using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 1;
    public AudioClip hitSound;
    public AudioClip fullRecharge;
    private AudioSource audioSource;
    private Transform escalador; // Hace referencia al gameobject Escalador, que controla el progreso de la barra de poder
    private float maxScaleX = 1f; // Valor máximo de la escala en el eje X

    void Start()
    {
        Destroy(gameObject, lifetime);
        audioSource = GameObject.Find("SFX_SHOOT").GetComponent<AudioSource>(); // Obtener el AudioSource del GameObject "SFX"
        escalador = GameObject.Find("Escalador").transform; // Obtener la referencia al GameObject "Escalador"
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
        RockHealth rockHealth = rock.GetComponent<RockHealth>();

        if (rockHealth != null)
        {
            rockHealth.TakeDamage(damage);
            PlayHitSound();

            // Incrementar la escala de la barra de poder
            if (escalador != null)
            {
                Vector3 newScale = escalador.localScale + new Vector3(0.1f, 0f, 0f); // Incrementar gradualmente la escala
                escalador.localScale = Vector3.Min(newScale, new Vector3(maxScaleX, 1f, 1f)); // Limitar la escala al máximo

                // Verificar si la barra de poder está completamente llena
                if (escalador.localScale.x >= maxScaleX)
                {
                    //audioSource.PlayOneShot(fullRecharge);
                    GameManager.fullPower = true; // Establecer fullPower a true solo si la barra está completamente llena
                }
            }
        }

        Destroy(gameObject);
    }

    void DamageEnemy(GameObject enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            PlayHitSound();
        }
        if (escalador != null)
        {
            Vector3 newScale = escalador.localScale + new Vector3(0.1f, 0f, 0f); // Incrementar gradualmente la escala
            escalador.localScale = Vector3.Min(newScale, new Vector3(maxScaleX, 1f, 1f)); // Limitar la escala al máximo

            // Verificar si la barra de poder está completamente llena
            if (escalador.localScale.x >= maxScaleX)
            {
                //audioSource.PlayOneShot(fullRecharge);
                GameManager.fullPower = true; // Establecer fullPower a true solo si la barra está completamente llena
            }
        }

        Destroy(gameObject);
    }

    void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
