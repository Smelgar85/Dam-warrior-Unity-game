/**
 * MainMenu.cs
 * Este script controla el menú principal del juego,
 * incluyendo la navegación, inicio de sesión/cierre de sesión 
 * y el inicio o salida del juego.
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text logoutButtonText;
    
    public Button startGameButton;
    public Button exitGameButton;
    public Button loginButton;

    private PlayerControls controls;

    // Inicializa los controles cuando se despierta este componente
    private void Awake()
    {
        controls = new PlayerControls();

        // Asignar acciones de navegación y selección
        controls.UI.Move.performed += ctx => Navigate(ctx);
        controls.UI.Submit.performed += ctx => Submit(ctx);
    }


    // Configura el texto inicial del menú y la navegación entre botones
    private void Start()
    {
        string username = PlayerPrefs.GetString("username", "Guest");
        usernameText.text = "Logged as: " + username;

        if (username.ToLower() == "guest")
        {
            logoutButtonText.text = "Login";
        }
        else
        {
            logoutButtonText.text = "Logout";
        }

        // Configuración inicial de navegación
        SetupNavigation();

        // Seleccionar el primer botón por defecto
        if (startGameButton != null)
        {
            startGameButton.Select();
        }
    }

    // Configura la navegación entre los botones usando el teclado y el gamepad
    private void SetupNavigation()
    {
        Button[] buttons = { startGameButton, exitGameButton, loginButton };
        for (int i = 0; i < buttons.Length; i++)
        {
            Navigation navigation = new Navigation();
            navigation.mode = Navigation.Mode.Explicit;

            if (i > 0)
            {
                navigation.selectOnUp = buttons[i - 1];
            }
            if (i < buttons.Length - 1)
            {
                navigation.selectOnDown = buttons[i + 1];
            }

        }
    }

    // Permite navegar entre los elementos con la entrada del controlador
    private void Navigate(InputAction.CallbackContext context)
    {
        Vector2 navigation = context.ReadValue<Vector2>();
        GameObject current = EventSystem.current.currentSelectedGameObject;

        if (navigation.y > 0 && current.GetComponent<Selectable>().navigation.selectOnUp != null)
        {
            current.GetComponent<Selectable>().navigation.selectOnUp.Select();
        }
        else if (navigation.y < 0 && current.GetComponent<Selectable>().navigation.selectOnDown != null)
        {
            current.GetComponent<Selectable>().navigation.selectOnDown.Select();
        }
    }

    // Permite seleccionar un elemento utilizando la entrada del controlador
    private void Submit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;
            if (current != null)
            {
                Button button = current.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke();
                }
            }
        }
    }

    // Métodos de manejo del cursor para clics en pantalla
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress != null)
        {
            eventData.pointerPress.GetComponent<Button>().onClick.Invoke();
        }
    }

    // Inicia el juego cargando la escena del mapa
    public void PlayGame()
    {
        SceneManager.LoadScene("Map1");
    }

    // Cierra la aplicación del juego
    public void QuitGame()
    {
        Application.Quit();
    }

    // Cierra sesión y carga la escena de inicio de sesión
    public void Logout()
    {
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
        SceneManager.LoadScene("LoginScene");
    }
}
