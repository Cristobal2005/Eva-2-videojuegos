using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [Header("Ajustes de la Bala")]
    public float velocidad = 15f;
    public float tiempoVida = 3f; 

    void Start()
    {
        
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        
        transform.position += Vector3.right * velocidad * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
{
    // Impacto contra el Jefe
    if (collision.CompareTag("Boss") || collision.CompareTag("Enemy"))
    {
        BossController jefe = collision.GetComponent<BossController>();
        if (SoundEffectsManager.instance != null)
            {
                SoundEffectsManager.instance.ReproducirImpacto();
            }
        Destroy(gameObject);
        return;
    }

    
    if (collision.CompareTag("Obstacle"))
    {
        Destroy(gameObject);
    }
}
}