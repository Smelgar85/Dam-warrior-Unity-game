/**
 * ScoreManager.cs
 * Este script gestiona la puntuación del juego, registra estadísticas de los disparos y daños,
 * y guarda estas estadísticas tanto localmente como en un servidor remoto.
 */

using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int score = 0;
    private int totalShots = 0;
    private int shotsHit = 0;
    private int damageDealt = 0;
    private int damageTaken = 0;
    private float startTime;
    private string mapName = "Map 1"; // Nombre del mapa.

    public TMP_Text scoreText;

    void Awake()
    {
        // Inicializa la instancia singleton y asegura que no se destruya al cargar una nueva escena.
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
        // Inicializa la puntuación y el tiempo de inicio.
        UpdateScoreText();
        startTime = Time.time;
    }

    public void AddScore(int amount)
    {
        // Añade puntos a la puntuación actual y actualiza el texto de la puntuación.
        score += amount;
        UpdateScoreText();
    }

    public void RegisterShot()
    {
        // Registra un disparo realizado.
        totalShots++;
    }

    public void RegisterHit()
    {
        // Registra un disparo acertado.
        shotsHit++;
    }

    public void RegisterDamageDealt(int amount)
    {
        // Registra el daño causado.
        damageDealt += amount;
    }

    public void RegisterDamageTaken(int amount)
    {
        // Registra el daño recibido.
        damageTaken += amount;
    }

    public void SaveGameStatistics()
    {
        // Guarda las estadísticas del juego en PlayerPrefs y las envía al servidor si el usuario está logueado.
        GameStatistics stats = new GameStatistics(
            DateTime.Now,
            mapName,
            score,
            totalShots > 0 ? (float)shotsHit / totalShots : 0,
            Time.time - startTime,
            damageDealt,
            damageTaken
        );

        string json = JsonUtility.ToJson(stats);
        PlayerPrefs.SetString("ultimaPartida", json);
        PlayerPrefs.Save();

        string username = PlayerPrefs.GetString("username", "guest");
        if (username != "guest")
        {
            StartCoroutine(SendStatisticsToServer(json, username, PlayerPrefs.GetString("password")));
        }
    }

    private void UpdateScoreText()
    {
        // Actualiza el texto de la puntuación en la interfaz.
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    private IEnumerator SendStatisticsToServer(string json, string username, string password)
    {
        // Envía las estadísticas del juego al servidor.
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("stats", json);

        using (UnityWebRequest www = UnityWebRequest.Post("http://smelgar85.eu.pythonanywhere.com/guardar_estadisticas", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al enviar las estadísticas: " + www.error);
            }
            else
            {
                Debug.Log("Estadísticas enviadas correctamente");
            }
        }
    }

    public int GetScore()
    {
        // Retorna la puntuación actual.
        return score;
    }
}
