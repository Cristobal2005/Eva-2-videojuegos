using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Fuentes de Audio")]
    public AudioSource fuenteNormal;
    public AudioSource fuenteJefe;

    [Header("Pistas de Audio")]
    public AudioClip musicaNormal;
    public AudioClip musicaJefe;

    [Header("Configuración de Fundido")]
    [Tooltip("Tiempo en segundos que tarda en hacer la transición")]
    public float duracionTransicion = 1.5f;

    [Range(0f, 1f)]
    public float volumenMaximo = 1f;

    private Coroutine corrutinaTransicion;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        
        if (fuenteNormal != null && musicaNormal != null)
        {
            fuenteNormal.clip = musicaNormal;
            fuenteNormal.loop = true;
            fuenteNormal.volume = volumenMaximo;
            fuenteNormal.Play();
        }

        
        if (fuenteJefe != null && musicaJefe != null)
        {
            fuenteJefe.clip = musicaJefe;
            fuenteJefe.loop = true;
            fuenteJefe.volume = 0f;
        }
    }

    
    public void ReproducirJefe()
    {
        if (fuenteJefe != null)
        {
            
            fuenteJefe.time = 0f;
            if (!fuenteJefe.isPlaying)
            {
                fuenteJefe.Play();
            }
        }

        if (corrutinaTransicion != null) StopCoroutine(corrutinaTransicion);
        corrutinaTransicion = StartCoroutine(FundidoCruzado(fuenteNormal, fuenteJefe));
    }

    
    public void ReproducirNormal()
    {
        if (corrutinaTransicion != null) StopCoroutine(corrutinaTransicion);
        corrutinaTransicion = StartCoroutine(FundidoCruzado(fuenteJefe, fuenteNormal, true));
    }

    private IEnumerator FundidoCruzado(AudioSource audioBaja, AudioSource audioSube, bool detenerAlFinalizar = false)
    {
        float tiempo = 0f;

        float volInicioBaja = audioBaja != null ? audioBaja.volume : 0f;
        float volInicioSube = audioSube != null ? audioSube.volume : 0f;

        while (tiempo < duracionTransicion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracionTransicion;

            if (audioBaja != null)
            {
                audioBaja.volume = Mathf.Lerp(volInicioBaja, 0f, t);
            }

            if (audioSube != null)
            {
                audioSube.volume = Mathf.Lerp(volInicioSube, volumenMaximo, t);
            }

            yield return null;
        }

        if (audioBaja != null)
        {
            audioBaja.volume = 0f;
            if (detenerAlFinalizar) audioBaja.Stop();
        }

        if (audioSube != null) audioSube.volume = volumenMaximo;
    }
}