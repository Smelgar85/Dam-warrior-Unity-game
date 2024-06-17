/**
 * AudioFader.cs
 * Este script se utiliza para hacer un fade out de una fuente de audio.
 */

using System.Collections;
using UnityEngine;

public class AudioFader : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 2f;

    public void FadeOutAudio()
    {
        // Inicia la corrutina para hacer fade out del audio.
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        // Reduce gradualmente el volumen de la fuente de audio hasta silenciarla.
        float startVolume = audioSource.volume;
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0, timer / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }
}
