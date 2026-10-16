using UnityEngine;

public class LoopGround : MonoBehaviour
{
    [Header("Configuración del Bucle")]
    public float anchoSuelo = 20f;

    void Update()
    {
        // Usa la velocidad global del GameManager
        transform.position += Vector3.left * GameManager.velocidadActual * Time.deltaTime;

        if (transform.position.x <= -anchoSuelo)
        {
            transform.position += new Vector3(anchoSuelo * 2f, 0, 0);
        }
    }
}