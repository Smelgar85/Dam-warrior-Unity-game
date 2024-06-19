/**
 * GamepadButtonHandler.cs
 * Este script maneja el pasar a otra escena de manera sencilla. Sé que hay formas más elegantes de hacerlo, pero no me daba tiempo a refinarlo.
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamepadButtonHandler : MonoBehaviour
{
    public Button nextButton; // Referencia al botón en la interfaz de usuario que carga la escena de créditos

    void Update()
    {
        // Verifica si se presiona el botón de submit del gamepad
        if (Input.GetButtonDown("Submit"))
        {
            // Simula un clic en el botón
            nextButton.onClick.Invoke();
        }
    }
}
