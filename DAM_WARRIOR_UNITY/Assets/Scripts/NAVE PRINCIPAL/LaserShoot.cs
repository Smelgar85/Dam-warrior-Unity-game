/**
 * LaserShoot.cs
 * Este script maneja el disparo de balas y balas especiales desde la nave.
 */

using UnityEngine;
using UnityEngine.InputSystem;

public class LaserShoot : MonoBehaviour
{
    public float bulletSpeed = 10f;
    private AudioSource audioSource;
    private AudioSource audioSource2;
    public AudioClip shootSound;
    public AudioClip specialShootSound;
    private Transform escalador;
    private bool isShooting = false;

    void Start()
    {
        // Inicializa las referencias a los componentes necesarios.
        escalador = GameObject.Find("Escalador").transform;
        audioSource = GameObject.Find("SFX_SHOOT").GetComponent<AudioSource>();
        audioSource2 = GameObject.Find("SFX_SHOOT2").GetComponent<AudioSource>();
    }

    void Update()
    {
        // Dispara una bala normal cuando se presiona el botón Jump.
        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }

        // Dispara una bala especial cuando se presiona el botón Fire1 y la barra de poder está llena.
        if (Input.GetButtonDown("Fire1") && GameManager.fullPower && !isShooting)
        {
            ShootSpecial();
        }
    }

    void Shoot()
    {
        // Reproduce el sonido de disparo.
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Encuentra el punto de disparo.
        Transform firePoint = transform.Find("PuntoDisparo");
        if (firePoint == null)
        {
            Debug.LogError("No se encontró el punto de disparo. Asegúrate de que el objeto 'PuntoDisparo' existe como hijo en la nave.");
            return;
        }

        // Obtiene una bala del pool de balas.
        GameObject bullet = BulletPool.Instance.GetPlayerBullet();
        if (bullet == null)
        {
            Debug.LogError("No se pudo obtener una bala del pool. Asegúrate de que el BulletPool está configurado correctamente.");
            return;
        }

        // Configura la posición y velocidad de la bala.
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.tag = "Bullet";

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No se encontró Rigidbody2D en la bala. Asegúrate de que todas las balas tienen un Rigidbody2D.");
            return;
        }
        rb.velocity = transform.right * bulletSpeed;
        ScoreManager.Instance.RegisterShot();
    }

    void ShootSpecial()
    {
        // Reproduce el sonido de disparo especial.
        if (specialShootSound != null && audioSource2 != null)
        {
            audioSource2.PlayOneShot(specialShootSound);
        }

        // Resetea la barra de poder y marca que está disparando.
        escalador.localScale = new Vector3(0f, escalador.localScale.y, escalador.localScale.z);
        GameManager.fullPower = false;
        isShooting = true;

        // Obtiene una bala especial del pool de balas.
        GameObject specialBullet = BulletPool.Instance.GetSpecialBullet();
        specialBullet.transform.position = transform.Find("PuntoDisparo").position;
        specialBullet.transform.rotation = Quaternion.identity;
        specialBullet.tag = "Bullet";

        Rigidbody2D rb = specialBullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed * 3;
        ScoreManager.Instance.RegisterShot();

        // Resetea la bandera de disparo después de un corto tiempo.
        Invoke("ResetIsShooting", 0.5f);
    }

    void ResetIsShooting()
    {
        // Permite disparar nuevamente.
        isShooting = false;
    }
}
