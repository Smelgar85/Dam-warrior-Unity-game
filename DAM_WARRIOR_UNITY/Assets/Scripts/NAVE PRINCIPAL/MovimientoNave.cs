using System.Collections;
using UnityEngine;

public class MovimientoNave : MonoBehaviour
{
    public float velocidad = 5f;
    public float velocidadRotacion = 100f;
    public float maxRotacionX = 45f;
    public GameObject JetEngine1;
    public GameObject JetEngine2;

    private Vector3 escalaJetReposo = new Vector3(0.01f, 0.01f, 0.006f);
    private Vector3 escalaJetAcelerado = new Vector3(0.01f, 0.01f, 0.018f);
    public float velocidadTransicionEscala = 5f; // Velocidad de la transición de la escala

    private VidaNave vidaNave;

    void Start()
    {
        vidaNave = GetComponent<VidaNave>();
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
        if (!vidaNave.esInvulnerable && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Nave ha colisionado con: " + collision.gameObject.name);

            vidaNave.AplicarDanio(1);
        }
        else if (vidaNave.esInvulnerable)
        {
            Debug.Log("Colisión ignorada porque la nave es invulnerable");
        }
    }

    void ResetearMovimiento()
    {
        // Detenemos el movimiento de la nave
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
            rb.rotation = 0f;
        }
    }
}
