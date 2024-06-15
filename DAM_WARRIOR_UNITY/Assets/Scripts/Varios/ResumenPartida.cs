using UnityEngine;
using TMPro;

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
        string json = PlayerPrefs.GetString("ultimaPartida", "{}");
        GameStatistics estadisticas = JsonUtility.FromJson<GameStatistics>(json);

        fechaText.text = "Fecha: " + estadisticas.fecha;
        nombreMapaText.text = "Mapa: " + estadisticas.nombreMapa;
        puntuacionText.text = "Puntuaci�n: " + estadisticas.puntuacion;
        precisionText.text = "Precisi�n: " + (estadisticas.precision * 100).ToString("F2") + "%";
        tiempoCompletadoText.text = "Tiempo: " + estadisticas.tiempoCompletado.ToString("F2") + "s";
        danoCausadoText.text = "Da�o Causado: " + estadisticas.danoCausado;
        danoRecibidoText.text = "Da�o Recibido: " + estadisticas.danoRecibido;
    }
}
