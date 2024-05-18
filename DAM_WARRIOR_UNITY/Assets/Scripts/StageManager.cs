using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public RockSpawner rockSpawner;       // Asegúrate de asignar estos en el inspector de Unity
    public SpaceShipSpawner spaceShipSpawner;
    public BossSpawner bossSpawner;

    public enum GameStage
    {
        SpawningRocks,
        SpawningSpaceships,
        SpawningBoss,
        RestPeriod  // Añadido para manejar el período de descanso
    }

    public GameStage currentStage = GameStage.SpawningRocks;
    public float timeToNextStage = 60f; // Duración de cada etapa en segundos
    public float restPeriodDuration = 10f; // Duración del período de descanso en segundos
    private float stageTimer;
    private bool isInRestPeriod = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        stageTimer = timeToNextStage;
        UpdateStageSettings();
        GameStatistics.Instance.StartGame();
    }

    void Update()
    {
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

    void StartRestPeriod()
    {
        isInRestPeriod = true;
        stageTimer = restPeriodDuration; // Establecer el tiempo de descanso
        // Desactiva los spawners aquí para detener el spawn de enemigos
        rockSpawner.enabled = false;
        spaceShipSpawner.enabled = false;
        bossSpawner.enabled = false;
    }

    void EndRestPeriod()
    {
        isInRestPeriod = false;
        AdvanceStage();
        stageTimer = timeToNextStage; // Reinicia el contador para la próxima etapa
    }

    void AdvanceStage()
    {
        if (currentStage == GameStage.SpawningRocks)
        {
            currentStage = GameStage.SpawningSpaceships;
            rockSpawner.StopSpawning();
            spaceShipSpawner.StartSpawning();
        }
        else if (currentStage == GameStage.SpawningSpaceships)
        {
            currentStage = GameStage.SpawningBoss;
            spaceShipSpawner.StopSpawning();
            bossSpawner.StartSpawning();
        }
        else if (currentStage == GameStage.SpawningBoss)
        {
            currentStage = GameStage.SpawningRocks; // Reinicia el ciclo
            bossSpawner.StopSpawning();
            rockSpawner.StartSpawning();
        }
        UpdateStageSettings();
    }

    void UpdateStageSettings()
    {
        switch (currentStage)
        {
            case GameStage.SpawningRocks:
                rockSpawner.enabled = true;
                spaceShipSpawner.enabled = false;
                bossSpawner.enabled = false;
                break;
            case GameStage.SpawningSpaceships:
                rockSpawner.enabled = false;
                spaceShipSpawner.enabled = true;
                bossSpawner.enabled = false;
                break;
            case GameStage.SpawningBoss:
                rockSpawner.enabled = false;
                spaceShipSpawner.enabled = false;
                bossSpawner.enabled = true;
                break;
        }
    }
}
