using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotate : MonoBehaviour
{
    public Vector3 objectRotation;
    public float rotationSpeed;

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(objectRotation * rotationSpeed * Time.deltaTime);
    }
}
