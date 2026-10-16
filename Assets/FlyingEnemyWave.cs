using UnityEngine;

public class FlyingEnemyWave : MonoBehaviour
{
    [Header("Ondulación en el aire")]
    public float frecuencia = 3f;  // Qué tan rápido aletea arriba y abajo
    public float amplitud = 0.5f;   // Qué tan alto sube y baja
    private float posYInicial;

    void Start()
    {
        posYInicial = transform.position.y;
    }

    void Update()
    {
        // Genera una oscilación suave vertical
        float nuevaY = posYInicial + Mathf.Sin(Time.time * frecuencia) * amplitud;
        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
    }
}