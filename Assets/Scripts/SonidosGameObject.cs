using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosGameObject : MonoBehaviour
{
    public LibreriaDeSonidos libreriaDeSonido;

    public void ReproducirSonido()
    {
        SoundFXManager.instance.ReproducirSFX(libreriaDeSonido);
    }
}
