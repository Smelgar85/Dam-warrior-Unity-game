using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del jefe
    public float advanceDistance = 5f; // Distancia que avanza inicialmente
    public float swayDistance = 1f; // Distancia de oscilaci�n hacia adelante y hacia atr�s
    public float swaySpeed = 1f; // Velocidad de oscilaci�n
    public float verticalSwayDistance = 1f; // Distancia de oscilaci�n vertical
    public float verticalSwaySpeed = 1f; // Velocidad de oscilaci�n vertical

    private Vector3 initialPosition;
    private bool advancing = true;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
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
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // Si ha avanzado la distancia deseada, detiene el avance
        if (transform.position.x <= initialPosition.x - advanceDistance)
        {
            advancing = false;
        }
    }

    void Sway()
    {
        // Oscilaci�n horizontal
        float swayAmount = Mathf.Sin(Time.time * swaySpeed) * swayDistance;
        // Oscilaci�n vertical
        float verticalSwayAmount = Mathf.Sin(Time.time * verticalSwaySpeed) * verticalSwayDistance;

        transform.position = new Vector3(initialPosition.x - advanceDistance + swayAmount, initialPosition.y + verticalSwayAmount, initialPosition.z);

        // Si ha alcanzado el l�mite de la oscilaci�n hacia atr�s, vuelve a avanzar
        if (swayAmount <= -swayDistance)
        {
            advancing = true;
        }
    }
}
