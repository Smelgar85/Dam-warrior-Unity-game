/**
 * StageManager.cs
 * Este script maneja multitud de funciones. Principalmente sirve para gestionar el paso de las distintas etapas de un mapa
 Pero ha ido engordando con otros métodos como el sistema de pausa, o el finalizar la escena una vez el boss muere.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public ScoreManager scoreManager; 
    public RockSpawner rockSpawner;
    public SpaceShipSpawner spaceShipSpawner;
    public BossSpawner bossSpawner;
    public AudioSource musicAudioSource; 
    public GameObject canvasPause; 

    public enum GameStage
    {
        SpawningRocks,
        SpawningSpaceships,
        SpawningBoss,
        RestPeriod
    }

    public GameStage currentStage = GameStage.SpawningRocks;
    public float timeToNextStage = 60f;
    public float restPeriodDuration = 10f;
    private float stageTimer;
    private bool isInRestPeriod = false;
    private bool juegoPausado = false;

    void Start()
    {
        Debug.Log("StageManager Start called");

        // Reinicia el sistema de puntuación al comenzar la escena
        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }

        // Reinicia el sistema de spawneo al comenzar la escena
        ResetStageManager();

        if (canvasPause != null)
        {
            canvasPause.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar si se presiona la tecla ESCAPE en el teclado o START en el gamepad para pausar o reanudar el juego
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            if (juegoPausado)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (juegoPausado)
        {
            return; // No hacer nada si el juego está pausado...
        }

        // Contador de tiempo para avanzar las etapas del juego
        stageTimer -= Time.deltaTime;
        if (stageTimer <= 0)
        {
            if (isInRestPeriod)
            {
                EndRestPeriod();
            }
            else
            {
                StartRestPeriod();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Pausar el tiempo en el juego
        juegoPausado = true;

        // Pausar la música si existe un AudioSource asociado
        if (musicAudioSource != null && musicAudioSource.isPlaying)
        {
            musicAudioSource.Pause();
        }

        // Activar el canvas de pausa
        if (canvasPause != null)
        {
            canvasPause.SetActive(true);
        }

        Debug.Log("Juego pausado");
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Reanudar el tiempo en el juego
        juegoPausado = false;

        // Reanudar la música si existe un AudioSource asociado
        if (musicAudioSource != null && !musicAudioSource.isPlaying)
        {
            musicAudioSource.Play();
        }

        // Desactivar el canvas de pausa
        if (canvasPause != null)
        {
            canvasPause.SetActive(false);
        }

        Debug.Log("Juego reanudado");
    }

    void StartRestPeriod()
    {
        isInRestPeriod = true;
        stageTimer = restPeriodDuration;
        StopAllSpawners();
        Debug.Log("Iniciando período de descanso");
    }

    void EndRestPeriod()
    {
        isInRestPeriod = false;
        AdvanceStage();
        stageTimer = timeToNextStage;
        Debug.Log("Finalizando período de descanso");
    }

    void AdvanceStage()
    {
        Debug.Log($"Avanzando etapa desde {currentStage}");
        if (currentStage == GameStage.SpawningRocks)
        {
            currentStage = GameStage.SpawningSpaceships;
            StopSpawner(rockSpawner);
            StartSpawner(spaceShipSpawner);
        }
        else if (currentStage == GameStage.SpawningSpaceships)
        {
            currentStage = GameStage.SpawningBoss;
            StopSpawner(spaceShipSpawner);
            StartSpawner(bossSpawner);
        }
        else if (currentStage == GameStage.SpawningBoss)
        {
            StopAllSpawners(); // Cambio para evitar reiniciar el ciclo después del jefe.
        }
        UpdateStageSettings();
        Debug.Log($"Nueva etapa: {currentStage}");
    }

    void UpdateStageSettings()
    {
        Debug.Log($"Configurando etapa: {currentStage}");
        StopAllSpawners();
        switch (currentStage)
        {
            case GameStage.SpawningRocks:
                StartSpawner(rockSpawner);
                Debug.Log("Configurado para SpawningRocks");
                break;
            case GameStage.SpawningSpaceships:
                StartSpawner(spaceShipSpawner);
                Debug.Log("Configurado para SpawningSpaceships");
                break;
            case GameStage.SpawningBoss:
                StartSpawner(bossSpawner);
                Debug.Log("Configurado para SpawningBoss");
                break;
        }
    }

    public void FlyingFortressDestroyed()
    {
        Debug.Log("Flying Fortress Destroyed");
        StopAllSpawners();

        // Llamar a SaveGameStatistics del ScoreManager
        if (scoreManager != null)
        {
            scoreManager.SaveGameStatistics();
        }
        else
        {
            Debug.LogError("ScoreManager no encontrado.");
        }

        StartCoroutine(LoadSummaryScene());
    }

    IEnumerator LoadSummaryScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("ResumenPartida");
    }

    void StopAllSpawners()
    {
        StopSpawner(rockSpawner);
        StopSpawner(spaceShipSpawner);
        StopSpawner(bossSpawner);
    }

    void StopSpawner(MonoBehaviour spawner)
    {
        if (spawner != null)
        {
            spawner.Invoke("StopSpawning", 0);
        }
    }

    void StartSpawner(MonoBehaviour spawner)
    {
        if (spawner != null)
        {
            spawner.Invoke("StartSpawning", 0);
        }
    }

    public void ResetStageManager()
    {
        Debug.Log("Reseteando StageManager");
        currentStage = GameStage.SpawningRocks;
        stageTimer = timeToNextStage;
        isInRestPeriod = false;

        // Reiniciar todos los spawners correctamente
        StartSpawner(rockSpawner);
        StopSpawner(spaceShipSpawner);
        StopSpawner(bossSpawner);

        // Asegurar que el tiempo no esté pausado al inicio
        Time.timeScale = 1f;

        // Desactivar el canvas de pausa si está activado
        if (canvasPause != null && canvasPause.activeSelf)
        {
            canvasPause.SetActive(false);
        }
    }
}
