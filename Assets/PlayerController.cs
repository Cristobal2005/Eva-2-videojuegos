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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        saltosRestantes = saltosMaximos;
        
        if (muzzleFlash != null)
        {
            muzzleFlash.SetActive(false);
        }
    }

    void Update()
    {
        
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && saltosRestantes > 0)
        {
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
        if (balaPrefab != null && firePoint != null)
        {
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
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}