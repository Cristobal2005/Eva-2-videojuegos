using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Configuracion de Salto")]
    public float fuerzaSalto = 10f;
    public int saltosMaximos = 2; 
    private int saltosRestantes;
    private Rigidbody2D rb;
    private bool enSuelo = false;

    [Header("Configuracion de Disparo")]
    public GameObject balaPrefab;
    public Transform firePoint;
    public GameObject muzzleFlash;
    public float duracionFlash = 0.05f;
    public float cadenciaDisparo = 0.2f;
    private float tiempoSiguienteDisparo = 0f;
    public static bool puedeMoverse = true;
    private Vector3 posicionInicial;
    [Header("Ajuste de Gravedad en Salto")]
    public float multiplicadorCaida = 2.5f;        
    public float multiplicadorSaltoCorto = 2f;
    

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        saltosRestantes = saltosMaximos;
        enSuelo = true;
        puedeMoverse = true;

        if (muzzleFlash != null)
        {
            muzzleFlash.SetActive(false);
        }
    }

    void Update()
    {
        if (!puedeMoverse || Time.timeScale == 0f) return;
        
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && saltosRestantes > 0)
        {
            if (SoundEffectsManager.instance != null)
            {
                SoundEffectsManager.instance.ReproducirSalto();
            }
            Saltar();
        }

        
        if (Mouse.current != null && Mouse.current.leftButton.isPressed && Time.time >= tiempoSiguienteDisparo)
        {
            Disparar();
            tiempoSiguienteDisparo = Time.time + cadenciaDisparo;
        }
    }

    void Saltar()
    {
        
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        saltosRestantes--;
        enSuelo = false;
    }

    void Disparar()
    {
        if (PauseMenu.juegoPausado) return;
        if (balaPrefab != null && firePoint != null)
        {
            if (SoundEffectsManager.instance != null)
            {
                SoundEffectsManager.instance.ReproducirDisparo();
            }
            Instantiate(balaPrefab, firePoint.position, firePoint.rotation);
        }

        if (muzzleFlash != null)
        {
            StopCoroutine("OcultarFlash");
            StartCoroutine("OcultarFlash");
        }
    }

    IEnumerator OcultarFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(duracionFlash);
        muzzleFlash.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!puedeMoverse) return;
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            if (GameOverManager.instance != null)
            {
                GameOverManager.instance.MostrarGameOver();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            enSuelo = true;
            saltosRestantes = saltosMaximos; 
        }
    }
    void Awake()
    {
        posicionInicial = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }
    public void ResetearJugador()
    {
        transform.position = posicionInicial;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    void FixedUpdate()
    {
        if (rb == null || Time.timeScale == 0f) return;
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (multiplicadorCaida - 1) * Time.fixedDeltaTime;

        }
        else if (rb.linearVelocity.y > 0 && Keyboard.current != null && !Keyboard.current.spaceKey.isPressed)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (multiplicadorSaltoCorto - 1) * Time.fixedDeltaTime;
        }
    }
}