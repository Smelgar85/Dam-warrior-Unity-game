/**
 * PlaySoundOnType.cs
 * Este script reproduce un sonido cuando el usuario escribe en un campo de entrada de texto.
 */

using UnityEngine;
using TMPro;

public class PlaySoundOnType : MonoBehaviour
{
    public AudioSource audioSource; // Fuente de audio para reproducir el sonido.
    public AudioClip typingSound; // Clip de audio que queremos reproducir.

    void Start()
    {
        // Añadir listener para cada entrada de texto en los campos TMP_InputField.
        TMP_InputField inputField = GetComponent<TMP_InputField>();
        if (inputField != null)
        {
            inputField.onValueChanged.AddListener(PlaySound);
        }
    }

    void Update()
    {
        // Aquí se podrían detectar otras teclas si es necesario.
    }

    void PlaySound(string input)
    {
        // No reproducir sonido si se presiona Backspace o Delete.
        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))
        {
            return;
        }

        // Reproduce el sonido al escribir.
        if (audioSource != null && typingSound != null)
        {
            audioSource.PlayOneShot(typingSound);
        }
    }
}
