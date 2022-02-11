using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Generator : MonoBehaviour, Iinteriact
{
    public GameObject pellet, pointOfSpawn;
    public UnityEvent InteractResponse;

    public void Interiact()
    {
        InteractResponse?.Invoke();
        Destroy(gameObject);
    }

    public void SpawnBigPellet()
    {
        Instantiate(pellet, pointOfSpawn.transform.position, Quaternion.identity);
    }
    
}
