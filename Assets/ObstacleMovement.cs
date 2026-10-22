using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [Header("Limites")]
    public float limiteIzquierda = -15f;

    void Update()
    {
        
        transform.position += Vector3.left * GameManager.velocidadActual * Time.deltaTime;

        if (transform.position.x <= limiteIzquierda)
        {
            if (GameOverManager.instance != null)
            {
                GameOverManager.instance.RegistrarObstaculoEsquivado();
            }
            Destroy(gameObject);
        }
    }
}