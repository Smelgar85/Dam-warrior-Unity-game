/**
 * BossMovement.cs
 * Este script controla el movimiento del jefe, incluyendo avance inicial y oscilación.
 */

using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del jefe.
    public float advanceDistance = 5f; // Distancia que avanza inicialmente.
    public float swayDistance = 1f; // Distancia de oscilación hacia adelante y hacia atrás.
    public float swaySpeed = 1f; // Velocidad de oscilación.
    public float verticalSwayDistance = 1f; // Distancia de oscilación vertical.
    public float verticalSwaySpeed = 1f; // Velocidad de oscilación vertical.

    private Vector3 initialPosition;
    private bool advancing = true;

    void Start()
    {
        // Guarda la posición inicial del jefe.
        initialPosition = transform.position;
    }

    void Update()
    {
        // Controla el movimiento del jefe, alternando entre avanzar y oscilar.
        if (advancing)
        {
            Advance();
        }
        else
        {
            Sway();
        }
    }

    void Advance()
    {
        // Mueve el jefe hacia la izquierda hasta alcanzar la distancia de avance.
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // Si ha avanzado la distancia deseada, detiene el avance.
        if (transform.position.x <= initialPosition.x - advanceDistance)
        {
            advancing = false;
        }
    }

    void Sway()
    {
        // Oscilación horizontal y vertical.
        float swayAmount = Mathf.Sin(Time.time * swaySpeed) * swayDistance;
        float verticalSwayAmount = Mathf.Sin(Time.time * verticalSwaySpeed) * verticalSwayDistance;

        transform.position = new Vector3(initialPosition.x - advanceDistance + swayAmount, initialPosition.y + verticalSwayAmount, initialPosition.z);

        // Si ha alcanzado el límite de la oscilación hacia atrás, vuelve a avanzar.
        if (swayAmount <= -swayDistance)
        {
            advancing = true;
        }
    }
}
