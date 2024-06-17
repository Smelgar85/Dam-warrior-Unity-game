/**
 * BulletEnemigo.cs
 * Este script controla el comportamiento de las balas enemigas en el juego.
 */

using UnityEngine;

public class BulletEnemigo : MonoBehaviour
{
    public float lifetime = 5f;
    public AudioClip hitSound;
    private AudioSource audioSource;
    private GameController gameController;

    void Awake()
    {
        // Obtiene la referencia al componente AudioSource.
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        // Obtiene la referencia al GameController y registra un disparo.
        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }

        if (gameController != null)
        {
            gameController.RegistrarDisparo(false);
        }

        // Programa el retorno de la bala al pool después de su vida útil.
        Invoke("ReturnToPool", lifetime);
    }

    void ReturnToPool()
    {
        // Devuelve la bala al pool.
        BulletPool.Instance.ReturnEnemyBullet(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Aplica daño a la nave del jugador si colisiona con ella.
        if (collision.gameObject.CompareTag("Player"))
        {
            VidaNave vidanave = collision.gameObject.GetComponent<VidaNave>();
            if (vidanave != null)
            {
                Debug.Log("Disparo enemigo ha colisionado con la nave");
                vidanave.AplicarDanio(1);
                PlayHitSound();
            }

            // Devuelve la bala al pool.
            BulletPool.Instance.ReturnEnemyBullet(gameObject);
        }
    }

    private void PlayHitSound()
    {
        // Reproduce el sonido de impacto si está configurado.
        Debug.Log("Reproduciendo sonido de impacto");
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
