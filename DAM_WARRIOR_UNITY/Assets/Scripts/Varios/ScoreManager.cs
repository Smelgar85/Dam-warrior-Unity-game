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
    private string mapName = "Map 1"; // Nombre del mapa

    public TMP_Text scoreText;

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
        UpdateScoreText();
        startTime = Time.time;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void RegisterShot()
    {
        totalShots++;
    }

    public void RegisterHit()
    {
        shotsHit++;
    }

    public void RegisterDamageDealt(int amount)
    {
        damageDealt += amount;
    }

    public void RegisterDamageTaken(int amount)
    {
        damageTaken += amount;
    }

    public void SaveGameStatistics()
    {
        GameStatistics stats = new GameStatistics(
            DateTime.Now,
            mapName, // Incluir el nombre del mapa
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
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    private IEnumerator SendStatisticsToServer(string json, string username, string password)
    {
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
        return score;
    }
}
