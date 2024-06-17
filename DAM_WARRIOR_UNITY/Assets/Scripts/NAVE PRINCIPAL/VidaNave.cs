/**
 * VidaNave.cs
 * Este script maneja la salud y las vidas de la nave, así como la activación de la invulnerabilidad y los efectos visuales.
 */

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
    public GameObject campoDeFuerza; // Referencia al campo de fuerza.
    public AudioClip campoDeFuerzaSound; // Sonido del campo de fuerza.
    public AudioClip explosionSound; // Sonido de la explosión.
    public string nombreHijoConMeshRenderer = "MODELO_DAM_WARRIOR/MESH_NAVE";
    public bool esInvulnerable = false;

    private float tamanoInicialX;
    public float tiempoUltimoDanio;
    public float tiempoCooldown = 2f; // Tiempo de cooldown en segundos.

    // Variables de duración del campo de fuerza.
    public float duracionFadeIn = 1f; // Duración del fade in en segundos.
    public float duracionFadeOut = 1f; // Duración del fade out en segundos.
    public float esperaFadeOut = 1f; // Tiempo de espera antes de iniciar el fade out.

    private GameController gameController;

    void Start()
    {
        // Inicializa referencias y verifica componentes.
        if (barraDeVida == null)
        {
            Debug.LogError("¡No se ha asignado la barra de vida en el inspector!");
        }

        tamanoInicialX = barraDeVida.localScale.x;
        gameController = FindObjectOfType<GameController>();

        if (gameController == null)
        {
            Debug.LogWarning("GameController no encontrado en la escena.");
        }

        if (campoDeFuerza != null)
        {
            SetFieldAlpha(0f);
        }

        ActualizarBarraDeVida(); // Asegurarse de que la barra de vida se configure correctamente al inicio.
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Maneja las colisiones y aplica daño si no es invulnerable.
        Debug.Log($"Colisión detectada con: {collision.gameObject.name} con tag: {collision.gameObject.tag}");

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
    }

    public void AplicarDanio(int cantidad)
    {
        // Aplica daño a la nave y activa la invulnerabilidad temporal.
        if (!esInvulnerable)
        {
            Debug.Log("Aplicando daño: " + cantidad);
            PerderSalud(cantidad);
            tiempoUltimoDanio = Time.time;
            StartCoroutine(ActivarInvulnerabilidad());

            // Inicia el fade in del campo de fuerza y reproduce el sonido de daño.
            StartCoroutine(FadeFieldIn(duracionFadeIn)); // Duración ajustable.
        }
        else
        {
            Debug.Log("Intento de aplicar daño ignorado porque la nave es invulnerable");
        }
    }

    public IEnumerator ActivarInvulnerabilidad()
    {
        // Activa la invulnerabilidad temporalmente.
        Debug.Log("Activando invulnerabilidad");
        esInvulnerable = true;
        yield return new WaitForSeconds(tiempoCooldown);
        esInvulnerable = false;
        Debug.Log("Invulnerabilidad desactivada");
    }

    public void PerderSalud(int cantidad)
    {
        // Reduce la salud de la nave y verifica si debe perder una vida.
        health -= cantidad;
        ScoreManager.Instance.RegisterDamageTaken(cantidad);
        health = Mathf.Max(health, 0);

        if (gameController != null)
        {
            gameController.RegistrarDanoRecibido(cantidad);
        }

        if (health <= 0)
        {
            PerderVida();
            return;
        }

        ActualizarBarraDeVida();
    }

    void ActualizarBarraDeVida()
    {
        // Actualiza la barra de vida según la salud actual.
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
        }
    }

    void PerderVida()
    {
        // Reduce las vidas de la nave y maneja la lógica de reinicio o pérdida del juego.
        lives--;

        // Reproduce el sonido de pérdida de vida.
        AudioSource audioSource = GameObject.Find("SFX_EXPLOSION").GetComponent<AudioSource>();
        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }

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
            health = 4; // Resetear la salud.
            ActualizarBarraDeVida(); // Asegurar que la barra de vida se actualiza.
            StartCoroutine(RespawnAndBlink());
        }
    }

    IEnumerator RespawnAndBlink()
    {
        // Maneja el respawn de la nave y la animación de parpadeo de invulnerabilidad.
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
            renderer.enabled = true;
        }
        esInvulnerable = false;
    }

    void SpawnExplosion()
    {
        // Genera una explosión en la posición de la nave.
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }

    private void SetFieldAlpha(float alpha)
    {
        // Establece la transparencia del campo de fuerza.
        if (campoDeFuerza != null)
        {
            Renderer renderer = campoDeFuerza.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color color = renderer.material.color;
                color.a = alpha;
                renderer.material.color = color;
            }
        }
    }

    IEnumerator FadeFieldIn(float duration)
    {
        // Realiza un fade in en el campo de fuerza.
        AudioSource audioSource = GameObject.Find("SFX_SHIELD").GetComponent<AudioSource>();
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

        yield return new WaitForSeconds(esperaFadeOut); // Espera antes de comenzar el fade out.
        StartCoroutine(FadeFieldOut(duracionFadeOut)); // Duración ajustable.
    }

    IEnumerator FadeFieldOut(float duration)
    {
        // Realiza un fade out en el campo de fuerza.
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
