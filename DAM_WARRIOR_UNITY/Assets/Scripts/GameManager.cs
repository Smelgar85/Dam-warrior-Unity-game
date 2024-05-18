using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool fullPower = false; // Accesible globalmente

    public void LoadSceneZero()
    {
        SceneController.Instance.LoadScene(0);
    }
}
