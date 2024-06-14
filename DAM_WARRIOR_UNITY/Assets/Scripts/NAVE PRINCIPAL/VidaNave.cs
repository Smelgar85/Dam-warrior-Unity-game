using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaNave : MonoBehaviour
{
    public int health = 4;
    public int lives = 3;
    public Transform barraDeVida;
    public GameObject LIFE_1;
    public GameObject LIFE_2;
    public GameObject LIFE_3;
    public GameObject explosionPrefab;
    public GameObject campoDeFuerza; // Referencia al campo de fuerza
    public AudioClip damageSound; // Sonido de daño
    private AudioSource audioSource; // Componente de audio
    public string nombreHijoConMeshRenderer = "MODELO_DAM_WARRIOR/MESH_NAVE";
    public bool esInvulnerable = false; // Cambiado a público

    private float tamanoInicialX;
    public float tiempoUltimoDanio; // Cambiado a público
    public float tiempoCooldown = 2f; // Nuevo: tiempo de cooldown en segundos

    // Variables para el sistema de sonido
    public AudioClip campoDeFuerzaSound; // Arrastra aquí tu AudioClip desde el Inspector

    // Variables de duración del campo de fuerza
    public float duracionFadeIn = 1f; // Duración del fade in en segundos
    public float duracionFadeOut = 1f; // Duración del fade out en segundos
    public float esperaFadeOut = 1f; // Tiempo de espera antes de iniciar el fade out

    private GameController gameController;

    void Start()
    {
        if (barraDeVida == null)
        {
            Debug.LogError("¡No se ha asignado la barra de vida en el inspector!");
        }

        tamanoInicialX = barraDeVida.localScale.x;
        gameController = FindObjectOfType<GameController>();
        audioSource = GetComponent<AudioSource>();

        if (gameController == null)
        {
            Debug.LogWarning("GameController no encontrado en la escena.");
        }

        if (campoDeFuerza != null)
        {
            // Asegúrate de que el campo de fuerza comienza invisible, si su shader soporta la transparencia.
            SetFieldAlpha(0f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Colisión detectada con: {collision.gameObject.name}");

        // Ignorar colisiones con las balas del jugador
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Colisión con bala del jugador ignorada");
            return;
        }

        if (!esInvulnerable)
        {
            Debug.Log("Daño recibido por colisión con: " + collision.gameObject.name);
            AplicarDanio(1);
        }
        else
        {
            Debug.Log("Colisión ignorada porque la nave es invulnerable");
        }
    }

    public void AplicarDanio(int cantidad)
    {
        Debug.Log("Aplicando daño: " + cantidad);
        PerderSalud(cantidad);
        tiempoUltimoDanio = Time.time;
        StartCoroutine(ActivarInvulnerabilidad());

        // Inicia el fade in del campo de fuerza y reproduce el sonido de daño
        StartCoroutine(FadeFieldIn(duracionFadeIn)); // Duración ajustable
        PlayDamageSound();
    }

    public IEnumerator ActivarInvulnerabilidad()
    {
        Debug.Log("Activando invulnerabilidad");
        esInvulnerable = true;
        yield return new WaitForSeconds(tiempoCooldown);
        esInvulnerable = false;
        Debug.Log("Invulnerabilidad desactivada");
    }

    public void PerderSalud(int cantidad)
    {
        Debug.Log($"Daño recibido: {cantidad}");
        health -= cantidad;
        health = Mathf.Max(health, 0);

        if (gameController != null)
        {
            gameController.RegistrarDanoRecibido(cantidad); // Registrar el daño recibido
        }

        switch (health)
        {
            case 4:
                barraDeVida.localScale = new Vector3(2.4f, barraDeVida.localScale.y, barraDeVida.localScale.z);
                barraDeVida.localPosition = new Vector3(-2.15f, 0.227f, barraDeVida.localPosition.z);
                break;
            case 3:
                barraDeVida.localScale = new Vector3(1.8f, barraDeVida.localScale.y, barraDeVida.localScale.z);
                barraDeVida.localPosition = new Vector3(-2.42f, 0.227f, barraDeVida.localPosition.z);
                break;
            case 2:
                barraDeVida.localScale = new Vector3(1.2f, barraDeVida.localScale.y, barraDeVida.localScale.z);
                barraDeVida.localPosition = new Vector3(-2.74f, 0.227f, barraDeVida.localPosition.z);
                break;
            case 1:
                barraDeVida.localScale = new Vector3(0.6f, barraDeVida.localScale.y, barraDeVida.localScale.z);
                barraDeVida.localPosition = new Vector3(-3.038f, 0.227f, barraDeVida.localPosition.z);
                break;
            case 0:
                barraDeVida.localScale = new Vector3(2.4f, barraDeVida.localScale.y, barraDeVida.localScale.z);
                barraDeVida.localPosition = new Vector3(-2.15f, 0.227f, barraDeVida.localPosition.z);
                PerderVida();

                SpawnExplosion(); // Llamamos a la función para reproducir la explosión

                health = 4;
                return;
        }
    }

    void PerderVida()
    {
        lives--;

        switch (lives)
        {
            case 2:
                LIFE_3.SetActive(false);
                break;
            case 1:
                LIFE_2.SetActive(false);
                break;
            case 0:
                LIFE_1.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }

        if (lives <= 0)
        {
            Debug.Log("¡La nave ha sido destruida!");
        }
        else
        {
            StartCoroutine(RespawnAndBlink());
        }
    }

    IEnumerator RespawnAndBlink()
    {
        Vector3 startPosition = new Vector3(-8.029f, 1.323f, 0);
        transform.position = startPosition;
        esInvulnerable = true;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            Debug.LogError("Ningún Renderer encontrado. Asegúrate de que haya al menos uno en los hijos del objeto.");
            yield break;
        }

        float endTime = Time.time + 3.0f;
        while (Time.time < endTime)
        {
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = !renderer.enabled;
            }
            yield return new WaitForSeconds(0.2f);
        }

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true; // Aseguramos que los objetos estén visibles al finalizar el parpadeo
        }
        esInvulnerable = false;
    }

    void SpawnExplosion()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }

    private void PlayDamageSound()
    {
        Debug.Log("Reproduciendo sonido de daño");
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    private void SetFieldAlpha(float alpha)
    {
        if (campoDeFuerza != null)
        {
            Renderer renderer = campoDeFuerza.GetComponent<Renderer>();
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
        // Reproduce el sonido cuando se inicia el FadeFieldIn
        if (campoDeFuerzaSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(campoDeFuerzaSound);
        }

        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, counter / duration);
            SetFieldAlpha(alpha);
            yield return null;
        }

        yield return new WaitForSeconds(esperaFadeOut); // Espera antes de comenzar el fade out
        StartCoroutine(FadeFieldOut(duracionFadeOut)); // Duración ajustable
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
