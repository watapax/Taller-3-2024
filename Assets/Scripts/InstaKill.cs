using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstaKill : MonoBehaviour
{
    public GameObject sangre;
    public UnityEvent onContact;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            onContact.Invoke();
            Instantiate(sangre, collision.GetContact(0).point, Quaternion.identity);
            collision.gameObject.GetComponent<ControlesPlayer>().transform.position = CheckPointSystem.instance.UltimaPos;
        }
    }
}
