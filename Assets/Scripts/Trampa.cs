using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            print("lava!");
            collision.transform.position = CheckPointSystem.instance.UltimaPos;
        }
    }
}
