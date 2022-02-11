using System.Collections;
using System.Collections.Generic;
using Team14;
using UnityEngine;

public class CollisionTaskUpdater : MonoBehaviour, ITaskInvoker
{
    public event TaskTrigger Trigger;
    public Team14.CharacterController Controller;
    public List<ScriptableTaskBase> CollisionTasks;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Team14.CharacterController>() != null)
            CollisionTasks.ForEach(t => t.Update());
    }
}
