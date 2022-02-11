using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SensorTrigger : MonoBehaviour
{
    
    public UnityEvent onTrigger;
    public UnityEvent<Iinteriact> onTriggerInterface;
    public Iinteriact stuff;
    
    
    private void OnTriggerEnter(Collider other)
    {
        InteriactComponent thing;
        if (other.CompareTag("Player"))
        {
            thing = other.GetComponent<InteriactComponent>();
            if (thing != null)
            {
                onTriggerInterface.AddListener(thing.SET); 
                TriggerInteriact();
            }
            Trigger();
        }  

       
    }
    private void OnTriggerExit(Collider other)
    {
        InteriactComponent thing;
        if (other.CompareTag("Player"))
        {
            thing = other.GetComponent<InteriactComponent>();
            if (thing != null)
            {
                onTriggerInterface.RemoveListener(thing.SET); 
            }
            Trigger();
        }    
    }

    private void Trigger()
    {
        onTrigger.Invoke();
        Debug.Log("Open Sesmami");
    }
    
    private void TriggerInteriact()
    {
        onTriggerInterface.Invoke(stuff);
        Debug.Log("Open Sesmami");
    }
    

    
}


