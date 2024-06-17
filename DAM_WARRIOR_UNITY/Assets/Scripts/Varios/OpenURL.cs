/**
 * OpenURL.cs
 * Este script se utiliza para abrir una URL de registro en el navegador
 */

using UnityEngine;

public class OpenURL : MonoBehaviour
{
    // URL que queremos abrir.
    public string url = "http://smelgar85.eu.pythonanywhere.com/register";

    // Método para abrir la URL.
    public void OpenRegistrationPage()
    {
        // Abre la URL en el navegador.
        Application.OpenURL(url);
    }
}
