using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColisionesPlayer : MonoBehaviour
{
    public UnityEvent onEnter, onStay, onExit;
    public string tag;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // SI el collider que acaba de entrar
        // ¿Tiene el tag "Daño"?
        // entonces ejecuta el codigo
        if (collision.CompareTag(tag))
        {
            print("ENTER");
            onEnter.Invoke();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            print("STAY");
            onStay.Invoke();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            print("EXIT");
            onExit.Invoke();
        }


    }


}
