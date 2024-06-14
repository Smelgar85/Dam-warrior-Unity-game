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

    // Añadir referencias para el fade out
    public Image fadeImage;
    public Animator fadeAnimator;
    public string nextSceneName = "MenuInicio"; // Nombre de la escena a cargar

    void Start()
    {
        // Asegurarse de que la imagen de fade está inicializada como transparente
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public void OnLoginButtonClicked()
    {
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
            // Aquí manejamos la respuesta del servidor
            if (www.downloadHandler.text.Contains("incorrectos"))
            {
                messageText.text = "Nombre de usuario o contraseña incorrectos.";
            }
            else
            {
                // Guardar las credenciales del usuario
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.SetString("password", password);
                // Iniciar el fade out
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
        // Reproducir la animación de fade out
        fadeAnimator.Play("FadeOut");
        // Esperar hasta que la imagen de fade esté completamente opaca
        yield return new WaitForSeconds(1); // Ajusta el tiempo según la duración de tu animación de fade out
        // Cambiar a la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
