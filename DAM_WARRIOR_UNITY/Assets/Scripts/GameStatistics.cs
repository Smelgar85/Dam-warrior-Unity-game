using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    public static GameStatistics Instance { get; private set; }

    private int totalShotsFired;
    private int totalHits;
    private float totalDamageDealt;
    private float totalDamageReceived;
    private float startTime;
    private float endTime;

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

    public void StartGame()
    {
        startTime = Time.time;
        ResetStatistics();
    }

    public void EndGame()
    {
        endTime = Time.time;
        DisplayStatistics();
    }

    private void ResetStatistics()
    {
        totalShotsFired = 0;
        totalHits = 0;
        totalDamageDealt = 0;
        totalDamageReceived = 0;
    }

    public void RegisterShotFired()
    {
        totalShotsFired++;
    }

    public void RegisterHit()
    {
        totalHits++;
    }

    public void RegisterDamageDealt(float damage)
    {
        totalDamageDealt += damage;
    }

    public void RegisterDamageReceived(float damage)
    {
        totalDamageReceived += damage;
    }

    private void DisplayStatistics()
    {
        float gameDuration = endTime - startTime;
        float accuracy = (totalShotsFired > 0) ? (float)totalHits / totalShotsFired * 100 : 0;

        Debug.Log($"Game Duration: {gameDuration} seconds");
        Debug.Log($"Total Damage Dealt: {totalDamageDealt}");
        Debug.Log($"Total Damage Received: {totalDamageReceived}");
        Debug.Log($"Accuracy: {accuracy}%");
    }
}
