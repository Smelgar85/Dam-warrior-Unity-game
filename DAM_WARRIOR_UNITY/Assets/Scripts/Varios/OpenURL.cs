using UnityEngine;

public class OpenURL : MonoBehaviour
{
    // URL que queremos abrir
    public string url = "http://smelgar85.eu.pythonanywhere.com/register";

    // M�todo para abrir la URL
    public void OpenRegistrationPage()
    {
        Application.OpenURL(url);
    }
}
