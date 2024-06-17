/**
 * ScrollCredits.cs
 * Este script se utiliza para desplazar los créditos en la pantalla.
 */

using UnityEngine;
using TMPro;

public class ScrollCredits : MonoBehaviour
{
    public float scrollSpeed = 20f;
    private RectTransform rectTransform;

    void Start()
    {
        // Obtiene el componente RectTransform del objeto.
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Desplaza los créditos hacia arriba a una velocidad constante.
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
