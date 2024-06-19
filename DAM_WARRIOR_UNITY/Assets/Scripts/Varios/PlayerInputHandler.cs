// Este script maneja la entrada del jugador para controlar una nave espacial en un juego.
// Incluye mecanismos para mover la nave y disparar láseres utilizando el sistema de Input System de Unity.

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls controls;               // Objeto para manejar los controles del jugador
    private MovimientoNave movimientoNave;         // Referencia al componente MovimientoNave
    private LaserShoot laserShoot;                 // Referencia al componente LaserShoot

    // Inicializa el manejo de controles y obtiene las referencias a los componentes relacionados
    private void Awake()
    {
        controls = new PlayerControls();
        movimientoNave = GetComponent<MovimientoNave>();
        laserShoot = GetComponent<LaserShoot>();

        // Configura los callbacks para los eventos de control
        controls.PlayerControl.Move.performed += ctx => movimientoNave.OnMove(ctx);
        controls.PlayerControl.Move.canceled += ctx => movimientoNave.OnMove(ctx);

        controls.PlayerControl.Shoot.performed += ctx => laserShoot.OnShoot(ctx);
        controls.PlayerControl.SpecialShoot.performed += ctx => laserShoot.OnSpecialShoot(ctx);
    }

    // Habilita los controles cuando el objeto está activo
    private void OnEnable()
    {
        controls.Enable();
    }

    // Deshabilita los controles cuando el objeto ya no está activo
    private void OnDisable()
    {
        controls.Disable();
    }
}
