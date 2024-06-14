using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [Tooltip("La textura del cursor")]
    public Texture2D cursorTexture; // La textura del cursor
    [Tooltip("Punto caliente del cursor (0,0) es la esquina superior izquierda)")]
    public Vector2 hotSpot = Vector2.zero; // Punto caliente del cursor
    public CursorMode cursorMode = CursorMode.Auto; // Modo del cursor
    public float hideDelay = 2f; // Tiempo de inactividad antes de ocultar el cursor (en segundos)

    private float lastMouseMoveTime; // Última vez que el mouse se movió

    void Start()
    {
        SetCursor();
        lastMouseMoveTime = Time.time;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            // Si el mouse se mueve, actualiza el tiempo de la última actividad y muestra el cursor
            lastMouseMoveTime = Time.time;
            ShowCursor();
        }

        if (Time.time - lastMouseMoveTime > hideDelay)
        {
            // Si ha pasado el tiempo de inactividad, oculta el cursor
            HideCursor();
        }
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

    void ShowCursor()
    {
        Cursor.visible = true;
    }

    void HideCursor()
    {
        Cursor.visible = false;
    }
}
