using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;
    public float speed = 5f;
    public float rotationSpeed = 10f;
    public float zigzagAmplitude = 2f;
    public float zigzagFrequency = 2f;

    public AudioClip breakSound;
    public AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        transform.Rotate(0f, Random.Range(0f, 360f), 0f);
    }

    void Update()
    {
        // Movimiento de izquierda a derecha
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        // Movimiento de zigzag en el eje Y
        float zigzagMovement = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        transform.Translate(Vector3.up * zigzagMovement * Time.deltaTime, Space.World);

        // Rotación similar a las rocas
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // Ajusta el valor de la posición x para cambiar el punto de destrucción
        if (transform.position.x < -20f || currentHealth <= 0)
        {
            PlayBreakSound();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            PlayBreakSound();
            Destroy(gameObject);
        }
    }

    private void PlayBreakSound()
    {
        if (breakSound != null)
        {
            audioSource.clip = breakSound;
            audioSource.Play();
        }
    }
}
