using UnityEngine;

public class PhysicsCleanable : CleanableBase
{
    public override void Setup()
    {
        if (!GetComponent<Rigidbody>()) 
        {
            var rigidbody=gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }
    }

    public override void DoClean(Equipment equipment, CleanableBase cleanable)
    {
        OnDestruction.Invoke(this);
    }
}
