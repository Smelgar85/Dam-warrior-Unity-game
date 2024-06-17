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
        // Configura la instancia singleton y asegura que no se destruya al cargar una nueva escena.
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
        // Inicializa el temporizador de la etapa y busca el GameController.
        stageTimer = timeToNextStage;
        FindGameController();
        UpdateStageSettings();
    }

    void Update()
    {
        // Verifica si la escena activa es "ResumenPartida" y detiene la actualización si es así.
        if (SceneManager.GetActiveScene().name == "ResumenPartida")
        {
            return;
        }

        // Actualiza el temporizador de la etapa y cambia de etapa si es necesario.
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
        // Inicia un período de descanso.
        isInRestPeriod = true;
        stageTimer = restPeriodDuration;
        StopAllSpawners();
        Debug.Log("Iniciando período de descanso");
    }

    void EndRestPeriod()
    {
        // Termina el período de descanso y avanza a la siguiente etapa.
        isInRestPeriod = false;
        AdvanceStage();
        stageTimer = timeToNextStage;
        Debug.Log("Finalizando período de descanso");
    }

    void AdvanceStage()
    {
        // Avanza a la siguiente etapa del juego y actualiza los spawners según la nueva etapa.
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
        // Configura los ajustes para la etapa actual y reinicia los spawners.
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
        // Busca y asigna el GameController en la escena.
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
        // Maneja la destrucción de la fortaleza voladora.
        Debug.Log("Flying Fortress Destroyed");
        StopAllSpawners();
        ScoreManager.Instance.SaveGameStatistics();
        StartCoroutine(LoadSummaryScene());
    }

    IEnumerator LoadSummaryScene()
    {
        // Carga la escena de resumen después de un retraso.
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("ResumenPartida");
    }

    public void FinalizarPartida()
    {
        // Llama a FinalizarPartida en el GameController.
        if (gameController != null)
        {
            gameController.FinalizarPartida();
        }
    }

    void StopAllSpawners()
    {
        // Detiene todos los spawners activos.
        StopSpawner(rockSpawner);
        StopSpawner(spaceShipSpawner);
        StopSpawner(bossSpawner);
    }

    void StopSpawner(MonoBehaviour spawner)
    {
        // Detiene un spawner específico.
        if (spawner != null)
        {
            spawner.Invoke("StopSpawning", 0);
        }
    }

    void StartSpawner(MonoBehaviour spawner)
    {
        // Inicia un spawner específico.
        if (spawner != null)
        {
            spawner.Invoke("StartSpawning", 0);
        }
    }
}
