using UnityEngine;

public class ControladorZonasSuelo : MonoBehaviour
{
    public static ControladorZonasSuelo instance;

    [System.Serializable]
    public class ParejaSuelo
    {
        public GameObject pisoA;
        public GameObject pisoB;
    }

    [Header("Parejas de pisos por Zona")]
    public ParejaSuelo[] zonasSuelo;
    private int zonaActual = 0;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ActualizarSuelosInicial();
    }

    public void SiguienteZona()
    {
        if (zonasSuelo == null || zonasSuelo.Length == 0) return;

        int zonaAnterior = zonaActual;
        zonaActual = (zonaActual + 1) % zonasSuelo.Length;

        
        Vector3 posA = zonasSuelo[zonaAnterior].pisoA.transform.position;
        Vector3 posB = zonasSuelo[zonaAnterior].pisoB.transform.position;

        
        if (zonasSuelo[zonaActual].pisoA != null)
        {
            zonasSuelo[zonaActual].pisoA.transform.position = posA;
            zonasSuelo[zonaActual].pisoA.SetActive(true);
        }

        if (zonasSuelo[zonaActual].pisoB != null)
        {
            zonasSuelo[zonaActual].pisoB.transform.position = posB;
            zonasSuelo[zonaActual].pisoB.SetActive(true);
        }

        
        if (zonasSuelo[zonaAnterior].pisoA != null) zonasSuelo[zonaAnterior].pisoA.SetActive(false);
        if (zonasSuelo[zonaAnterior].pisoB != null) zonasSuelo[zonaAnterior].pisoB.SetActive(false);

        Debug.Log("Transición limpia: Suelos de la Zona " + (zonaActual + 1) + " sincronizados.");
    }

    void ActualizarSuelosInicial()
    {
        for (int i = 0; i < zonasSuelo.Length; i++)
        {
            bool estaActiva = (i == zonaActual);
            if (zonasSuelo[i].pisoA != null) zonasSuelo[i].pisoA.SetActive(estaActiva);
            if (zonasSuelo[i].pisoB != null) zonasSuelo[i].pisoB.SetActive(estaActiva);
        }
    }
}