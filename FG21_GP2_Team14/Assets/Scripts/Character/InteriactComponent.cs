using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriactComponent : MonoBehaviour
{
    public Iinteriact Currnet { get; set; }

    public void Interiact()
    {
        if (Currnet != null)
        {
            Currnet.Interiact();
        }
    }

    public void SET(Iinteriact thing)
    {
        Currnet = thing;
    }
}
