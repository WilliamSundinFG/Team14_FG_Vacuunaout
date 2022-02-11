using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BrokenPipe : MonoBehaviour
{
    public GameObject BrokenHole;
    public float gasForce = 10f;
    public bool isOn = true;
    private List<GameObject> thingsToBlowAway;
    [FormerlySerializedAs("ItDoBeCleanAction")] public DoCleanAction CleanActoinIgnoringPipePushForce;


    private void Start()
    {
        thingsToBlowAway = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        for (int i = thingsToBlowAway.Count - 1; i >= 0; i--)
        {
            if (thingsToBlowAway[i] == null) { continue; }
            
            if (thingsToBlowAway[i] == null) { return; }
            
            AddForceInDirection(BrokenHole.transform.position , thingsToBlowAway[i]);
        }
    }

 

    public void AddThingToBlow(GameObject thing)
    {
        thingsToBlowAway.Add(thing);
        Debug.Log("Add things");
    }
    
    public void RemoveThingToBlow(GameObject thing)
    {
        thingsToBlowAway.Remove(thing);
        Debug.Log("Revmove things");

    }

    
    private void AddForceInDirection(Vector3 origin, GameObject obj)
    {


            var objPos = obj.transform.position;
            var outDir = objPos - origin;
            var flowDir = ( outDir).normalized;
            var rigidbody = obj.GetComponent<Rigidbody>();
            if (!rigidbody) {return; }
            
            var thing = obj.gameObject.GetComponent<CleanableBase>();
            if (thing) { if (thing.CleanAction == CleanActoinIgnoringPipePushForce) {return; }}
            
            
            if (isOn)
            {
                rigidbody.AddForce(new Vector3(flowDir.x,flowDir.y, flowDir.z)*gasForce,ForceMode.Impulse);
            }
    }

    public void TurnOffBlow()
    {
        isOn = false;
    }
    
    public void TurnOnBlow()
    {
        isOn = true; 
    }
}