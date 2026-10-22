using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager instance;

    [Header("Componente de Audio")]
    private AudioSource audioSource;

    [Header("Clips de Sonido")]
    public AudioClip sonidoSalto;
    public AudioClip sonidoDisparo;
    public AudioClip sonidoImpactoEnemigo;
    public AudioClip sonidoAlertaJefe;
    public AudioClip sonidoObjetoEspecial;

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

        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    public void ReproducirSalto()
    {
        if (sonidoSalto != null)
        {
            audioSource.PlayOneShot(sonidoSalto);
        }
    }

    public void ReproducirDisparo()
    {
        if (sonidoDisparo != null)
        {
            audioSource.PlayOneShot(sonidoDisparo);
        }
    }

    public void ReproducirImpacto()
    {
        if (sonidoImpactoEnemigo != null)
        {
            audioSource.PlayOneShot(sonidoImpactoEnemigo);
        }
    }
    public void ReproducirAlertaJefe()
    {
        if (sonidoAlertaJefe != null)
        {
            audioSource.PlayOneShot(sonidoAlertaJefe);
        }
    }
    public void ReproducirObjetoEspecial()
    {
        if (sonidoObjetoEspecial != null)
        {
            audioSource.PlayOneShot(sonidoObjetoEspecial);
        }
    }
}
