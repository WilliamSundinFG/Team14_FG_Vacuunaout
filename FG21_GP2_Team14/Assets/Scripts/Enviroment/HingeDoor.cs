using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeDoor : MonoBehaviour
{
    public float RotateAmount = 90f;
    private float originalRotation;
    public float rotateSpeed = 1;
    public bool doorOpen = false;
    
    
    void Start()
    {
        originalRotation = transform.rotation.eulerAngles.y;
    }
    void Update()
    {
        if (doorOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }    
    }
    private void OpenDoor()
    {
        if (originalRotation+RotateAmount > transform.rotation.eulerAngles.y)
        {
            transform.Rotate(0, rotateSpeed, 0, Space.Self);
        }
    }
    
    private void CloseDoor()
    {
        if (originalRotation < transform.rotation.eulerAngles.y)
        {
            transform.Rotate(0, -rotateSpeed, 0, Space.Self);
        }    
    }
}
