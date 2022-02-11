using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class TrashCancollector : MonoBehaviour
{
    private bool isOn = true;
    public GameObject pelet;
    public int Capacity = 10;
    private int amountOfSuckedItems = 0;
    public UnityEvent GeneratorDone;
    public UnityEvent SuckItem;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isOn) return;
        if (Equals(other.gameObject.tag ,pelet.tag))
        {
            addSuckedItem();
            SuckItem.Invoke();
            Destroy(other.gameObject);
        }    }

    void addSuckedItem()
    {
        amountOfSuckedItems++;
        progressBar();
    }
    
    void progressBar()
    {
        //TODO make progress bar work

        if (Capacity == amountOfSuckedItems)
        {
            FinnishedGenerator();
        }
    }
    
    
    private void FinnishedGenerator()
    {
        GeneratorDone.Invoke();
        isOn = false;
        gameObject.SetActive(false);
        Debug.Log("generator works");
    }
}
