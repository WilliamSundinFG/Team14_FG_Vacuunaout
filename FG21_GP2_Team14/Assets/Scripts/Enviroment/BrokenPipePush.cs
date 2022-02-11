using UnityEngine;

public class BrokenPipePush : MonoBehaviour
{
    private BrokenPipe Parent;

    private void Start()
    {
        Parent = gameObject.GetComponentInParent<BrokenPipe>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other) { return; }
        var otherObject = other.gameObject;

        if (!otherObject) { return; }
        if (!otherObject.GetComponent<Rigidbody>()) { return; }
        
        Parent.AddThingToBlow(otherObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other) { return; }
        var otherObject = other.gameObject;
        if (!otherObject) { return; }

        if (!otherObject.GetComponent<Rigidbody>()) { return; }
        Parent.RemoveThingToBlow(otherObject);
    }
    
}
