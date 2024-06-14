using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 2; // Vida de la nave enemiga
    public GameObject deathEffectPrefab; // Prefab de la animación de muerte
    private AudioSource explosionAudioSource; // Referencia al componente AudioSource del sonido de explosión
    public AudioClip dieSound;
    public GameObject campoDeFuerzaEnemigo; // Referencia al GameObject del campo de fuerza
    public AudioClip campoDeFuerzaEnemigoSound;
    private AudioSource audioSource; // Componente de audio para el campo de fuerza
    public float duracionFadeInEn = 1f; // Duración del fade in en segundos
    public float duracionFadeOutEn = 1f; // Duración del fade out en segundos
    public float esperaFadeOutEn = 1f; // Tiempo de espera antes de iniciar el fade out

    private GameController gameController;

    void Start()
    {
        // Obtener el componente AudioSource del GameObject "SFX_DEATH_ENEMY"
        explosionAudioSource = GameObject.Find("SFX_DEATH_ENEMY").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>(); // Asumiendo que el objeto tiene un componente AudioSource
        gameController = FindObjectOfType<GameController>();

        if (campoDeFuerzaEnemigo != null)
        {
            // Asegúrate de que el campo de fuerza comienza invisible, si su shader soporta la transparencia.
            SetFieldAlpha(0f);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount; // Reduce la salud por el daño recibido
        if (gameController != null)
        {
            gameController.RegistrarDanoCausado(damageAmount); // Registrar el daño causado
        }

        // Activa el campo de fuerza
        StartCoroutine(FadeFieldIn(0.2f)); // Duración ajustable

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

        // Actualizar la puntuación en ScoreManager
        ScoreManager.Instance.AddScore(50);

        // Aquí puedes implementar cualquier lógica adicional al morir la nave enemiga
        Destroy(gameObject); // Por ahora, simplemente destruimos el objeto de la nave
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignorar colisiones con las balas enemigas
        if (collision.gameObject.CompareTag("BulletEnemy"))
        {
            return;
        }

        // Aplicar daño por colisión con cualquier objeto que no tenga el tag "BulletEnemy"
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Rock") || !collision.gameObject.CompareTag("BulletEnemy"))
        {
            Debug.Log("Enemigo colisionó con: " + collision.gameObject.name);
            TakeDamage(1);

            // Destruir el objeto que colisiona si tiene el tag "Bullet"
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void SetFieldAlpha(float alpha)
    {
        if (campoDeFuerzaEnemigo != null)
        {
            Renderer renderer = campoDeFuerzaEnemigo.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color color = renderer.material.color;
                color.a = alpha;
                renderer.material.color = color;
                Debug.Log("Campo de fuerza actualizado con alpha: " + alpha);
            }
        }
    }

    IEnumerator FadeFieldIn(float duration)
    {
        Debug.Log("Iniciando FadeFieldIn");
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, counter / duration);
            SetFieldAlpha(alpha);
            yield return null;
        }

        yield return new WaitForSeconds(1f); // Espera antes de comenzar el fade out
        StartCoroutine(FadeFieldOut(0.2f)); // Duración ajustable
    }

    IEnumerator FadeFieldOut(float duration)
    {
        Debug.Log("Iniciando FadeFieldOut");
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, counter / duration);
            SetFieldAlpha(alpha);
            yield return null;
        }
    }
}
