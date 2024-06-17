/**
 * RockMovement.cs
 * Este script controla el movimiento y la destrucción de las rocas en el juego.
 */

using UnityEngine;

public class RockMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    void Update()
    {
        Move();

        // Destruye la roca si se mueve fuera de la pantalla.
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        // Mueve la roca hacia la izquierda y la rota alrededor del eje Y.
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
