/**
 * BulletPool.cs
 * Este script gestiona un pool de balas para optimizar la creación y destrucción de balas en el juego.
 * Permite obtener y devolver balas de diferentes tipos (jugador, enemigo, especial).
 */

using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject playerBulletPrefab;
    public GameObject enemyBulletPrefab;
    public GameObject specialBulletPrefab; // Prefab para la bala especial.

    private Queue<GameObject> playerBullets = new Queue<GameObject>();
    private Queue<GameObject> enemyBullets = new Queue<GameObject>();
    private Queue<GameObject> specialBullets = new Queue<GameObject>(); // Pool para balas especiales.

    private void Awake()
    {
        // Inicializa la instancia singleton.
        Instance = this;
    }

    public GameObject GetPlayerBullet()
    {
        // Obtiene una bala de jugador del pool o crea una nueva si el pool está vacío.
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
        // Devuelve una bala de jugador al pool.
        bullet.SetActive(false);
        playerBullets.Enqueue(bullet);
    }

    public GameObject GetEnemyBullet()
    {
        // Obtiene una bala de enemigo del pool o crea una nueva si el pool está vacío.
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
        // Devuelve una bala de enemigo al pool.
        bullet.SetActive(false);
        enemyBullets.Enqueue(bullet);
    }

    public GameObject GetSpecialBullet()
    {
        // Obtiene una bala especial del pool o crea una nueva si el pool está vacío.
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
        // Devuelve una bala especial al pool.
        bullet.SetActive(false);
        specialBullets.Enqueue(bullet);
    }
}
