using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 2; // Vida de la nave enemiga
    public GameObject deathEffectPrefab; // Prefab de la animación de muerte
    private AudioSource explosionAudioSource; // Referencia al componente AudioSource del sonido de explosión
    public AudioClip dieSound;

    void Start()
    {
        // Obtener el componente AudioSource del GameObject "SFX_EXPLOSION"
        explosionAudioSource = GameObject.Find("SFX_DEATH_ENEMY").GetComponent<AudioSource>();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount; // Reduce la salud por el daño recibido

        // Verifica si la salud es igual o menor a 0
        if (health <= 0)
        {
            Die(); // Llama a la función Die si la salud es igual o menor a 0
        }
    }

    void Die()
    {
        // Instancia el efecto de muerte en la posición actual de la nave enemiga
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        // Reproduce el sonido de explosión si se ha encontrado el AudioSource
        if (explosionAudioSource != null)
        {
            explosionAudioSource.PlayOneShot(dieSound);
        }

        // Aquí puedes implementar cualquier lógica adicional al morir la nave enemiga
        Destroy(gameObject); // Por ahora, simplemente destruimos el objeto de la nave
    }
}
