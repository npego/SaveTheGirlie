using UnityEngine;

public class MobileButtons : MonoBehaviour
{
    // Esto lo leerá el Player para moverse
    public static float MoveAxis; // -1 izquierda, 0 nada, +1 derecha

    public void PressLeft()  => MoveAxis = -1f;
    public void PressRight() => MoveAxis =  1f;
    public void ReleaseMove()
    {
        // Solo para el movimiento (evita que se quede andando)
        MoveAxis = 0f;
    }
}