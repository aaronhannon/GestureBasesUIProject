using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private GameObject destructionPoint;
    // Start is called before the first frame update
    void Start()
    {
        //Find object by name so doesnt need attachment to each platform;
        destructionPoint = GameObject.Find("ObjDestructionTransform");
    }

    // Update is called once per frame
    void Update()
    {
        //if platforms z position is less than the destruction points z position.
        if (transform.position.z < destructionPoint.transform.position.z)
        {
            //Destroy the object the script is attached to
            Destroy(gameObject);
        }
    }
}
