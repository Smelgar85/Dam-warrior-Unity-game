using UnityEngine;

public class SpecialBullet : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 5;
    public AudioClip hitSound;
    private AudioSource audioSource;

    void Start()
    {
        Destroy(gameObject, lifetime);
        audioSource = GameObject.Find("SFX_SHOOT2").GetComponent<AudioSource>();
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
            Debug.Log("Special bullet hit a rock and applied damage.");
            rockHealth.TakeDamage(damage);
            PlayHitSound();
            ScoreManager.Instance.AddScore(10);
        }

        Destroy(gameObject);
    }

    void DamageEnemy(GameObject enemy)
    {
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
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
