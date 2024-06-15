using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject playerBulletPrefab;
    public GameObject enemyBulletPrefab;
    public GameObject specialBulletPrefab; // Prefab para la bala especial

    private Queue<GameObject> playerBullets = new Queue<GameObject>();
    private Queue<GameObject> enemyBullets = new Queue<GameObject>();
    private Queue<GameObject> specialBullets = new Queue<GameObject>(); // Pool para balas especiales

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetPlayerBullet()
    {
        if (playerBullets.Count > 0)
        {
            GameObject bullet = playerBullets.Dequeue();
            bullet.SetActive(true);
            bullet.tag = "Bullet";
            return bullet;
        }
        else
        {
            GameObject newBullet = Instantiate(playerBulletPrefab);
            newBullet.tag = "Bullet";
            return newBullet;
        }
    }

    public void ReturnPlayerBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        playerBullets.Enqueue(bullet);
    }

    public GameObject GetEnemyBullet()
    {
        if (enemyBullets.Count > 0)
        {
            GameObject bullet = enemyBullets.Dequeue();
            bullet.SetActive(true);
            bullet.tag = "BulletEnemy";
            return bullet;
        }
        else
        {
            GameObject newBullet = Instantiate(enemyBulletPrefab);
            newBullet.tag = "BulletEnemy";
            return newBullet;
        }
    }

    public void ReturnEnemyBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        enemyBullets.Enqueue(bullet);
    }

    public GameObject GetSpecialBullet()
    {
        if (specialBullets.Count > 0)
        {
            GameObject bullet = specialBullets.Dequeue();
            bullet.SetActive(true);
            bullet.tag = "Bullet";
            return bullet;
        }
        else
        {
            GameObject newBullet = Instantiate(specialBulletPrefab);
            newBullet.tag = "Bullet";
            return newBullet;
        }
    }

    public void ReturnSpecialBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        specialBullets.Enqueue(bullet);
    }
}
