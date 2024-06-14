using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public RockSpawner rockSpawner;       // Aseg�rate de asignar estos en el inspector de Unity
    public SpaceShipSpawner spaceShipSpawner;
    public BossSpawner bossSpawner;

    public enum GameStage
    {
        SpawningRocks,
        SpawningSpaceships,
        SpawningBoss,
        RestPeriod  // A�adido para manejar el per�odo de descanso
    }

    public GameStage currentStage = GameStage.SpawningRocks;
    public float timeToNextStage = 60f; // Duraci�n de cada etapa en segundos
    public float restPeriodDuration = 10f; // Duraci�n del per�odo de descanso en segundos
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
        // Desactiva los spawners aqu� para detener el spawn de enemigos
        rockSpawner.StopSpawning();
        spaceShipSpawner.StopSpawning();
        bossSpawner.StopSpawning();
        Debug.Log("Iniciando per�odo de descanso");
    }

    void EndRestPeriod()
    {
        isInRestPeriod = false;
        AdvanceStage();
        stageTimer = timeToNextStage; // Reinicia el contador para la pr�xima etapa
        Debug.Log("Finalizando per�odo de descanso");
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

    // M�todo agregado para manejar la destrucci�n de la fortaleza volante
    public void FlyingFortressDestroyed()
    {
        Debug.Log("Flying Fortress Destroyed");
        // Aqu� puedes poner la l�gica que necesites cuando la fortaleza es destruida
        // Por ejemplo, avanzar a la siguiente etapa o finalizar el juego
    }

    // M�todo para finalizar la partida y guardar las estad�sticas
    public void FinalizarPartida()
    {
        if (gameController != null)
        {
            gameController.FinalizarPartida();
        }
    }
}
