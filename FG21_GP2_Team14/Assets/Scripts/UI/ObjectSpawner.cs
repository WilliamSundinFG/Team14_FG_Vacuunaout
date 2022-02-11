using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject object_0;
    public GameObject object_1;
    public GameObject object_2;
    public GameObject object_3;
    public GameObject object_4;
    public GameObject object_5;
    public GameObject object_6;
    public GameObject object_7;

    List<GameObject> objectList = new List<GameObject>();
    
    [SerializeField] Vector3 spawnPos;
    [SerializeField] Vector3 spawnSize;

    private float _counter = 0;
    void Start()
    {
        objectList.Add(object_0);
        objectList.Add(object_1);
        objectList.Add(object_2);
        objectList.Add(object_3);
        objectList.Add(object_4);
        objectList.Add(object_5);
        objectList.Add(object_6);
        objectList.Add(object_7);
    }


    void Update()
    {
        _counter += 0.01f;
        Debug.Log(_counter);
        if(_counter >= 5)
        {
            int objectNumber = Random.Range(0, 7);

            GameObject newObject = objectList[objectNumber];

            GameObject clone = Instantiate(newObject, spawnPos, Quaternion.identity);

            //if(clone.GetComponent<Rigidbody>() != null)
            //{
            //    Destroy(clone.GetComponent<Rigidbody>());
            //}

            if (clone.transform.GetComponentsInChildren<Rigidbody>() != null)
            {
                Destroy(clone.GetComponent<Rigidbody>());
            }

            if (clone.transform.GetComponentsInChildren<MeshCollider>() != null)
            {
                Destroy(clone.GetComponent<MeshCollider>());
            }
            if (clone.transform.GetComponentsInChildren<BoxCollider>() != null)
            {
                Destroy(clone.GetComponent<BoxCollider>());
            }

            Rigidbody newBody = clone.AddComponent<Rigidbody>();
            newBody.useGravity = false;
            newBody.AddForce(new Vector3(-50, 0, 0), ForceMode.Impulse);
            newBody.AddTorque(new Vector3(-50, -50, -50), ForceMode.Impulse);



            clone.transform.localScale = spawnSize;
            _counter = 0;
        }
    }
}
