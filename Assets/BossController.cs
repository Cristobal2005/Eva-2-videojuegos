using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Vida del Jefe")]
    public int vidaMaxima = 12;
    private int vidaActual;

    [Header("Posición de Combate")]
    public float velocidadEntrada = 4f;
    public float posicionParadaX = 6f; 
    private bool enPosicion = false;

    [Header("Movimiento Ondulado (Vuelo)")]
    public float frecuencia = 2.5f;   
    public float amplitud = 0.6f;      
    private float posYInicial;

    [Header("Recompensa al morir")]
    public GameObject objetoVictoriaPrefab;

    void Start()
    {
        vidaActual = vidaMaxima;
        posYInicial = transform.position.y;
    }

    void Update()
    {
        
        if (!enPosicion)
        {
            if (transform.position.x > posicionParadaX)
            {
                transform.position += Vector3.left * velocidadEntrada * Time.deltaTime;
            }
            else
            {
                enPosicion = true;
            }
        }

        
        float nuevaY = posYInicial + Mathf.Sin(Time.time * frecuencia) * amplitud;
        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
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
        vidaActual -= cantidad;
        Debug.Log("Vida del Jefe: " + vidaActual + "/" + vidaMaxima);

        if (vidaActual <= 0)
        {
            DerrotarJefe();
        }
    }

    void DerrotarJefe()
    {
        Debug.Log("¡Jefe derrotado!");

        if (objetoVictoriaPrefab != null)
        {
            Instantiate(objetoVictoriaPrefab, transform.position, Quaternion.identity);
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.JefeVencido();
        }

        Destroy(gameObject);
    }
}
