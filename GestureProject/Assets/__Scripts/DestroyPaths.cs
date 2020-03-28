using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class handle to destrurction of paths
public class DestroyPaths : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If collides with river
        // Makes sure paths aren't spawned in the river.
        if (other.CompareTag("river"))
        {
            //destroy gameobject
            Destroy(gameObject);
        }
    }
}
