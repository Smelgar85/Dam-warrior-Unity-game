using UnityEngine;

public class BulletEnemigo : MonoBehaviour
{
    public float lifetime = 5f;
    public AudioClip hitSound;
    private AudioSource audioSource;
    private GameController gameController;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }

        if (gameController != null)
        {
            gameController.RegistrarDisparo(false);
        }

        Invoke("ReturnToPool", lifetime);
    }

    void ReturnToPool()
    {
        BulletPool.Instance.ReturnEnemyBullet(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"BulletEnemigo colisionó con: {collision.gameObject.name} con tag: {collision.gameObject.tag}");
        if (collision.gameObject.CompareTag("Player"))
        {
            VidaNave vidanave = collision.gameObject.GetComponent<VidaNave>();
            if (vidanave != null)
            {
                Debug.Log("Disparo enemigo ha colisionado con la nave");
                vidanave.AplicarDanio(1);
                PlayHitSound();
            }

            BulletPool.Instance.ReturnEnemyBullet(gameObject);
        }
        /*
            else
        {
            Debug.Log("Colisión ignorada porque no es un jugador");
        }
        */
    }

    private void PlayHitSound()
    {
        Debug.Log("Reproduciendo sonido de impacto");
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
