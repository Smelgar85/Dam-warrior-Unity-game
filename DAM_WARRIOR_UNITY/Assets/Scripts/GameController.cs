using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public string nombreMapa = "Map1";
    private int disparosRealizados = 0;
    private int disparosAcertados = 0;
    private float tiempoInicio;
    private int danoCausado = 0;
    private int danoRecibido = 0;

    void Start()
    {
        // Almacena el tiempo de inicio al comenzar la partida.
        tiempoInicio = Time.time;
    }

    public void RegistrarDisparo(bool acertado)
    {
        // Registra un disparo y si fue acertado, incrementa la puntuación.
        disparosRealizados++;
        if (acertado)
        {
            disparosAcertados++;
            ScoreManager.Instance.AddScore(10); // Incrementa la puntuación por disparo acertado.
        }
    }

    public void RegistrarDanoCausado(int dano)
    {
        // Registra el daño causado y añade la puntuación correspondiente.
        danoCausado += dano;
        ScoreManager.Instance.AddScore(dano); // Incrementa la puntuación por el daño causado.
    }

    public void RegistrarDanoRecibido(int dano)
    {
        // Registra el daño recibido.
        danoRecibido += dano;
    }

    public void FinalizarPartida()
    {
        // Calcula las estadísticas de la partida y las guarda.
        float tiempoCompletado = Time.time - tiempoInicio;
        float precision = (disparosRealizados > 0) ? (float)disparosAcertados / disparosRealizados : 0f;

        GameStatistics estadisticas = new GameStatistics(
            DateTime.Now,
            nombreMapa,
            ScoreManager.Instance.GetScore(),
            precision,
            tiempoCompletado,
            danoCausado,
            danoRecibido
        );

        GuardarEstadisticas(estadisticas);
    }

    private void GuardarEstadisticas(GameStatistics estadisticas)
    {
        // Guarda las estadísticas en PlayerPrefs como JSON.
        string json = JsonUtility.ToJson(estadisticas);
        PlayerPrefs.SetString("ultimaPartida", json);
        PlayerPrefs.Save();

        Debug.Log("Estadísticas guardadas: " + json);
    }
}
