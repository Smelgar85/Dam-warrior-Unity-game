using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int health = 2;
    public GameObject deathEffectPrefabBoss;
    private AudioSource explosionAudioSourceBoss;
    public AudioClip dieSoundBoss;

    void Start()
    {
        explosionAudioSourceBoss = GameObject.Find("SFX_DEATH_ENEMY").GetComponent<AudioSource>();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        GameStatistics.Instance.RegisterDamageReceived(damageAmount);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayDeathEffect();
        PlayDeathSound();
        GameStatistics.Instance.EndGame();
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
