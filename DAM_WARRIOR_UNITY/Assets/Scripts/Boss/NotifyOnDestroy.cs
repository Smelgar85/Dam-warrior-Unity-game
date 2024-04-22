using UnityEngine;

public class NotifyOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        StageManager.Instance.FlyingFortressDestroyed();
    }
}
