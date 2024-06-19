// Este script gestiona las estadísticas del juego, como la puntuación, los disparos realizados y acertados, el daño infligido y recibido.
// También permite guardar estas estadísticas y enviarlas a un servidor.

using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private int totalShots = 0;
    private int shotsHit = 0;
    private int damageDealt = 0;
    private int damageTaken = 0;
    private float startTime;
    private string mapName = "Map 1"; // Nombre del mapa.

    public TMP_Text scoreText;

    // Método llamado al inicio del juego.
    void Start()
    {
        Debug.Log("ScoreManager Start");
        ResetScore();
        startTime = Time.time;
    }

    // Método para reiniciar las estadísticas de puntuación.
    public void ResetScore()
    {
        Debug.Log("ResetScore called");
        score = 0;
        totalShots = 0;
        shotsHit = 0;
        damageDealt = 0;
        damageTaken = 0;
        startTime = Time.time;

        UpdateScoreText();
    }

    // Método llamado cuando el objeto es habilitado.
    void OnEnable()
    {
        Debug.Log("ScoreManager OnEnable");
        UpdateScoreText();
    }

    // Método para añadir puntos a la puntuación actual.
    public void AddScore(int amount)
    {
        Debug.Log($"AddScore called with amount: {amount}");
        score += amount;
        UpdateScoreText();
    }

    // Método para actualizar el texto con la puntuación actual.
    private void UpdateScoreText()
    {
        Debug.Log($"UpdateScoreText called with score: {score}");
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    // Método para registrar un disparo realizado.
    public void RegisterShot()
    {
        totalShots++;
    }

    // Método para registrar un disparo acertado.
    public void RegisterHit()
    {
        shotsHit++;
    }

    // Método para registrar el daño infligido.
    public void RegisterDamageDealt(int amount)
    {
        damageDealt += amount;
    }

    // Método para registrar el daño recibido.
    public void RegisterDamageTaken(int amount)
    {
        damageTaken += amount;
    }

    // Método para guardar las estadísticas del juego localmente y enviarlas al servidor.
    public void SaveGameStatistics()
    {
        Debug.Log("SaveGameStatistics called");
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

    // Corrutina para enviar las estadísticas al servidor.
    private IEnumerator SendStatisticsToServer(string json, string username, string password)
    {
        Debug.Log("SendStatisticsToServer called");
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

    // Método para obtener la puntuación actual.
    public int GetScore()
    {
        return score;
    }
}
