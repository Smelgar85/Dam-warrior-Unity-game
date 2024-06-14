using UnityEngine;

public class BulletEnemigo : MonoBehaviour
{
    public float lifetime = 5f;
    public AudioClip hitSound;
    private AudioSource audioSource;
    private GameController gameController; // Referencia al controlador del juego

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>(); // Buscar el controlador del juego en la escena
        }

        if (gameController != null)
        {
            gameController.RegistrarDisparo(false); // Registrar disparo (en este caso, enemigo disparando)
        }

        Invoke("ReturnToPool", lifetime);
    }

    void ReturnToPool()
    {
        BulletPool.Instance.ReturnBullet(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"BulletEnemigo colisionó con: {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Player"))
        {
            VidaNave vidanave = collision.gameObject.GetComponent<VidaNave>();
            if (vidanave != null)
            {
                Debug.Log("Disparo enemigo ha colisionado con la nave");
                vidanave.AplicarDanio(1); // Notificar a VidaNave sobre la colisión y aplicar daño
                PlayHitSound();
            }

            BulletPool.Instance.ReturnBullet(gameObject);
        }
        else
        {
            Debug.Log("Colisión ignorada porque no es un jugador");
        }
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
