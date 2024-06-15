using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public RockSpawner rockSpawner;
    public SpaceShipSpawner spaceShipSpawner;
    public BossSpawner bossSpawner;

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

    private GameController gameController;

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
        FindGameController();
        UpdateStageSettings();
    }

    void Update()
    {
        // Verificar si la escena activa es "ResumenPartida"
        if (SceneManager.GetActiveScene().name == "ResumenPartida")
        {
            return; // Detener la ejecución del Update si estamos en "ResumenPartida"
        }

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
            // Cambio para evitar reiniciar el ciclo después del jefe
            StopAllSpawners();
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

    void FindGameController()
    {
        gameController = FindObjectOfType<GameController>();
        if (gameController == null)
        {
            Debug.LogWarning("GameController no encontrado en la escena.");
        }
        else
        {
            Debug.Log("GameController encontrado.");
        }
    }

    public void FlyingFortressDestroyed()
    {
        Debug.Log("Flying Fortress Destroyed");
        StopAllSpawners(); // Detener todos los spawners
        ScoreManager.Instance.SaveGameStatistics();
        StartCoroutine(LoadSummaryScene());
    }

    IEnumerator LoadSummaryScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("ResumenPartida");
    }

    public void FinalizarPartida()
    {
        if (gameController != null)
        {
            gameController.FinalizarPartida();
        }
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
}
