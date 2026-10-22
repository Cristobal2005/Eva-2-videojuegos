using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject panelPausa;

    public static bool juegoPausado = false;

    void Start()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (GameOverManager.instance != null && GameOverManager.instance.juegoTerminado)
            {
                return;
            }

            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        juegoPausado = true;
        Time.timeScale = 0f;

        if (panelPausa != null)
        {
            panelPausa.SetActive(true);
        }

        AudioListener.pause = true; 
    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;

        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }

        AudioListener.pause = false; 
    }

    public void ReiniciarPartida()
    {
        Time.timeScale = 1f;
        juegoPausado = false;
        AudioListener.pause = false;
        GameOverManager.debeIniciarJugando = true;
        PlayerController.puedeMoverse = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        juegoPausado = false;
        AudioListener.pause = false; 
        PlayerController.puedeMoverse = false;

        
        GameOverManager.debeIniciarJugando = false; 
        

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}