using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [Header("Ajustes de la Bala")]
    public float velocidad = 15f;
    public float tiempoVida = 3f; // Se destruye sola a los 3 segundos si no choca con nada

    void Start()
    {
        // Se autodestruye para no saturar memoria si sale volando
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        // Avanza en línea recta hacia la derecha
        transform.position += Vector3.right * velocidad * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si impacta contra un enemigo o un obstáculo, destruye la bala
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}