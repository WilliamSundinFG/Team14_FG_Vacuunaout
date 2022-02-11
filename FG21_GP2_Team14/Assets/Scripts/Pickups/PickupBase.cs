using System.Collections;
using System.Collections.Generic;
using Team14;
using UnityEngine;
using UnityEngine.Events;

public class PickupBase : MonoBehaviour, Iinteriact
{
    public UnityEvent OnPickUpResponse;
    public void Interiact()
    {
        OnPickUpResponse.Invoke();
        Destroy(gameObject);
    }
}
