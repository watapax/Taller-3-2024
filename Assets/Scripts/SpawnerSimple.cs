using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSimple : MonoBehaviour
{
    public Transform spawnPosition;
    public GameObject spawnObject;

    public void Spawnear()
    {
        Instantiate(spawnObject, spawnPosition.position, spawnPosition.rotation);
    }
}
