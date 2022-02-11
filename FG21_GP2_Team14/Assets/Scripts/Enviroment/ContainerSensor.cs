using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSensor : MonoBehaviour
{
    public Container container;

    private void OnTriggerEnter(Collider other)
    {
        container.Sensor(other);
    }
}
