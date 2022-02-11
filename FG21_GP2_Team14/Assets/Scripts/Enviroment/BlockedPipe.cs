using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedPipe : MonoBehaviour
{
    public DoCleanAction ItDoBeCleanAction;
    private BrokenPipe Parent;
    public GameObject DisablePushEffect;

    private void Start()
    {
        Parent = gameObject.GetComponentInParent<BrokenPipe>();
    }


    public void MakeAffectDissapere()
    {
        DisablePushEffect.SetActive(false);
    }
    public void MakeAffectApere()
    {
        DisablePushEffect.SetActive(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other) { return; }
        var thing = other.gameObject.GetComponent<CleanableBase>();
        if (!thing) { return; }
        if (thing.CleanAction == ItDoBeCleanAction)
        {
            Parent.TurnOnBlow();
            MakeAffectApere();
        }    
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other) { return; }
        var thing = other.gameObject.GetComponent<CleanableBase>();
        if (!thing) { return; }
        if (thing.CleanAction == ItDoBeCleanAction)
        {
            
            Parent.TurnOffBlow();
            MakeAffectDissapere();
        }    
    }
}
