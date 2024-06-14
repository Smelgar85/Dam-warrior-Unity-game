using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int health = 2;
    public GameObject deathEffectPrefabBoss;
    private AudioSource explosionAudioSourceBoss;
    public AudioClip dieSoundBoss;
    private GameController gameController; // Referencia al controlador del juego

    void Start()
    {
        explosionAudioSourceBoss = GameObject.Find("SFX_DEATH_ENEMY").GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>(); // Buscar el controlador del juego en la escena
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (gameController != null)
        {
            gameController.RegistrarDanoRecibido(damageAmount); // Registrar daño recibido
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayDeathEffect();
        PlayDeathSound();

        if (gameController != null)
        {
            gameController.FinalizarPartida(); // Finalizar la partida cuando el jefe muere
        }

        NotifyDeath();
    }

    private void PlayDeathEffect()
    {
        if (deathEffectPrefabBoss != null)
        {
            Instantiate(deathEffectPrefabBoss, transform.position, Quaternion.identity);
        }
    }

    private void PlayDeathSound()
    {
        if (explosionAudioSourceBoss != null && dieSoundBoss != null)
        {
            explosionAudioSourceBoss.PlayOneShot(dieSoundBoss);
        }
    }

    private void NotifyDeath()
    {
        StageManager stageManager = FindObjectOfType<StageManager>();
        if (stageManager != null)
        {
            stageManager.FlyingFortressDestroyed();
        }
    }
}
