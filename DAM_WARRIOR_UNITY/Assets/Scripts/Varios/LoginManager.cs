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

    void Start()
    {
        // Asegurarse de que la imagen de fade está inicializada como transparente
        fadeImage.color = new Color(0, 0, 0, 0);

        // Configurar el campo de contraseña para que muestre asteriscos
        passwordInput.contentType = TMP_InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate(); // Actualiza el campo para que se apliquen los cambios

        // Añadir listener para la tecla Enter en ambos campos de entrada
        usernameInput.onSubmit.AddListener(delegate { OnEnterKeyPressed(); });
        passwordInput.onSubmit.AddListener(delegate { OnEnterKeyPressed(); });
    }

    void Update()
    {
        // Detectar la tecla Enter para iniciar sesión
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnLoginButtonClicked();
        }
    }

    public void OnLoginButtonClicked()
    {
        // Reproducir sonido de login si está configurado
        if (audioSource != null && loginSound != null)
        {
            audioSource.PlayOneShot(loginSound);
        }

        StartCoroutine(Login());
    }

    public void OnGuestButtonClicked()
    {
        // Establecer el usuario y contraseña como "guest"
        PlayerPrefs.SetString("username", "guest");
        PlayerPrefs.SetString("password", "guest");
        // Iniciar el fade out
        StartCoroutine(FadeOutAndChangeScene());
    }

    private IEnumerator Login()
    {
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
                messageText.text = "Nombre de usuario o contraseña incorrectos.";
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
        StartCoroutine(Register());
    }

    private IEnumerator Register()
    {
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
        fadeAnimator.Play("FadeOut");
        yield return new WaitForSeconds(1); // Ajusta el tiempo según la duración de tu animación de fade out
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnEnterKeyPressed()
    {
        OnLoginButtonClicked();
    }
}
