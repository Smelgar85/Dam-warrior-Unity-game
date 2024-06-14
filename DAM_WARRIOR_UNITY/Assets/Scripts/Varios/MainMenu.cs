using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text usernameText; // Campo para mostrar el nombre de usuario
    public TMP_Text logoutButtonText; // Campo para el texto del botón de logout

    void Start()
    {
        // Obtener el nombre de usuario de PlayerPrefs y mostrarlo en el texto
        string username = PlayerPrefs.GetString("username", "Guest");
        usernameText.text = "Logged as: " + username;

        // Cambiar el texto del botón de logout si el usuario es "guest"
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
        // Cargar la escena Map1
        SceneManager.LoadScene("Map1");
    }

    public void QuitGame()
    {
        // Salir del juego
        Application.Quit();
    }

    public void Logout()
    {
        // Limpiar los PlayerPrefs
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
        // Cargar la escena de login
        SceneManager.LoadScene("LoginScene");
    }
}
