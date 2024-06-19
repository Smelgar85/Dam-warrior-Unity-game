/**
 * GameManager.cs
 * En realidad este script lo cree en un principio para gestionar la variable fullPower, que indica si el disparo secundario está disponible.
 Debería moverlo a otro script, por ejemplo al que gestiona los disparos (LaserShoot)
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool fullPower = false; // Accesible globalmente

}
