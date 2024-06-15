using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShootEnemy : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala enemiga
    public float bulletSpeed = 10f; // Velocidad de la bala
    public AudioClip shootSound; // Sonido de disparo
    public float shootInterval = 2f; // Intervalo de tiempo entre disparos
    private AudioSource audioSource_SFX_SHOOT; // Fuente de audio para el sonido de disparo
    private float shootTimer = 0f;

    void Start()
    {
        // Asignar la fuente de audio para el sonido de disparo
        audioSource_SFX_SHOOT = GameObject.Find("SFX_SHOOT").GetComponent<AudioSource>();

        if (audioSource_SFX_SHOOT == null)
        {
            Debug.LogError("No se encontró el componente AudioSource en SFX_SHOOT. Asegúrate de que el objeto existe y tiene un AudioSource.");
        }
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        if (shootSound != null && audioSource_SFX_SHOOT != null)
        {
            audioSource_SFX_SHOOT.PlayOneShot(shootSound);
        }

        Transform firePoint = transform.Find("PuntoDisparoEnemigo");
        if (firePoint == null)
        {
            Debug.LogError("El punto de disparo no está asignado correctamente.");
            return;
        }
        Vector3 firePointPosition = firePoint.position;

        GameObject bullet = BulletPool.Instance.GetEnemyBullet();
        if (bullet == null)
        {
            Debug.LogError("No se pudo obtener una bala del pool. Asegúrate de que el BulletPool está configurado correctamente.");
            return;
        }

        bullet.transform.position = firePointPosition;
        bullet.transform.rotation = Quaternion.identity;
        bullet.tag = "BulletEnemy";

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No se encontró Rigidbody2D en la bala enemiga. Asegúrate de que todas las balas enemigas tienen un Rigidbody2D.");
            return;
        }

        rb.AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
