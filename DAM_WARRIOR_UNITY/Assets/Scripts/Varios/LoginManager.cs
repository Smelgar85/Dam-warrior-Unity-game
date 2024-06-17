/**
 * LoginManager.cs
 * Este script gestiona la funcionalidad de inicio de sesión, registro y acceso como invitado en el juego.
 * Incluye la conexión a un servidor para autenticar y registrar usuarios, así como el manejo de transiciones 
 * de escena con efectos de fade y sonido.
 */

using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text messageText;
    public string loginUrl = "https://smelgar85.eu.pythonanywhere.com/login";
    public string registerUrl = "https://smelgar85.eu.pythonanywhere.com/register";

    public Image fadeImage;
    public Animator fadeAnimator;
    public string nextSceneName = "MenuInicio"; // Nombre de la escena a cargar

    // Añadido para el sonido de login
    public AudioSource audioSource;
    public AudioClip loginSound;

    // Añadido para el fade de audio
    public AudioFader audioFader;

    void Start()
    {
        // Inicializa la imagen de fade como transparente.
        fadeImage.color = new Color(0, 0, 0, 0);

        // Configura el campo de contraseña para mostrar asteriscos.
        passwordInput.contentType = TMP_InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate(); // Actualiza el campo para que se apliquen los cambios.

        // Añade listener para la tecla Enter en ambos campos de entrada.
        usernameInput.onSubmit.AddListener(delegate { OnEnterKeyPressed(); });
        passwordInput.onSubmit.AddListener(delegate { OnEnterKeyPressed(); });
    }

    void Update()
    {
        // Detecta la tecla Enter para iniciar sesión.
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnLoginButtonClicked();
        }
    }

    public void OnLoginButtonClicked()
    {
        // Reproduce sonido de login si está configurado.
        if (audioSource != null && loginSound != null)
        {
            audioSource.PlayOneShot(loginSound);
        }

        // Inicia el proceso de login.
        StartCoroutine(Login());
    }

    public void OnGuestButtonClicked()
    {
        // Establece el usuario y contraseña como "guest" y carga la escena siguiente.
        PlayerPrefs.SetString("username", "guest");
        PlayerPrefs.SetString("password", "guest");
        StartCoroutine(FadeOutAndChangeScene());
    }

    private IEnumerator Login()
    {
        // Envía los datos de login al servidor y maneja la respuesta.
        string username = usernameInput.text;
        string password = passwordInput.text;

        WWWForm form = new WWWForm();
        form.AddField("nombre_usuario", username);
        form.AddField("contrasena", password);

        UnityWebRequest www = UnityWebRequest.Post(loginUrl, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            messageText.text = "Error: " + www.error;
        }
        else
        {
            if (www.downloadHandler.text.Contains("incorrectos"))
            {
                messageText.text = "Wrong username or password.";
            }
            else
            {
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.SetString("password", password);
                StartCoroutine(FadeOutAndChangeScene());
            }
        }
    }

    public void OnRegisterButtonClicked()
    {
        // Inicia el proceso de registro.
        StartCoroutine(Register());
    }

    private IEnumerator Register()
    {
        // Envía los datos de registro al servidor y maneja la respuesta.
        string username = usernameInput.text;
        string password = passwordInput.text;

        WWWForm form = new WWWForm();
        form.AddField("nombre_usuario", username);
        form.AddField("contrasena", password);

        UnityWebRequest www = UnityWebRequest.Post(registerUrl, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            messageText.text = "Error: " + www.error;
        }
        else
        {
            messageText.text = www.downloadHandler.text;
        }
    }

    private IEnumerator FadeOutAndChangeScene()
    {
        // Inicia el efecto de fade out y cambia de escena.
        if (audioFader != null)
        {
            audioFader.FadeOutAudio();
        }

        fadeAnimator.Play("FadeOut");
        yield return new WaitForSeconds(1); // Ajusta el tiempo según la duración de tu animación de fade out.
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnEnterKeyPressed()
    {
        // Maneja la tecla Enter para iniciar sesión.
        OnLoginButtonClicked();
    }
}
