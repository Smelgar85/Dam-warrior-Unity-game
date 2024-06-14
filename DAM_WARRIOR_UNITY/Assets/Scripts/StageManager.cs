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

    private GameController gameController; // Referencia al controlador del juego

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
        FindGameController(); // Inicializar GameController
        UpdateStageSettings();
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
        rockSpawner.StopSpawning();
        spaceShipSpawner.StopSpawning();
        bossSpawner.StopSpawning();
        Debug.Log("Iniciando período de descanso");
    }

    void EndRestPeriod()
    {
        isInRestPeriod = false;
        AdvanceStage();
        stageTimer = timeToNextStage; // Reinicia el contador para la próxima etapa
        Debug.Log("Finalizando período de descanso");
    }

    void AdvanceStage()
    {
        Debug.Log($"Avanzando etapa desde {currentStage}");
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
        Debug.Log($"Nueva etapa: {currentStage}");
    }

    void UpdateStageSettings()
    {
        Debug.Log($"Configurando etapa: {currentStage}");
        switch (currentStage)
        {
            case GameStage.SpawningRocks:
                rockSpawner.StartSpawning();
                spaceShipSpawner.StopSpawning();
                bossSpawner.StopSpawning();
                Debug.Log("Configurado para SpawningRocks");
                break;
            case GameStage.SpawningSpaceships:
                rockSpawner.StopSpawning();
                spaceShipSpawner.StartSpawning();
                bossSpawner.StopSpawning();
                Debug.Log("Configurado para SpawningSpaceships");
                break;
            case GameStage.SpawningBoss:
                rockSpawner.StopSpawning();
                spaceShipSpawner.StopSpawning();
                bossSpawner.StartSpawning();
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

    // Método agregado para manejar la destrucción de la fortaleza volante
    public void FlyingFortressDestroyed()
    {
        Debug.Log("Flying Fortress Destroyed");
        // Aquí puedes poner la lógica que necesites cuando la fortaleza es destruida
        // Por ejemplo, avanzar a la siguiente etapa o finalizar el juego
    }

    // Método para finalizar la partida y guardar las estadísticas
    public void FinalizarPartida()
    {
        if (gameController != null)
        {
            gameController.FinalizarPartida();
        }
    }
}
