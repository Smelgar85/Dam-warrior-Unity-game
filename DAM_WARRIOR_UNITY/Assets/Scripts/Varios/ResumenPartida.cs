/**
 * ResumenPartida.cs
 * Este script muestra el resumen de la partida, incluyendo estadísticas como la puntuación, precisión,
 * tiempo completado, daño causado y recibido. También maneja la transición a la escena de créditos.
 */

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResumenPartida : MonoBehaviour
{
    public TMP_Text fechaText;
    public TMP_Text nombreMapaText;
    public TMP_Text puntuacionText;
    public TMP_Text precisionText;
    public TMP_Text tiempoCompletadoText;
    public TMP_Text danoCausadoText;
    public TMP_Text danoRecibidoText;

    void Start()
    {
        // Obtiene y muestra las estadísticas de la última partida desde PlayerPrefs.
        string json = PlayerPrefs.GetString("ultimaPartida", "{}");
        GameStatistics estadisticas = JsonUtility.FromJson<GameStatistics>(json);

        fechaText.text = "Date: " + estadisticas.fecha;
        nombreMapaText.text = "Map: " + estadisticas.nombreMapa;
        puntuacionText.text = "Score: " + estadisticas.puntuacion;
        precisionText.text = "Accuracy: " + (estadisticas.precision * 100).ToString("F2") + "%";
        tiempoCompletadoText.text = "Time: " + estadisticas.tiempoCompletado.ToString("F2") + "s";
        danoCausadoText.text = "Damage Done: " + estadisticas.danoCausado;
        danoRecibidoText.text = "Damage Taken: " + estadisticas.danoRecibido;
    }

    public void OnNextButtonClicked()
    {
        // Carga la escena de créditos.
        SceneManager.LoadScene("Creditos");
    }
}
