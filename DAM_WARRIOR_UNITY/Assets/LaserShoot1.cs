using UnityEngine;

public class LaserShoot1 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    private AudioSource audioSource; // Referencia al componente AudioSource del GameObject "SFX_SHOOT"
    public AudioClip shootSound; // AudioClip para el sonido de disparo
    public GameObject specialBulletPrefab; // Prefab para el disparo especial
    public AudioClip specialShootSound; // Sonido de disparo especial
    private Transform escalador; // Referencia al objeto de la barra de poder
    private bool isShooting = false; // Para controlar si se está realizando un disparo especial

    void Start()
    {
        // Obtener la referencia al GameObject "Escalador"
        escalador = GameObject.Find("Escalador").transform;
        // Obtener el componente AudioSource del GameObject "SFX"
        audioSource = GameObject.Find("SFX_SHOOT").GetComponent<AudioSource>();
    }

    void Update()
    {
        // Disparo normal con el botón de salto (o el que tengas configurado para disparar)
        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }

        // Disparo especial cuando fullPower es true y no se está realizando un disparo especial
        if (Input.GetButtonDown("Fire1") && GameManager.fullPower && !isShooting)
        {
            ShootSpecial(); // Disparar el proyectil especial
        }
    }

    void Shoot()
    {
        // Reproducir el sonido de disparo si está configurado
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        Vector3 firePointPosition = transform.Find("PuntoDisparo").position;

        // Crea el proyectil
        GameObject bullet = Instantiate(bulletPrefab, firePointPosition, Quaternion.identity);

        // Aplica la fuerza al proyectil
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }

    void ShootSpecial()
    {
        // Reproducir el sonido del disparo especial si está configurado
        if (specialShootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(specialShootSound);
        }

        // Restablecer la escala del escalador solo en el eje X al valor por defecto
        if (escalador != null)
        {
            escalador.localScale = new Vector3(0f, escalador.localScale.y, escalador.localScale.z);
        }
        // Restablecer fullPower a false
        GameManager.fullPower = false;

        // Marcar que se está realizando un disparo especial
        isShooting = true;

        Vector3 firePointPosition = transform.Find("PuntoDisparo").position;

        // Crea el proyectil especial
        GameObject specialBullet = Instantiate(specialBulletPrefab, firePointPosition, Quaternion.identity);
        Rigidbody2D rb = specialBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletSpeed*3, ForceMode2D.Impulse);

        // Marcar que se ha completado el disparo especial después de un breve retraso
        // Esto es para evitar que se realicen múltiples disparos especiales al mantener presionado el botón
        Invoke("ResetIsShooting", 0.5f);
    }

    // Método para restablecer isShooting después de disparar el proyectil especial
    void ResetIsShooting()
    {
        isShooting = false;
    }
}
