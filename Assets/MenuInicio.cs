using UnityEngine;

public class MenuInicio : MonoBehaviour
{
    [Header("Referencias de UI")]
    public GameObject MenuJuego;

    void Start()
    {
        // Si venimos de un Reiniciar (Pausa o GameOver), no mostramos el menú y dejamos jugar
        if (GameOverManager.debeIniciarJugando)
        {
            if (MenuJuego != null)
            {
                MenuJuego.SetActive(false);
            }
            Time.timeScale = 1f;
            PlayerController.puedeMoverse = true;
        }
        else
        {
            // Inicio normal del juego por primera vez
            Time.timeScale = 0f;
            PlayerController.puedeMoverse = false; 

            if (MenuJuego != null)
            {
                MenuJuego.SetActive(true);
            }
        }
    }

    public void IniciarJuego()
    {
        if (MenuJuego != null)
        {
            MenuJuego.SetActive(false);
        }

        GameObject[] obstaculos = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obs in obstaculos)
        {
            Destroy(obs);
        }

        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject ene in enemigos)
        {
            Destroy(ene);
        }

        PlayerController jugador = FindAnyObjectByType<PlayerController>();
        if (jugador != null)
        {
            jugador.ResetearJugador();
        }

        PlayerController.puedeMoverse = true;

        ObstacleSpawner spawner = FindAnyObjectByType<ObstacleSpawner>();
        if (spawner != null)
        {
            spawner.enabled = false;
            spawner.enabled = true;
        }

        Time.timeScale = 1f;
    }
}