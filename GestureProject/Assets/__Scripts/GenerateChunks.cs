using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateChunks : MonoBehaviour
{
    GameObject villageChunk;
    GameObject forestChunk;
    float currentZ= 0 ;
    string previousChunk;

    // Start is called before the first frame update
    void Start()
    {
        villageChunk = Resources.Load("VillageChunk") as GameObject;
        forestChunk = Resources.Load("ForestChunk") as GameObject;
        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, 2);
            Debug.Log("RANDOM: " + rand);
            if(rand == 0)
            {
                if(previousChunk == "forest")
                {
                    currentZ += 450f;
                }
                else
                {
                    currentZ += 350f;
                }
                
                Instantiate(villageChunk, new Vector3(-18f, 1f, currentZ), Quaternion.Euler(0, 90, 0));
                previousChunk = "village";
            }
            else
            {

                currentZ += 230;
                Instantiate(forestChunk, new Vector3(-22f, 0f, currentZ), Quaternion.Euler(0, 90, 0));
                previousChunk = "forest";
            }


        }

        //spawn village Add 360 every time to Z coord

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
