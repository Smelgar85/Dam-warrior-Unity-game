using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        SceneController.Instance.LoadNextScene();
    }

    public void Salir()
    {
        SceneController.Instance.QuitGame();
    }
}

