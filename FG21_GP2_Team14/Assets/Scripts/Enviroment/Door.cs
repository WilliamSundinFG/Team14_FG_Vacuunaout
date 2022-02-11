using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Transform PivotPoint;
    private List<Transform> children;
    public float degreeToOpenDoor = 90;
    public SideOfDoor sideOfDoor = SideOfDoor.Right;
    public bool OpenClosed = false;
    private Quaternion startingAngle;

    
    public Vector3 axis = Vector3.right;

    public enum SideOfDoor
    {
        Top,
        Bottom,
        Right,
        Left
    }
    // Start is called before the first frame update
    void Start()
    {
        children = new List<Transform>();

        getChildren();
        DoorCenter();

    }

    private void Update()
    {
        //DoorCenter();
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (OpenClosed)
        {
            if (transform.rotation.eulerAngles.y < degreeToOpenDoor)
            {
                transform.RotateAround(PivotPoint.position, axis, degreeToOpenDoor* Time.deltaTime);
            }
        }
    }
    

    void DoorCenter()
    {
        switch (sideOfDoor)
        {
            case SideOfDoor.Top:
                axis = Vector3.forward;
                PivotPoint = children[0];
                startingAngle = gameObject.transform.rotation;
                break;
            
            case SideOfDoor.Bottom:
                axis = Vector3.back;
                PivotPoint = children[1];
                startingAngle = gameObject.transform.rotation;
                break;
            
            case SideOfDoor.Right:
                axis = Vector3.up;
                PivotPoint = children[2];
                startingAngle = gameObject.transform.rotation;
                break;
            
            case SideOfDoor.Left:
                axis =Vector3.down;
                PivotPoint = children[3];
                startingAngle = gameObject.transform.rotation;
                break;
            
        }
        
    }

    void getChildren()
    {
        foreach (Transform childThing in gameObject.GetComponentInChildren<Transform>())
        {
            children.Add(childThing);
        }
    }

}
