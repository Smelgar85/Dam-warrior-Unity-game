using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        // Carga la siguiente escena en la secuencia.
        SceneController.Instance.LoadNextScene();
    }

    public void Salir()
    {
        // Sale del juego.
        SceneController.Instance.QuitGame();
    }
}
