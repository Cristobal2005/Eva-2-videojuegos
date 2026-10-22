using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class BossController : MonoBehaviour
{
    [Header("Vida del Jefe")]
    public int vidaMaxima = 12;
    private int vidaActual;

    [Header("UI del Jefe")]
    public Slider barraVida;

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
    [Header("Efecto de Daño")]
    public Color colorDanio = Color.red; // Puedes poner Color.white si prefieres parpadeo blanco
    public float duracionParpadeo = 0.08f;

    private SpriteRenderer spriteRenderer;
    private Color colorOriginal;
    private Coroutine corrutinaParpadeo;

    void Start()
{
    vidaActual = vidaMaxima;
    posYInicial = transform.position.y;
    spriteRenderer = GetComponent<SpriteRenderer>();
    if (spriteRenderer != null)
        {
            colorOriginal = spriteRenderer.color;
        }

    
    if (barraVida != null)
    {
        barraVida.minValue = 0;
        barraVida.maxValue = vidaMaxima;
        barraVida.value = vidaActual; 
    }

    if (MusicManager.instance != null)
    {
        MusicManager.instance.ReproducirJefe();
    }
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
    if (corrutinaParpadeo != null)
        {
            StopCoroutine(corrutinaParpadeo);
        }
        corrutinaParpadeo = StartCoroutine(ParpadeoDanio());

    if (barraVida != null)
    {
        barraVida.value = vidaActual;
    }

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

        
        if (GameOverManager.instance != null)
        {
            GameOverManager.instance.RegistrarEnemigoMuerto();
        }

        try
        {
            if (MusicManager.instance != null)
            {
                MusicManager.instance.ReproducirNormal();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Error en MusicManager: " + e.Message);
        }

        Destroy(gameObject);
    }
    private IEnumerator ParpadeoDanio()
{
    if (spriteRenderer != null)
    {
        spriteRenderer.color = colorDanio;
        yield return new WaitForSeconds(duracionParpadeo);
        spriteRenderer.color = colorOriginal;
    }
    corrutinaParpadeo = null;
}
}
