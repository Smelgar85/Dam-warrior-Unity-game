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

    // El manejo de colisiones y daños se delega a EnemyHealth
}
