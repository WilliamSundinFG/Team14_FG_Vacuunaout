using UnityEngine;

public class DrainedCleanable : CleanableBase
{
    [Range(1f,10f)]
    public float CleanTime = 2f;

    public override void Setup()
    {
        
    }

    public override void DoClean(Equipment equipment, CleanableBase cleanable)
    {
        CleanTime = Mathf.Clamp(CleanTime - Time.deltaTime, 0f, 10f);
        if (CleanTime <= 0.0f) { OnDestruction.Invoke(this); }
    }
}
