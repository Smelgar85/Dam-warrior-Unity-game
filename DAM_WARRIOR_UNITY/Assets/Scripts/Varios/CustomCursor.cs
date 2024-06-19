/**
 * CustomCursor.cs
 * Este script permite personalizar el cursor del juego y ocultarlo tras un período de inactividad.
 */

using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [Tooltip("La textura del cursor")]
    public Texture2D cursorTexture; // La textura del cursor.
    [Tooltip("Punto caliente del cursor (0,0) es la esquina superior izquierda)")]
    public Vector2 hotSpot = Vector2.zero; // Punto caliente del cursor.
    public CursorMode cursorMode = CursorMode.Auto; // Modo del cursor.
    public float hideDelay = 2f; // Tiempo de inactividad antes de ocultar el cursor (en segundos).

    private float lastMouseMoveTime; // Última vez que el mouse se movió.

    void Start()
    {
        // Configura el cursor personalizado.
        SetCursor();
        lastMouseMoveTime = Time.time;
    }

    void Update()
    {
        // Detecta si el mouse se mueve y actualiza el tiempo de la última actividad.
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            lastMouseMoveTime = Time.time;
            ShowCursor();
        }

        // Oculta el cursor si ha pasado el tiempo de inactividad.
        if (Time.time - lastMouseMoveTime > hideDelay)
        {
            HideCursor();
        }
    }

    void SetCursor()
    {
        // Establece la textura del cursor.
        if (cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);
        }
        else
        {
            Debug.LogWarning("Cursor texture not set.");
        }
    }

    void ShowCursor()
    {
        // Muestra el cursor.
        Cursor.visible = true;
    }

    void HideCursor()
    {
        // Oculta el cursor.
        Cursor.visible = false;
    }
}
