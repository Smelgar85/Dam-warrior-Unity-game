using System.Collections;
using UnityEngine;

public class MovimientoNave1 : MonoBehaviour
{
    public float velocidad = 5f;
    public float velocidadRotacion = 100f;
    public float maxRotacionX = 45f;
    public GameObject JetEngine1;
    public GameObject JetEngine2;
    public GameObject campoDeFuerza; // Referencia al GameObject del campo de fuerza

    // Variables para el sistema de sonido
    public AudioClip campoDeFuerzaSound; // Arrastra aquí tu AudioClip desde el Inspector

    private Vector3 escalaJetReposo = new Vector3(0.01f, 0.01f, 0.006f);
    private Vector3 escalaJetAcelerado = new Vector3(0.01f, 0.01f, 0.018f);
    public float velocidadTransicionEscala = 5f; // Velocidad de la transición de la escala

    public float duracionFadeIn = 1f; // Duración del fade in en segundos
    public float duracionFadeOut = 1f; // Duración del fade out en segundos
    public float esperaFadeOut = 1f; // Tiempo de espera antes de iniciar el fade out


    void Start()
    {
        if (campoDeFuerza != null)
        {
            // Asegúrate de que el campo de fuerza comienza invisible, si su shader soporta la transparencia.
            // Esto se hace ajustando la transparencia a 0.
            SetFieldAlpha(0f);
        }
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 nuevaPosicion = transform.position + new Vector3(movimientoHorizontal, movimientoVertical, 0f) * velocidad * Time.deltaTime;
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, -10f, 10f);
        nuevaPosicion.y = Mathf.Clamp(nuevaPosicion.y, -5f, 5f);
        transform.position = nuevaPosicion;

        Vector3 escalaObjetivo = movimientoHorizontal > 0 ? escalaJetAcelerado : escalaJetReposo;
        JetEngine1.transform.localScale = Vector3.Lerp(JetEngine1.transform.localScale, escalaObjetivo, velocidadTransicionEscala * Time.deltaTime);
        JetEngine2.transform.localScale = Vector3.Lerp(JetEngine2.transform.localScale, escalaObjetivo, velocidadTransicionEscala * Time.deltaTime);

        float rotacionX = movimientoVertical * velocidadRotacion * Time.deltaTime;
        float currentRotacionX = transform.rotation.eulerAngles.x;
        float newRotacionX = currentRotacionX + rotacionX;

        if (newRotacionX > 180f) newRotacionX -= 360f;
        newRotacionX = Mathf.Clamp(newRotacionX, -maxRotacionX, maxRotacionX);
        transform.rotation = Quaternion.Euler(newRotacionX, 0f, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si la nave colisiona con un objeto que no es un disparo láser
        if (collision.gameObject.tag != "Bullet")
        {
            Debug.Log("Nave ha colisionado con: " + collision.gameObject.name);

            // Inicia el fade in solo si la colisión no es con un disparo láser
            StartCoroutine(FadeFieldIn(duracionFadeIn));
        }
        else
        {
            // Opcional: Puedes agregar aquí más lógica si necesitas manejar específicamente la colisión con disparos láser
            Debug.Log("Colisión con DisparoLaser ignorada.");
        }
    }




    IEnumerator FadeFieldIn(float duration)
    {
        // Reproduce el sonido cuando se inicia el FadeFieldIn
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

        // Espera y luego inicia FadeFieldOut
        yield return new WaitForSeconds(esperaFadeOut);
        StartCoroutine(FadeFieldOut(duracionFadeOut));
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

    void SetFieldAlpha(float alpha)
    {
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

    void ResetearMovimiento()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
            rb.rotation = 0f;
        }
    }
}
