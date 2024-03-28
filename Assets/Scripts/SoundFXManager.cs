using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    AudioSource audioSource;
    public static SoundFXManager instance;

    private void Awake()
    {
        if (SoundFXManager.instance != null) Destroy(gameObject);
        else instance = this;

        audioSource = GetComponent<AudioSource>();
    }
    public void ReproducirSFX(LibreriaDeSonidos lib)
    {
        audioSource.PlayOneShot(lib.clip);
    }

    public void ReproducirSFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
