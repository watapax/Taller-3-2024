using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactuable : MonoBehaviour , Damagable
{
    public UnityEvent evento;

    public void TakeDamage(int damage)
    {
        evento.Invoke();
    }
}
