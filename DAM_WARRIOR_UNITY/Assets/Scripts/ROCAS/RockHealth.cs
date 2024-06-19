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
    public float minScale = 0.5f; // Valor mínimo de la escala.
    public float maxScale = 1.5f; // Valor máximo de la escala.
    private bool hasEnteredScreen = false;

    private ScoreManager scoreManager; // Referencia al ScoreManager

    void Start()
    {
        // Inicializa la salud de la roca y obtiene el ScoreManager.
        currentHealth = maxHealth;
        Debug.Log("Roca inicializada: " + gameObject.name);

        // Verifica si los componentes necesarios están presentes.
        if (GetComponent<Rigidbody2D>() == null)
            Debug.LogError("Rigidbody2D no encontrado en " + gameObject.name);
        if (GetComponent<Collider2D>() == null)
            Debug.LogError("Collider2D no encontrado en " + gameObject.name);
        if (GetComponent<Renderer>() == null)
            Debug.LogError("Renderer no encontrado en " + gameObject.name);
        if (GetComponent<RockMovement>() == null)
            Debug.LogError("RockMovement script no encontrado en " + gameObject.name);

        // Busca dinámicamente una instancia de ScoreManager en la escena
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogWarning("ScoreManager no encontrado en la escena.");
        }
    }

    void OnBecameVisible()
    {
        // Marca la roca como visible cuando entra en pantalla.
        hasEnteredScreen = true;
    }

    void OnBecameInvisible()
    {
        // Destruye la roca si se vuelve invisible después de haber sido visible.
        if (hasEnteredScreen)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        // Aplica daño a la roca y verifica si debe destruirse.
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            // Reproduce un sonido de destrucción y crea una explosión.
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

            // Divide la roca en rocas más pequeñas si puede dividirse.
            if (canDivide && rockPrefabs.Length > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    int index = Random.Range(0, rockPrefabs.Length);
                    GameObject newRock = Instantiate(rockPrefabs[index], transform.position, Quaternion.identity);
                    Debug.Log("Nueva roca creada en la posición: " + newRock.transform.position);

                    // Genera una escala aleatoria dentro de los rangos especificados.
                    float randomScale = Random.Range(minScale, maxScale);
                    newRock.transform.localScale = Vector3.one * randomScale;
                    Debug.Log("Nueva roca escala: " + newRock.transform.localScale);

                    Rigidbody2D rb = newRock.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = Vector2.zero;
                        Vector2 randomDirection = Random.insideUnitCircle.normalized;
                        rb.AddForce(randomDirection * explosionForce, ForceMode2D.Impulse);
                        Debug.Log("Dirección de la nueva roca: " + randomDirection);
                    }

                    Renderer renderer = newRock.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        Debug.Log("Renderer encontrado para la nueva roca: " + renderer.name);
                    }
                    else
                    {
                        Debug.LogError("Renderer no encontrado para la nueva roca: " + newRock.name);
                    }

                    RockHealth newRockHealth = newRock.GetComponent<RockHealth>();
                    if (newRockHealth != null)
                    {
                        newRockHealth.canDivide = false;
                        newRockHealth.hasEnteredScreen = true;
                    }
                }
            }

            // Actualiza la puntuación en ScoreManager si está disponible.
            if (scoreManager != null)
            {
                scoreManager.AddScore(30);
            }

            // Destruye la roca.
            Destroy(gameObject);
        }
    }
}
