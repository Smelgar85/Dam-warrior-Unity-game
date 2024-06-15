using System.Collections;
using UnityEngine;

public class MovimientoEnemigoZigZag : MonoBehaviour
{
    public float lateralSpeed = 7f;
    public float minVerticalAmplitude = 0.02f;
    public float maxVerticalAmplitude = 0.05f;
    public float minVerticalFrequency = 1f;
    public float maxVerticalFrequency = 5f;
    public float maxRotationX = 45f;
    public float rotationSmoothness = 5f;

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
        transform.Translate(Vector3.left * lateralSpeed * Time.deltaTime);

        float yOffset = Mathf.Sin(Time.time * verticalFrequency) * verticalAmplitude;
        transform.position += Vector3.up * yOffset * Time.deltaTime;

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    void Rotate()
    {
        float verticalMovement = Mathf.Sign(transform.position.y - startingYPosition);
        float targetRotationX = -verticalMovement * maxRotationX;

        Quaternion targetRotation = Quaternion.Euler(targetRotationX, 0f, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
    }
}
