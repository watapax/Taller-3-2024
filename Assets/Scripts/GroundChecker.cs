using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    ControlesPlayer controlesPlayer;

    private void Awake()
    {
        controlesPlayer = GetComponentInParent<ControlesPlayer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Piso"))
            controlesPlayer.HaySuelo(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Piso"))
            controlesPlayer.HaySuelo(false);
    }
}
