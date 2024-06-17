/**
 * BulletBoss.cs
 * Este script controla el comportamiento de las balas disparadas por el jefe.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 1;
    public AudioClip hitSound;
    public GameObject explosionPrefab; // Prefab de la explosión.
    private AudioSource audioSource;

    void Start()
    {
        // Destruye la bala después de su vida útil.
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Aplica daño a la nave del jugador si colisiona con ella.
        if (collision.gameObject.CompareTag("Player"))
        {
            VidaNave vidanave = collision.gameObject.GetComponent<VidaNave>();

            if (vidanave != null)
            {
                vidanave.PerderSalud(damage);
                PlayHitSound();
            }

            Destroy(gameObject);
            SpawnExplosion(); // Genera la explosión.
        }
    }

    void PlayHitSound()
    {
        // Reproduce el sonido de impacto si está configurado.
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    void SpawnExplosion()
    {
        // Genera la explosión en la posición actual.
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
