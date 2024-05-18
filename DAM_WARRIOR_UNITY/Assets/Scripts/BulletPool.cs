using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    public GameObject bulletPrefab;
    private Queue<GameObject> bullets = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetBullet()
    {
        if (bullets.Count > 0)
        {
            GameObject bullet = bullets.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            return Instantiate(bulletPrefab);
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullets.Enqueue(bullet);
    }


    private Queue<GameObject> specialBullets = new Queue<GameObject>();
    public GameObject specialBulletPrefab; // Prefab para el proyectil especial

    public GameObject GetSpecialBullet()
    {
        if (specialBullets.Count > 0)
        {
            GameObject bullet = specialBullets.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            return Instantiate(specialBulletPrefab);
        }
    }

    public void ReturnSpecialBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        specialBullets.Enqueue(bullet);
    }


}
