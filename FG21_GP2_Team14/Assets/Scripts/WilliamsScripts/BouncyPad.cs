using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPad : MonoBehaviour
{
    public float suckRadius = 1f, secondsDelayOnSuck = 1f, forceToThrowObject = 1f;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Text");
        StartCoroutine(WaitForSeconds(secondsDelayOnSuck, collision));
    }
    

    void throwObjectAway(Collision collision)
    {
        Vector3 throwVector = collision.transform.position - transform.position;
        throwVector = Vector3.Normalize(throwVector);
        collision.rigidbody.AddForce(throwVector * forceToThrowObject, ForceMode.Impulse);
    }

    IEnumerator WaitForSeconds(float Time, Collision collision)
    {
        yield return new WaitForSeconds(Time);
        if (Vector3.Distance(collision.transform.position, transform.position) < suckRadius)
        {
            throwObjectAway(collision);
        }
        
    }
}
