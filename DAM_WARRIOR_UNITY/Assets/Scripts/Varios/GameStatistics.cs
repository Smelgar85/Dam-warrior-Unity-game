/**
 * GameStatistics.cs
 * Esta clase almacena las estadísticas de una partida, como la fecha, el nombre del mapa, la puntuación,
 * la precisión, el tiempo completado, el daño causado y el daño recibido.
 */

using System;

[Serializable]
public class GameStatistics
{
    public string fecha;
    public string nombreMapa;
    public int puntuacion;
    public float precision;
    public float tiempoCompletado;
    public int danoCausado;
    public int danoRecibido;

    public GameStatistics(DateTime fecha, string nombreMapa, int puntuacion, float precision, float tiempoCompletado, int danoCausado, int danoRecibido)
    {
        // Inicializa los campos con los valores proporcionados.
        this.fecha = fecha.ToString("yyyy-MM-dd HH:mm:ss");
        this.nombreMapa = nombreMapa;
        this.puntuacion = puntuacion;
        this.precision = precision;
        this.tiempoCompletado = tiempoCompletado;
        this.danoCausado = danoCausado;
        this.danoRecibido = danoRecibido;
    }
}
