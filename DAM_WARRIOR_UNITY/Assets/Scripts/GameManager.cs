using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool fullPower = false; // Accesible globalmente

    // Este m�todo carga la escena 0
    public void LoadSceneZero()
    {
        SceneManager.LoadScene(0);
    }
}
