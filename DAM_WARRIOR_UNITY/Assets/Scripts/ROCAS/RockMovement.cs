using UnityEngine;

public class RockMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    void Update()
    {
        Move();

        // Ajusta el valor de la posición x para cambiar el punto de destrucción
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
