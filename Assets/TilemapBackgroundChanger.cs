using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBackgroundChanger : MonoBehaviour
{
    public static TilemapBackgroundChanger instance;

    [Header("Tilemaps de los 3 Fondos")]
    public GameObject[] fondosTilemap; 
    private int indiceFondoActual = 0;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ActualizarVisibilidadFondos();
    }

    public void AvanzarSiguienteFondo()
    {
        if (fondosTilemap == null || fondosTilemap.Length == 0) return;

        
        indiceFondoActual = (indiceFondoActual + 1) % fondosTilemap.Length;
        ActualizarVisibilidadFondos();

        Debug.Log("Cambiado a Tilemap de fondo: " + fondosTilemap[indiceFondoActual].name);
    }

    void ActualizarVisibilidadFondos()
    {
        for (int i = 0; i < fondosTilemap.Length; i++)
        {
            if (fondosTilemap[i] != null)
            {
                
                fondosTilemap[i].SetActive(i == indiceFondoActual);
            }
        }
    }
}
