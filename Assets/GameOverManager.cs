using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;
    public static bool debeIniciarJugando = false;

    [Header("Paneles de UI")]
    public GameObject panelGameOver;
    public TextMeshProUGUI textoEstadisticasGameOver;

    public GameObject panelVictoria;
    public TextMeshProUGUI textoEstadisticasVictoria;

    private int enemigosEliminados = 0;
    private int obstaculosEsquivados = 0;
    private float tiempoInicio;
    
    
    [HideInInspector] public bool juegoTerminado = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        tiempoInicio = Time.time;
        juegoTerminado = false; // <-- Restablecer siempre al iniciar la escena
        if (panelGameOver != null) panelGameOver.SetActive(false);
        if (panelVictoria != null) panelVictoria.SetActive(false);
    }

    public void RegistrarEnemigoMuerto()
    {
        if (!juegoTerminado) enemigosEliminados++;
    }

    public void RegistrarObstaculoEsquivado()
    {
        if (!juegoTerminado) obstaculosEsquivados++;
    }

    private string ObtenerTextoEstadisticas()
    {
        float tiempoTotal = Time.time - tiempoInicio;
        int minutos = Mathf.FloorToInt(tiempoTotal / 60F);
        int segundos = Mathf.FloorToInt(tiempoTotal % 60F);
        string tiempoFormateado = string.Format("{0:00}:{1:00}", minutos, segundos);

        return $"Enemigos eliminados: {enemigosEliminados}\n" +
               $"Objetos esquivados: {obstaculosEsquivados}\n" +
               $"Tiempo de partida: {tiempoFormateado}";
    }

    public void MostrarGameOver()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;

        if (textoEstadisticasGameOver != null)
        {
            textoEstadisticasGameOver.text = ObtenerTextoEstadisticas();
        }

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void MostrarVictoria()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;

        if (textoEstadisticasVictoria != null)
        {
            textoEstadisticasVictoria.text = ObtenerTextoEstadisticas();
        }

        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void ReiniciarNivel()
    {
       Time.timeScale = 1f;
       debeIniciarJugando = true;
       PlayerController.puedeMoverse = true;
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}