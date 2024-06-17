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
        // Inicializa la salud y rota el objeto de forma aleatoria al inicio.
        currentHealth = maxHealth;
        transform.Rotate(0f, Random.Range(0f, 360f), 0f);
    }

    void Update()
    {
        // Mueve el objeto hacia la izquierda.
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        // Realiza un movimiento en zigzag en el eje Y.
        float zigzagMovement = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        transform.Translate(Vector3.up * zigzagMovement * Time.deltaTime, Space.World);

        // Rota el objeto alrededor del eje Y.
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // Destruye el objeto si sale de la pantalla o si su salud llega a 0.
        if (transform.position.x < -20f || currentHealth <= 0)
        {
            PlayBreakSound();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        // Reduce la salud del objeto cuando recibe daño.
        currentHealth -= damage;

        // Destruye el objeto si su salud llega a 0.
        if (currentHealth <= 0)
        {
            PlayBreakSound();
            Destroy(gameObject);
        }
    }

    private void PlayBreakSound()
    {
        // Reproduce un sonido cuando el objeto es destruido.
        if (breakSound != null)
        {
            audioSource.clip = breakSound;
            audioSource.Play();
        }
    }
}
