using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInvalidObstacles : MonoBehaviour
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
            if(gameObject.tag == "VillageObstacle" || gameObject.tag == "ForestObstacle" || gameObject.tag == "trigger"){
                Destroy(gameObject);
                Debug.Log("River: " + gameObject.name);
            }

        }
        if(other.CompareTag("VillageChunk")){
            if(gameObject.tag == "RiverObstacle" || gameObject.tag == "ForestObstacle" || gameObject.tag == "trigger"){
                Destroy(gameObject);
                Debug.Log("Village: " + gameObject.name);
            }
        }
        if(other.CompareTag("ForestChunk")){
            if(gameObject.tag == "RiverObstacle" || gameObject.tag == "VillageObstacle"){
                Destroy(gameObject);
                Debug.Log("Forest: " + gameObject.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
