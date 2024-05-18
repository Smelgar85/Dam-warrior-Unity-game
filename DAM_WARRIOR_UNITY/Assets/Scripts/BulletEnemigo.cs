using UnityEngine;

public class BulletEnemigo : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 1;
    public AudioClip hitSound;
    private AudioSource audioSource;

    void OnEnable()
    {
        GameStatistics.Instance.RegisterShotFired();
        Invoke("ReturnToPool", lifetime);
    }

    void ReturnToPool()
    {
        BulletPool.Instance.ReturnBullet(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameStatistics.Instance.RegisterHit();
            VidaNave vidanave = collision.gameObject.GetComponent<VidaNave>();
            if (vidanave != null)
            {
                vidanave.PerderSalud(damage);
                PlayHitSound();
            }

            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }

    private void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
