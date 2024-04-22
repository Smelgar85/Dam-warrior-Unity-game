using System.Collections;
using UnityEngine;

public class MovimientoEnemigoZigZag : MonoBehaviour
{
    public float lateralSpeed = 7f; // Velocidad de movimiento lateral
    public float minVerticalAmplitude = 0.02f; // Amplitud mínima del movimiento vertical
    public float maxVerticalAmplitude = 0.05f; // Amplitud máxima del movimiento vertical
    public float minVerticalFrequency = 1f; // Frecuencia mínima del movimiento vertical
    public float maxVerticalFrequency = 5f; // Frecuencia máxima del movimiento vertical
    public float maxRotationX = 45f; // Máxima rotación en el eje X
    public float rotationSmoothness = 5f; // Suavidad de la interpolación de rotación
    public GameObject campoDeFuerzaEnemigo; // Referencia al GameObject del campo de fuerza
    public AudioClip campoDeFuerzaEnemigoSound;
    public float duracionFadeInEn = 1f; // Duración del fade in en segundos
    public float duracionFadeOutEn = 1f; // Duración del fade out en segundos
    public float esperaFadeOutEn = 1f; // Tiempo de espera antes de iniciar el fade out

    private float startingYPosition;
    private float verticalAmplitude;
    private float verticalFrequency;

    void Start()
    {
        startingYPosition = transform.position.y;
        verticalAmplitude = Random.Range(minVerticalAmplitude, maxVerticalAmplitude);
        verticalFrequency = Random.Range(minVerticalFrequency, maxVerticalFrequency);
        if (campoDeFuerzaEnemigo != null)
        {
            // Asegúrate de que el campo de fuerza comienza invisible, si su shader soporta la transparencia.
            // Esto se hace ajustando la transparencia a 0.
            SetFieldAlpha(0f);
        }
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void SetFieldAlpha(float alpha)
    {
        if (campoDeFuerzaEnemigo != null)
        {
            Renderer renderer = campoDeFuerzaEnemigo.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color color = renderer.material.color;
                color.a = alpha;
                renderer.material.color = color;
            }
        }
    }

    void Move()
    {
        // Mueve la nave hacia la izquierda
        transform.Translate(Vector3.left * lateralSpeed * Time.deltaTime);

        // Calcula el desplazamiento vertical basado en un patrón de zigzag suave
        float yOffset = Mathf.Sin(Time.time * verticalFrequency) * verticalAmplitude;

        // Actualiza la posición en el eje Y
        transform.position += Vector3.up * yOffset * Time.deltaTime;

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    void Rotate()
    {
        // Verifica si la nave se está moviendo hacia arriba o hacia abajo
        float verticalMovement = Mathf.Sign(transform.position.y - startingYPosition);

        // Calcula la rotación en función del movimiento vertical (invierte el ángulo)
        float targetRotationX = -verticalMovement * maxRotationX;

        // Aplica la rotación al objeto con interpolación suave
        Quaternion targetRotation = Quaternion.Euler(targetRotationX, 0f, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness); // Ajusta la suavidad de la interpolación
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si la nave colisiona con un objeto que no es un disparo láser
        if (collision.gameObject.tag != "BulletEnemy")
        {
            Debug.Log("Nave ha colisionado con: " + collision.gameObject.name);

            // Inicia el fade in solo si la colisión no es con un disparo láser
            StartCoroutine(FadeFieldIn(duracionFadeInEn));
        }
        else
        {
            // Opcional: Puedes agregar aquí más lógica si necesitas manejar específicamente la colisión con disparos láser
            Debug.Log("Colisión con DisparoLaser ignorada.");
        }

        IEnumerator FadeFieldIn(float duration)
        {
            // Reproduce el sonido cuando se inicia el FadeFieldIn
            AudioSource audioSource = GameObject.Find("SFX_SHIELD").GetComponent<AudioSource>();
            if (campoDeFuerzaEnemigoSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(campoDeFuerzaEnemigoSound);
            }

            float counter = 0f;
            while (counter < duration)
            {
                counter += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, counter / duration);
                SetFieldAlpha(alpha);
                yield return null;
            }

            // Espera y luego inicia FadeFieldOut
            yield return new WaitForSeconds(esperaFadeOutEn);
            StartCoroutine(FadeFieldOut(duracionFadeOutEn));
        }

        IEnumerator FadeFieldOut(float duration)
        {
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
}
