using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [Tooltip("La textura del cursor")]
    public Texture2D cursorTexture; // La textura del cursor
    [Tooltip("Punto caliente del cursor (0,0) es la esquina superior izquierda)")]
    public Vector2 hotSpot = Vector2.zero; // Punto caliente del cursor
    public CursorMode cursorMode = CursorMode.Auto; // Modo del cursor

    void Start()
    {
        SetCursor();
    }

    void SetCursor()
    {
        if (cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);
        }
        else
        {
            Debug.LogWarning("Cursor texture not set.");
        }
    }
}
