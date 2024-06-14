using System.Collections;
using UnityEngine;

public class MovimientoEnemigoZigZag : MonoBehaviour
{
    public float lateralSpeed = 7f; // Velocidad de movimiento lateral
    public float minVerticalAmplitude = 0.02f; // Amplitud m�nima del movimiento vertical
    public float maxVerticalAmplitude = 0.05f; // Amplitud m�xima del movimiento vertical
    public float minVerticalFrequency = 1f; // Frecuencia m�nima del movimiento vertical
    public float maxVerticalFrequency = 5f; // Frecuencia m�xima del movimiento vertical
    public float maxRotationX = 45f; // M�xima rotaci�n en el eje X
    public float rotationSmoothness = 5f; // Suavidad de la interpolaci�n de rotaci�n

    private float startingYPosition;
    private float verticalAmplitude;
    private float verticalFrequency;

    void Start()
    {
        startingYPosition = transform.position.y;
        verticalAmplitude = Random.Range(minVerticalAmplitude, maxVerticalAmplitude);
        verticalFrequency = Random.Range(minVerticalFrequency, maxVerticalFrequency);
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        // Mueve la nave hacia la izquierda
        transform.Translate(Vector3.left * lateralSpeed * Time.deltaTime);

        // Calcula el desplazamiento vertical basado en un patr�n de zigzag suave
        float yOffset = Mathf.Sin(Time.time * verticalFrequency) * verticalAmplitude;

        // Actualiza la posici�n en el eje Y
        transform.position += Vector3.up * yOffset * Time.deltaTime;

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    void Rotate()
    {
        // Verifica si la nave se est� moviendo hacia arriba o hacia abajo
        float verticalMovement = Mathf.Sign(transform.position.y - startingYPosition);

        // Calcula la rotaci�n en funci�n del movimiento vertical (invierte el �ngulo)
        float targetRotationX = -verticalMovement * maxRotationX;

        // Aplica la rotaci�n al objeto con interpolaci�n suave
        Quaternion targetRotation = Quaternion.Euler(targetRotationX, 0f, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness); // Ajusta la suavidad de la interpolaci�n
    }

    // El manejo de colisiones y da�os se delega a EnemyHealth
}
