using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class handle to destrurction of paths
public class DestroyPaths : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if collides with river
        if (other.CompareTag("river"))
        {
            //destroy gameobject
            Destroy(gameObject);

        }
    }
}
