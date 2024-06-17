/**
 * MainMenu.cs
 * Este script maneja las interacciones del menú principal, como iniciar el juego, cerrar la aplicación,
 * y cerrar sesión del usuario.
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text usernameText; // Campo para mostrar el nombre de usuario.
    public TMP_Text logoutButtonText; // Campo para el texto del botón de logout.

    void Start()
    {
        // Obtiene el nombre de usuario de PlayerPrefs y lo muestra en el texto.
        string username = PlayerPrefs.GetString("username", "Guest");
        usernameText.text = "Logged as: " + username;

        // Cambia el texto del botón de logout si el usuario es "guest".
        if (username == "guest")
        {
            logoutButtonText.text = "Login";
        }
        else
        {
            logoutButtonText.text = "Logout";
        }
    }

    public void PlayGame()
    {
        // Carga la escena Map1.
        SceneManager.LoadScene("Map1");
    }

    public void QuitGame()
    {
        // Sale del juego.
        Application.Quit();
    }

    public void Logout()
    {
        // Limpia los PlayerPrefs y carga la escena de login.
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
        SceneManager.LoadScene("LoginScene");
    }
}
