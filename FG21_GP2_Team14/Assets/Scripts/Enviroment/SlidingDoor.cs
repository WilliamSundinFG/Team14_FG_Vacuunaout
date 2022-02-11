using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class SlidingDoor : MonoBehaviour
{
    private Vector3 startPosDoorRight, stopPosDoorRight;
    private Vector3 startPosDoorLeft, stopPosDoorLeft;


    public GameObject doorRight, doorLeft;

    private float moveOffSetFunk = 0f;
    [Range(-2.5f, 2.5f)]public float moveOffset;
    public bool doorOpen, lockDoor = false;
    private Vector3 sizeOfGameObjectDoorRight, sizeOfGameObjectDoorLeft;
    [Range(0f,1f)]public float moveDoor = 0f;
    private float minValue, maxValue;
    

    
    private void Start()
    {
        sizeOfGameObjectDoorRight = doorRight.GetComponent<Collider>().bounds.size;
        sizeOfGameObjectDoorLeft = doorLeft.GetComponent<Collider>().bounds.size;
        
        moveOffSetFunk = DoorAngle(gameObject.GetComponentInParent<Transform>().rotation.eulerAngles.y);
        
        var position1 = doorLeft.transform.localPosition;
        startPosDoorLeft = new Vector3(position1.x , position1.y, position1.z+ sizeOfGameObjectDoorLeft.z + moveOffset +moveOffSetFunk);
        stopPosDoorLeft = position1;
        
        var position = doorRight.transform.localPosition;
        startPosDoorRight = new Vector3(position.x , position.y, position.z+ (sizeOfGameObjectDoorRight.z + moveOffset+ moveOffSetFunk)*-1);
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
       doorLeft.transform.localPosition = Vector3.Lerp(stopPosDoorLeft, startPosDoorLeft, moveDoor);
       doorRight.transform.localPosition = Vector3.Lerp(stopPosDoorRight, startPosDoorRight, moveDoor);;
       moveDoor += 0.01f;
       moveDoor = Mathf.Clamp(moveDoor, 0f, 1f);
    }

    void CloseDoor()
    {
        
        
        if (moveDoor < 0)
        {
            return;
        }
        doorLeft.transform.localPosition = Vector3.Lerp(stopPosDoorLeft, startPosDoorLeft, moveDoor); 
        doorRight.transform.localPosition = Vector3.Lerp(stopPosDoorRight, startPosDoorRight, moveDoor); 
        
        moveDoor-= 0.01f;
        moveDoor = Mathf.Clamp(moveDoor, 0f, 1f); 
        
    }

    float DoorAngle(float doorRotation)
    {
        doorRotation = Mathf.Abs(doorRotation % 360);
        
        if(Math.Abs(doorRotation - 90f) < 0.1 || Math.Abs(doorRotation - 270f) < 0.1)
        {
            return 2.5f;
        }
        if (doorRotation <90 && doorRotation > 0)
        {
             if(Math.Abs(doorRotation - 90f) < 0.1)
             {
                 return 2.5f;
             }
             
             doorRotation %= 90;
             
            
             if (doorRotation == 0)
             {
                 return doorRotation;
             }
             doorRotation = Mathf.InverseLerp(0f,90f, doorRotation);
             doorRotation = Mathf.Lerp(0f,2.5f,doorRotation);
             return doorRotation;
        }

        if (doorRotation <180 && doorRotation > 90)
        {
            if(Math.Abs(doorRotation - 90f) < 0.1)
            {
                return 2.5f;
            }
                    
            doorRotation %= 90;
                    
            
            if (doorRotation == 0)
            {
                return doorRotation;
            }
            doorRotation = Mathf.InverseLerp(0f,90f, doorRotation);
            doorRotation = Mathf.Lerp(2.5f,0f,doorRotation);
            return doorRotation;
            
        }
        if (doorRotation <270 && doorRotation > 180)
        {
            if(Math.Abs(doorRotation - 90f) < 0.1)
            {
                return 2.5f;
            }
                    
            doorRotation %= 90;
                    
            
            if (doorRotation == 0)
            {
                return doorRotation;
            }
            doorRotation = Mathf.InverseLerp(0f,90f, doorRotation);
            doorRotation = Mathf.Lerp(0f,2.5f,doorRotation);
            return doorRotation;
        }
        if (doorRotation <360 && doorRotation > 270)
        {
            if(Math.Abs(doorRotation - 90f) < 0.1)
            {
                return 2.5f;
            }
                    
            doorRotation %= 90;
                    
            
            if (doorRotation == 0)
            {
                return doorRotation;
            }
            doorRotation = Mathf.InverseLerp(0f,90f, doorRotation);
            doorRotation = Mathf.Lerp(2.5f,0f,doorRotation);
            return doorRotation;
        }

        return 0;
    }
    
    
}
