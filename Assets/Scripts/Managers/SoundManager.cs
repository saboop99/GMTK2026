using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Fontes de Áudio")]
    public AudioSource musicaSource;
    public AudioSource sfxSource;

    [Header("Clipes")]
    public AudioClip somDerrota;
    public AudioClip somVitoria;

    public void TocarSomDerrota()
    {
        if (somDerrota != null)
            sfxSource.PlayOneShot(somDerrota);
    }

    public void TocarSomVitoria()
    {
        if (somVitoria != null)
            sfxSource.PlayOneShot(somVitoria);
    }
}