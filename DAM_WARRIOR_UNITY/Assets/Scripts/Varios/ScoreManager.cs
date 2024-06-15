using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int score = 0;
    private int totalShots = 0;
    private int shotsHit = 0;
    private int damageDealt = 0;
    private int damageTaken = 0;
    private float startTime;

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
            "Map 1",
            score,
            totalShots > 0 ? (float)shotsHit / totalShots : 0,
            Time.time - startTime,
            damageDealt,
            damageTaken
        );

        string json = JsonUtility.ToJson(stats);
        PlayerPrefs.SetString("ultimaPartida", json);
        PlayerPrefs.Save();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public int GetScore()
    {
        return score;
    }
}
