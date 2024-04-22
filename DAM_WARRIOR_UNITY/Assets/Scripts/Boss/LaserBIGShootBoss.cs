using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBIGShootBoss1 : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab del proyectil
    public float bulletSpeed = 10f; // Velocidad del proyectil
    public AudioClip shootSound; // Sonido de disparo
    public float shootInterval = 2f; // Intervalo entre disparos
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
            // Dispara
            Shoot();

            // Reinicia el temporizador
            shootTimer = 0f;
        }
    }

    // M�todo para realizar un disparo
    void Shoot()
    {
        // Reproduce el sonido de disparo si est� configurado y hay un componente AudioSource
        if (shootSound != null && audioSource_SFX_SHOOT != null)
        {
            audioSource_SFX_SHOOT.PlayOneShot(shootSound);
        }

        // Obtiene la posici�n del punto de disparo
        Transform firePoint = transform.Find("BossDisparo4BIG");
        if (firePoint == null)
        {
            Debug.LogError("El punto de disparo no est� asignado correctamente.");
            return;
        }
        Vector3 firePointPosition = firePoint.position;

        // Crea el proyectil en el punto de origen del disparo
        GameObject bullet = Instantiate(bulletPrefab, firePointPosition, Quaternion.identity);

        // Aplica la velocidad al proyectil
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse); // Disparo hacia la izquierda
    }
}
