using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShootBoss : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab del proyectil
    public float bulletSpeed = 10f; // Velocidad del proyectil
    public AudioClip shootSound; // Sonido de disparo
    public float shootInterval = 2f; // Intervalo entre disparos
    public Transform[] firePoints; // Puntos de disparo
    private AudioSource audioSource_SFX_SHOOT; // Referencia al componente AudioSource
    private float shootTimer = 0f; // Temporizador para controlar el intervalo entre disparos

    void Start()
    {
        // Obtener el componente AudioSource del GameObject "SFX_SHOOT"
        audioSource_SFX_SHOOT = GameObject.Find("SFX_SHOOT").GetComponent<AudioSource>();
    }

    void Update()
    {
        // Incrementa el temporizador
        shootTimer += Time.deltaTime;

        // Si el temporizador alcanza el intervalo deseado
        if (shootTimer >= shootInterval)
        {
            // Dispara desde un punto aleatorio
            Shoot();

            // Reinicia el temporizador
            shootTimer = 0f;
        }
    }

    // Método para realizar un disparo
    void Shoot()
    {
        // Reproduce el sonido de disparo si está configurado y hay un componente AudioSource
        if (shootSound != null && audioSource_SFX_SHOOT != null)
        {
            audioSource_SFX_SHOOT.PlayOneShot(shootSound);
        }

        // Obtiene un punto de disparo aleatorio
        Transform randomFirePoint = firePoints[Random.Range(0, firePoints.Length)];
        Vector3 firePointPosition = randomFirePoint.position;

        // Crea el proyectil en el punto de origen del disparo
        GameObject bullet = Instantiate(bulletPrefab, firePointPosition, Quaternion.identity);

        // Aplica la velocidad al proyectil
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-randomFirePoint.right * bulletSpeed, ForceMode2D.Impulse); // Disparo hacia la izquierda
    }
}
