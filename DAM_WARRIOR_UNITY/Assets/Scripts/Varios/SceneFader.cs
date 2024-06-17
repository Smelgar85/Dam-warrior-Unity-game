/**
 * SceneFader.cs
 * Este script se utiliza para hacer efectos de fade-in y fade-out entre transiciones de escenas.
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Start()
    {
        if (fadeImage != null)
        {
            // Comienza con un fade in.
            StartCoroutine(FadeIn());
        }
    }

    public void FadeToScene(string sceneName)
    {
        // Inicia el fade out y cambia de escena.
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeIn()
    {
        // Realiza un fade in gradual.
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeOut(string sceneName)
    {
        // Realiza un fade out gradual y luego carga la nueva escena.
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Carga la nueva escena.
        SceneManager.LoadScene(sceneName);
    }
}
