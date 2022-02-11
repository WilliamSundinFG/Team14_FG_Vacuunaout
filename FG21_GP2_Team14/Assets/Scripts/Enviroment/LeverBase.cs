using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverBase : MonoBehaviour, IpickupIntereact
{
    public UnityEvent InteractResponse, LeverTurnOnReaction;
    
    public void Interiact()
    {
        InteractResponse?.Invoke();
        
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //         Interiact();
    // }
    //
    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //         Interiact();
    // }
    
}
