using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadRodar = 4f;

    void Update()
    {
        
        transform.position += Vector3.left * velocidadRodar * Time.deltaTime;

        
        if (transform.position.x <= -15f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            if (SoundEffectsManager.instance != null)
            {
                SoundEffectsManager.instance.ReproducirObjetoEspecial();
            }
            if (GameManager.instance != null)
            {
                GameManager.instance.RecogerObjetoClave();
            }

            Destroy(gameObject);
        }
    }
}