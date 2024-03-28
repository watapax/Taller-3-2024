using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrevenirDispararPiso : MonoBehaviour
{
    bool armaEnPiso;

    public bool ArmaEnPiso
    {
        get { return armaEnPiso; }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

            armaEnPiso = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

            armaEnPiso = false;
    }
}
