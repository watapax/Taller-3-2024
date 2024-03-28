using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sonidos/Libreria")]
public class LibreriaDeSonidos : ScriptableObject
{
    public AudioClip[] sonidos;

    public AudioClip clip
    {
        // devuelve un sonido aleatorio de la libreria
        get { return sonidos[Random.Range(0, sonidos.Length)]; }
    }

}
