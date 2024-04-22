using UnityEngine;
using UnityEngine.UI; // Necesitas agregar esta línea para usar RawImage

public class MovimientoFondo : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;
    void Update()
    {
       _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x,_y) * Time.deltaTime, _img.uvRect.size);
    }
}
