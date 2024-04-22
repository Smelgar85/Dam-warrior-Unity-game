using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemigo : MonoBehaviour
{

    public float lifetime = 5f;
    public int damage = 1;
    public AudioClip hitSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            VidaNave vidanave = collision.gameObject.GetComponent<VidaNave>();

            if (vidanave != null)
            {
                vidanave.PerderSalud(damage);
                PlayHitSound();

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
}