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
        Destroy(gameObject, lifetime);
        audioSource = GameObject.Find("SFX_SHOOT").GetComponent<AudioSource>();
        escalador = GameObject.Find("Escalador").transform;
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
            //Debug.Log("Bullet hit a rock and applied damage.");
            rockHealth.TakeDamage(damage);
            PlayHitSound();
            ScoreManager.Instance.AddScore(10);

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
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            Debug.Log("Bullet hit an enemy and applied damage.");
            enemyHealth.TakeDamage(damage);
            PlayHitSound();
            ScoreManager.Instance.AddScore(20);
        }

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
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
