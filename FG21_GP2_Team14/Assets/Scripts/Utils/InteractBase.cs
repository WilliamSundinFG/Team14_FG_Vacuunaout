using UnityEngine;
using UnityEngine.Events;

public class InteractBase : MonoBehaviour, Iinteriact
{
    public UnityEvent InteractResponse;
    public UnityEvent UnInteractResponse;
    public bool DestroyAfterInteract = true;

    public void Interiact()
    {
        InteractResponse?.Invoke();
        if(DestroyAfterInteract)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Interiact();
    }

    public void Uninteract()
    {
        UnInteractResponse?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Interiact();
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            Uninteract();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Uninteract();
    }
}
