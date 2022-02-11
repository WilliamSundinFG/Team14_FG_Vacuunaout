using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroyer : MonoBehaviour
{
    public void DestroyAfterSeconds(float sec)
    {
        StartCoroutine(DestroySelf(sec));
    }

    private IEnumerator DestroySelf(float sec)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(sec);
            Destroy(gameObject);
        }
    }
}
