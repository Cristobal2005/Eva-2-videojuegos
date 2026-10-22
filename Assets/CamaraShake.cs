using System.Collections;
using UnityEngine;

public class CamaraShake : MonoBehaviour
{
    public static CamaraShake instance;

    private Vector3 posicionOriginal;
    private Coroutine corrutinaTemblor;

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
        posicionOriginal = transform.localPosition;
    }

    public void Sacudir(float duracion = 0.3f, float magnitud = 0.2f)
    {
        if (corrutinaTemblor != null)
        {
            StopCoroutine(corrutinaTemblor);
            transform.localPosition = posicionOriginal;
        }

        corrutinaTemblor = StartCoroutine(EfectoTemblor(duracion, magnitud));
    }

    private IEnumerator EfectoTemblor(float duracion, float magnitud)
    {
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracion)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitud;
            float offsetY = Random.Range(-1f, 1f) * magnitud;

            transform.localPosition = new Vector3(posicionOriginal.x + offsetX, posicionOriginal.y + offsetY, posicionOriginal.z);

            tiempoTranscurrido += Time.unscaledDeltaTime;
            yield return null; 
        }

        transform.localPosition = posicionOriginal;
        corrutinaTemblor = null;
    }
}