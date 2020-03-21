using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Class which handles destruction of invalid objects
public class DestroyInvalidObstacles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("river"))
        {
            //destroy non river chunk gameobjects
            if (gameObject.tag == "VillageObstacle" || gameObject.tag == "ForestObstacle" || gameObject.tag == "trigger"){
                Destroy(gameObject);
            }

        }
        if(other.CompareTag("VillageChunk")){
            //destroy non village chunk gameobjects
            if (gameObject.tag == "RiverObstacle" || gameObject.tag == "ForestObstacle" || gameObject.tag == "trigger"){
                Destroy(gameObject);
            }
        }
        if(other.CompareTag("ForestChunk")){
            //destroy non forest chunk gameobjects
            if (gameObject.tag == "RiverObstacle" || gameObject.tag == "VillageObstacle"){
                Destroy(gameObject);
            }
        }
    }
}
