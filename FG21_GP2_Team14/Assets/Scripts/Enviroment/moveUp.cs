using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUp : MonoBehaviour
{
private Vector3 startPosDoorRight, stopPosDoorRight;
    private Vector3 startPosDoorLeft, stopPosDoorLeft;


    public GameObject door;

    private float moveOffSetFunk = 0f;
    public float moveUpAmount;
    public bool doorOpen, lockDoor = false;
    private Vector3 sizeOfGameObjectDoor, sizeOfGameObjectDoorLeft;
    [Range(0f,1f)]public float moveDoor = 0f;
    private float minValue, maxValue;
    

    
    private void Start()
    {
        sizeOfGameObjectDoor = door.GetComponent<Collider>().bounds.size;
        
        var position = door.transform.localPosition;
        startPosDoorRight = new Vector3(position.x , position.y+ (sizeOfGameObjectDoor.y + moveUpAmount+ moveOffSetFunk), position.z);
        stopPosDoorRight = position;
    }


    public void DoorChangeState()
    {
        doorOpen = !doorOpen;
    }
    
    
    public void LockDoorChangeState()
    {
        lockDoor = !lockDoor;
    }

    private void FixedUpdate()
    {
        
        if (lockDoor)
            return;
        
        
        if (doorOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
       // minValue/maxValue

       if (moveDoor > 1f)
       {
           return;
       }
       door.transform.localPosition = Vector3.Lerp(stopPosDoorRight, startPosDoorRight, moveDoor);;
       moveDoor += 0.01f;
       moveDoor = Mathf.Clamp(moveDoor, 0f, 1f);
    }

    void CloseDoor()
    {
        
        
        if (moveDoor < 0)
        {
            return;
        }
        door.transform.localPosition = Vector3.Lerp(stopPosDoorRight, startPosDoorRight, moveDoor); 
        
        moveDoor-= 0.01f;
        moveDoor = Mathf.Clamp(moveDoor, 0f, 1f); 
        
    }

    
    
}
