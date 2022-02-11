using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Container : MonoBehaviour
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
    
    
    public UnityEvent ContainerFilled;
    private int counter = 0;
    public int FilledContainer = 1;
    private IEnumerator coriutine;
    private bool lightOn = false;
    public Renderer myRenderer;
    
    
    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        sizeOfGameObjectDoorRight = doorRight.GetComponent<Collider>().bounds.size;
        sizeOfGameObjectDoorLeft = doorLeft.GetComponent<Collider>().bounds.size;
        
        moveOffSetFunk = DoorAngle(gameObject.GetComponentInParent<Transform>().rotation.eulerAngles.x);
        
        var position1 = doorLeft.transform.localPosition;
        startPosDoorLeft = new Vector3(position1.x + (sizeOfGameObjectDoorLeft.x + moveOffset +moveOffSetFunk)*100f  , position1.y, position1.z);
        stopPosDoorLeft = position1;
        
        var position = doorRight.transform.localPosition;
        startPosDoorRight = new Vector3(position.x+ ((sizeOfGameObjectDoorRight.x + moveOffset+ moveOffSetFunk)*-1)*100f , position.y, position.z);
        stopPosDoorRight = position;
        LightTurnOff();
    }

    public void LightTurnOn()
    {
        myRenderer.material.EnableKeyword("_EmissionColor");
        myRenderer.material.SetColor("_EmissionColor", Color.yellow);
    }

    public void LightTurnOff()
    {
        myRenderer.material.DisableKeyword("_EmissionColor");
        myRenderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
        myRenderer.material.SetColor("_EmissionColor", Color.black);
    }
    public void Sensor(Collider other)
    {
        if (other.CompareTag("FuelPellet"))
        {
            counter++;
            if (counter == FilledContainer)
            {
                ContainerFilled.Invoke();
                ContainerDone(other);
                return;
            }
        }

        if (!other.CompareTag("PhysicsObject")) return;
        Destroy(other.gameObject);
    }

    void ContainerDone(Collider other)
    {
        DoorChangeState();
        LightTurnOn();
        Destroy(other.gameObject, 1.5f);
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
