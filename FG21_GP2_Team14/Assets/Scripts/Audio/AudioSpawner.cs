using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AudioSpawner : MonoBehaviour
{
    public AudioClipList ClipList;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void OnEnable()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = ClipList.AmbientSounds[Random.Range(0, ClipList.AmbientSounds.Count)];
        StartCoroutine(PlayNewSound());
    }

    private IEnumerator PlayNewSound()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            Debug.Log("Playing sound after delay");
        }
    }

    private void OnDisable()
    {
        StopCoroutine(PlayNewSound());
        Destroy(audioSource);
    }
}
