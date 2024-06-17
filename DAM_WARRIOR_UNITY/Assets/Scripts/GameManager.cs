using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool fullPower = false; // Accesible globalmente

    public void LoadSceneZero()
    {
        // Carga la escena con índice 0.
        SceneController.Instance.LoadScene(0);
    }
}
