using System;
using System.Collections;
using UnityEngine;


public class TrashCan : MonoBehaviour
{
    
    public float PushAwayRadius = 2f, secondsDelayOnSuck = 1f, forceToThrowObject = 10f;
    private bool isOn = true;
    private AudioManager _audioSource;
    
    private void Start()
    {
        _audioSource = AudioManager.Instance;
        _audioSource.PlayAudio(_audioSource.GameSounds.GeneratorSounds[0], AudioType.SFX,
            gameObject.transform.position, 1f, 1f, 5f, true);
    }
    
    public void PlayEatingSound()
    {
        _audioSource.PlayAudio(_audioSource.GameSounds.GeneratorSounds[1], AudioType.SFX);
    }
    public void ChangeAudioOnGenerator()
    {
        _audioSource.PlayAudio(_audioSource.GameSounds.GeneratorSounds[2], AudioType.SFX);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, PushAwayRadius);
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isOn) return;

        StartCoroutine(WaitForSeconds(secondsDelayOnSuck, collision));
    }


    
    void throwObjectAway(Collision collision)
    {
        collision?.rigidbody?.AddForce(Vector3.Normalize(collision.transform.position - transform.position) *
                                      forceToThrowObject * collision.rigidbody.mass, ForceMode.Impulse);
    }

    IEnumerator WaitForSeconds(float Time, Collision collision)
    {
        yield return new WaitForSeconds(Time);
        if (collision != null)
        {
            if (Vector3.Distance(collision.transform.position, transform.position) < PushAwayRadius)
            {
                throwObjectAway(collision);
            }
        }
    }

}
