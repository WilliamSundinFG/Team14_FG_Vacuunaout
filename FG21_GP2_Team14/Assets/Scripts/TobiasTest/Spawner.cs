using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Spawnable;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn(.5f));
    }

    IEnumerator Spawn(float timeDelay)
    {
        while (true)
        {
            Instantiate(Spawnable, transform);
            yield return new WaitForSeconds(timeDelay);
        }
    }
}
