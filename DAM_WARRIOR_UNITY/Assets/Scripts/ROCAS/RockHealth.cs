using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public AudioClip[] breakSounds;
    public GameObject explosionPrefab;
    public GameObject[] rockPrefabs;
    public bool canDivide = true;
    public float explosionForce = 500f;
    public float minScale = 0.5f; // Valor mínimo de la escala
    public float maxScale = 1.5f; // Valor máximo de la escala
    private bool hasEnteredScreen = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnBecameVisible()
    {
        hasEnteredScreen = true;
    }

    void OnBecameInvisible()
    {
        if (hasEnteredScreen)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            AudioSource audioSource = GameObject.Find("SFX_EXPLOSION").GetComponent<AudioSource>();
            if (breakSounds != null && breakSounds.Length > 0 && audioSource != null)
            {
                int randomIndex = Random.Range(0, breakSounds.Length);
                AudioClip selectedBreakSound = breakSounds[randomIndex];
                audioSource.PlayOneShot(selectedBreakSound);
            }

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            if (canDivide && rockPrefabs.Length > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    int index = Random.Range(0, rockPrefabs.Length);
                    GameObject newRock = Instantiate(rockPrefabs[index], transform.position, Quaternion.identity);

                    // Genera una escala aleatoria dentro de los rangos especificados
                    float randomScale = Random.Range(minScale, maxScale);
                    newRock.transform.localScale = Vector3.one * randomScale;

                    Rigidbody2D rb = newRock.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = Vector2.zero;
                        Vector2 randomDirection = Random.insideUnitCircle.normalized;
                        rb.AddForce(randomDirection * explosionForce, ForceMode2D.Impulse);
                    }

                    RockHealth newRockHealth = newRock.GetComponent<RockHealth>();
                    if (newRockHealth != null)
                    {
                        newRockHealth.canDivide = false;
                        newRockHealth.hasEnteredScreen = true;
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
