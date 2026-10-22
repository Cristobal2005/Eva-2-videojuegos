using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Configuracion de Velocidad")]
    public float velocidadInicial = 6f;
    public float aceleracionPorSegundo = 0.15f;
    public float velocidadMaxima = 18f;
    public static float velocidadActual;

    [Header("Progreso del Jefe")]
    public int bajasParaJefe = 15;
    public int bajasActuales = 0;
    public bool jefeActivo = false;
    public GameObject jefePrefab;
    public Vector3 puntoSpawnJefe = new Vector3(12f, -1.5f, 0f);

    [Header("Objetos Recogidos (Meta: 7)")]
    public int objetosClaveRecogidos = 0;
    public int totalObjetosParaGanar = 7;

    [Header("Referencias de Interfaz (UI)")]
    public TextMeshProUGUI textoObjetosUI;
    public TextMeshProUGUI textoAlertaJefeUI;
    public float duracionAlertaJefe = 2.5f;
    public bool juegoIniciado = false;
    public GameObject MenuJuego;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        velocidadActual = velocidadInicial;
    }

    void Start()
    {
        // Si debe iniciar jugando, aseguramos el movimiento
        if (GameOverManager.debeIniciarJugando)
        {
            if (MenuJuego != null)
            {
                MenuJuego.SetActive(false);
            }
            PlayerController.puedeMoverse = true;
        }

        ActualizarTextoObjetos();

        if (textoAlertaJefeUI != null)
        {
            textoAlertaJefeUI.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (velocidadActual < velocidadMaxima)
        {
            velocidadActual += aceleracionPorSegundo * Time.deltaTime;
        }
    }

    public void SumarBaja()
    {
        bajasActuales++;

        if (bajasActuales >= bajasParaJefe && !jefeActivo)
        {
            StartCoroutine(SecuenciaAparicionJefe());
        }
    }

    [Header("Efecto de Parpadeo de Alerta")]
    public int cantidadParpadeos = 5;       
    public float intervaloParpadeo = 0.25f; 

    IEnumerator SecuenciaAparicionJefe()
    {
        jefeActivo = true;

        if (textoAlertaJefeUI != null)
        {
            for (int i = 0; i < cantidadParpadeos; i++)
            {
                if (SoundEffectsManager.instance != null)
                {
                    SoundEffectsManager.instance.ReproducirAlertaJefe();
                }
                textoAlertaJefeUI.gameObject.SetActive(true);
                yield return new WaitForSeconds(intervaloParpadeo);
                textoAlertaJefeUI.gameObject.SetActive(false);
                yield return new WaitForSeconds(intervaloParpadeo);
            }
        }
        else
        {
            yield return new WaitForSeconds(duracionAlertaJefe);
        }

        if (CamaraShake.instance != null)
        {
            CamaraShake.instance.Sacudir(0.4f, 0.25f);
        }

        if (jefePrefab != null)
        {
            Instantiate(jefePrefab, puntoSpawnJefe, Quaternion.identity);
        }
    }

    public void JefeVencido()
    {
        jefeActivo = false;
        bajasActuales = 0;
    }

    public void RecogerObjetoClave()
    {
        objetosClaveRecogidos++;
        ActualizarTextoObjetos();

        if (TilemapBackgroundChanger.instance != null)
        {
            TilemapBackgroundChanger.instance.AvanzarSiguienteFondo();
        }

        if (objetosClaveRecogidos >= totalObjetosParaGanar)
        {
            Debug.Log("¡GANASTE EL JUEGO!");
            if (GameOverManager.instance != null)
            {
                GameOverManager.instance.MostrarVictoria();
            }
        }
    }

    void ActualizarTextoObjetos()
    {
        if (textoObjetosUI != null)
        {
            textoObjetosUI.text = "Objetos: " + objetosClaveRecogidos + "/" + totalObjetosParaGanar;
        }
    }
}