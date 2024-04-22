using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public enum GameStage
    {
        SpawningRocks,
        SpawningSpaceships,
        SpawningBoss
    }

    public GameStage currentStage = GameStage.SpawningRocks;
    public float timeToNextStage = 60f; // Duración de cada etapa en segundos
    private float stageTimer;

    // Referencias a otros managers o controladores
    public RockSpawner rockSpawner;
    public SpaceShipSpawner spaceShipSpawner;
    public BossSpawner bossSpawner; // Nuevo spawner para el jefe

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Solo si necesitas que persista entre escenas, sino quita esta línea
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
    }

    void Update()
    {
        stageTimer -= Time.deltaTime;
        if (stageTimer <= 0)
        {
            AdvanceStage();
            stageTimer = timeToNextStage; // Reinicia el contador para la próxima etapa
        }
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

    public void FlyingFortressDestroyed()
    {
        StartCoroutine(LoadFirstSceneAfterDelay(10)); 
    }

    public IEnumerator LoadFirstSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }
}
