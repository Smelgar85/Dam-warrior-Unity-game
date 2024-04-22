using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int health = 2; // Vida de la nave enemiga
    public GameObject deathEffectPrefabBoss; // Prefab de la animaci�n de muerte
    private AudioSource explosionAudioSourceBoss; // Referencia al componente AudioSource del sonido de explosi�n
    public AudioClip dieSoundBoss;

    void Start()
    {
        // Obtener el componente AudioSource del GameObject "SFX_DEATH_ENEMY"
        explosionAudioSourceBoss = GameObject.Find("SFX_DEATH_ENEMY").GetComponent<AudioSource>();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount; // Reduce la salud por el da�o recibido

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathEffectPrefabBoss != null)
        {
            Instantiate(deathEffectPrefabBoss, transform.position, Quaternion.identity);
        }

        if (explosionAudioSourceBoss != null)
        {
            explosionAudioSourceBoss.PlayOneShot(dieSoundBoss);
        }

        // Llama al m�todo LoadFirstSceneAfterDelay del StageManager solo despu�s de la muerte del jefe
        StageManager stageManager = FindObjectOfType<StageManager>();
        if (stageManager != null)
        {
            SceneManager.LoadScene(0);
            //stageManager.StartCoroutine(stageManager.LoadFirstSceneAfterDelay(5));
        }

       
    }

}
