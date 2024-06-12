using UnityEngine;
using UnityEngine.UI;

public class MovimientoFondo : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;

    void Update()
    {
        float newPosX = Mathf.Repeat(_img.uvRect.x + _x * Time.deltaTime, 1);
        float newPosY = Mathf.Repeat(_img.uvRect.y + _y * Time.deltaTime, 1);
        _img.uvRect = new Rect(new Vector2(newPosX, newPosY), _img.uvRect.size);
    }
}
