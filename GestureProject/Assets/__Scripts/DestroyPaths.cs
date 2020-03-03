using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPaths : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("DESTROY");
        if (other.CompareTag("river"))
        {
            //Debug.Log("PathwayDestroyed");
            Destroy(gameObject);

        }
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
