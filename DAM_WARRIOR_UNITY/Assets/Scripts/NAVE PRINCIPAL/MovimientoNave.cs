// Este script gestiona el movimiento de una nave espacial, incluyendo la rotación, desplazamiento y efectos visuales de los motores.

using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoNave : MonoBehaviour
{
    public float velocidad = 5f;
    public float velocidadRotacion = 100f;
    public float maxRotacionX = 45f;
    public GameObject JetEngine1;
    public GameObject JetEngine2;

    private Vector3 escalaJetReposo = new Vector3(0.01f, 0.01f, 0.006f);
    private Vector3 escalaJetAcelerado = new Vector3(0.01f, 0.01f, 0.018f);
    public float velocidadTransicionEscala = 5f;

    private VidaNave vidaNave;
    private Vector2 moveInput;

    // Método llamado al inicio del juego.
    void Start()
    {
        vidaNave = GetComponent<VidaNave>();
    }

    // Método llamado cada frame para actualizar el estado del objeto.
    void Update()
    {
        // Calcula la nueva posición de la nave basada en la entrada de movimiento.
        Vector3 nuevaPosicion = transform.position + new Vector3(moveInput.x, moveInput.y, 0f) * velocidad * Time.deltaTime;
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, -10f, 10f);
        nuevaPosicion.y = Mathf.Clamp(nuevaPosicion.y, -5f, 5f);
        transform.position = nuevaPosicion;

        // Ajusta la escala de los motores según el input de movimiento.
        Vector3 escalaObjetivo = moveInput.x != 0 ? escalaJetAcelerado : escalaJetReposo;
        JetEngine1.transform.localScale = Vector3.Lerp(JetEngine1.transform.localScale, escalaObjetivo, velocidadTransicionEscala * Time.deltaTime);
        JetEngine2.transform.localScale = Vector3.Lerp(JetEngine2.transform.localScale, escalaObjetivo, velocidadTransicionEscala * Time.deltaTime);

        // Calcula y aplica la rotación de la nave.
        float rotacionX = moveInput.y * velocidadRotacion * Time.deltaTime;
        float currentRotacionX = transform.rotation.eulerAngles.x;
        float newRotacionX = currentRotacionX + rotacionX;

        if (newRotacionX > 180f) newRotacionX -= 360f;
        newRotacionX = Mathf.Clamp(newRotacionX, -maxRotacionX, maxRotacionX);
        transform.rotation = Quaternion.Euler(newRotacionX, 0f, 0f);
    }

    // Método manejado por el Input System para leer la entrada de movimiento.
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Método llamado cuando la nave colisiona con otro objeto.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!vidaNave.esInvulnerable && collision.gameObject.CompareTag("Enemy"))
        {
            vidaNave.AplicarDanio(1);
        }
    }

    // Resetea el movimiento y rotación de la nave.
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
