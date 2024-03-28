using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Evento
{
    public string nombre;
    public UnityEvent evento;

    public void Invocar()
    {
        evento.Invoke();
    }
}

public class EventosAnimacion : MonoBehaviour
{
    public List<Evento> eventos = new List<Evento>();

    public void GatillarEvento(string nombre)
    {
        for(int i = 0; i < eventos.Count; i++)
        {
            if(nombre == eventos[i].nombre)
            {
                eventos[i].Invocar();
                break;
            }
        }
    }
}
