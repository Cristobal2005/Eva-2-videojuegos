using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida")]
    public int vida = 2; 

    [Header("Limites")]
    public float limiteIzquierda = -15f;

    void Update()
    {
        
        transform.position += Vector3.left * GameManager.velocidadActual * Time.deltaTime;

        
        if (transform.position.x <= limiteIzquierda)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Bullet"))
        {
            RecibirDanio(1);
        }
    }

    public void RecibirDanio(int cantidad)
    {
        vida -= cantidad;

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        
        if (GameManager.instance != null)
        {
            GameManager.instance.SumarBaja();
        }

        Destroy(gameObject);
    }
}