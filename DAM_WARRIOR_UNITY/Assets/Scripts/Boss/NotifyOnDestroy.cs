/**
 * NotifyOnDestroy.cs
 * Este script notifica al StageManager cuando el objeto es destruido, indicando la destrucción de la fortaleza voladora.
 */

using UnityEngine;

public class NotifyOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        // Notifica al StageManager que la fortaleza voladora ha sido destruida.
        StageManager.Instance.FlyingFortressDestroyed();
    }
}
