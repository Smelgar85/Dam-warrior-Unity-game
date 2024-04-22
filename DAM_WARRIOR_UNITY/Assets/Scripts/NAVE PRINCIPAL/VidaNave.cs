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
    public string nombreHijoConMeshRenderer = "MODELO_DAM_WARRIOR/MESH_NAVE";
    private bool esInvulnerable = false;

    private float tamanoInicialX;
    //private bool esInvulnerable = false;
    private float tiempoUltimoDanio; // Nuevo: para controlar el cooldown
    public float tiempoCooldown = 2f; // Nuevo: tiempo de cooldown en segundos

    void Start()
    {
        if (barraDeVida == null)
        {
            Debug.LogError("¡No se ha asignado la barra de vida en el inspector!");
        }

        tamanoInicialX = barraDeVida.localScale.x;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!esInvulnerable && !collision.gameObject.CompareTag("Bullet") && Time.time - tiempoUltimoDanio > tiempoCooldown)
        {
            PerderSalud(1);
            tiempoUltimoDanio = Time.time;
        }
    }

    public void PerderSalud(int cantidad)
    {
        health -= cantidad;
        health = Mathf.Max(health, 0);

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

    IEnumerator RespawnNave()
    {
        // Esperamos un segundo antes de hacer respawn
        yield return new WaitForSeconds(1f);

        // Hacemos respawn de la nave desde fuera del encuadre (desde la izquierda de la pantalla)
        Vector3 startPosition = new Vector3(-8f, transform.position.y, transform.position.z);
        transform.position = startPosition;

        // Hacemos parpadear la nave mientras aparece
        StartCoroutine(RespawnAndBlink());

        // Esperamos hasta que la nave haya aparecido completamente antes de permitir al jugador controlarla
        yield return new WaitForSeconds(3f); // Ajusta el tiempo según sea necesario

        // Permitimos al jugador controlar la nave nuevamente

    }
}
