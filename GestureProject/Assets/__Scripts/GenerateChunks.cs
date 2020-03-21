using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateChunks : MonoBehaviour
{
    GameObject villageChunk;
    GameObject forestChunk;
    GameObject riverChunk;
    GameObject riverChunkNoBoat;
    float currentZ = 0;
    string previousChunk;
    ArrayList chunks;
    ArrayList chunkObjects;
    int chunksIndex;
    // Start is called before the first frame update
    void Start()
    {
        chunksIndex = 0;
        chunks = new ArrayList();
        chunkObjects = new ArrayList();
        //Hardcode order you want to test chunks
        int[] ChunkPatterns = { 0, 2, 1};
        
        villageChunk = Resources.Load("VillageChunk") as GameObject;
        forestChunk = Resources.Load("ForestChunk") as GameObject;
        riverChunk = Resources.Load("RiverChunkPrefab") as GameObject;
        riverChunkNoBoat = Resources.Load("RiverChunkPrefabNoBoat") as GameObject;
        //int rand = 0;
        for (int i = 0; i < ChunkPatterns.Length; i++)
        {
            int rand = ChunkPatterns[i];
            // Debug.Log("RANDOM: " + rand);
            //VILLAGE
            if (rand == 0)
            {
                GenerateVillage();
            }
            //FOREST
            else if (rand == 1)
            {

                GenerateForest();
            }
            //RIVER
            else if (rand == 2)
            {
                GenerateRiver();

            }

            
            if (rand == 2 && previousChunk == "river")
            {
                rand = 1;
            }



        }

        //spawn village Add 360 every time to Z coord

        Debug.Log("Chunks Generated");
        int count =0;
        foreach (GameObject item in chunkObjects)
        {
            Debug.Log(item.ToString());

            // if(count == 0){
            //     Destroy(item);
            // }
            // count++;
        }
        
        gameObject.AddComponent<ObstacleGenerator>();

    }

    public void GenerateChunk(){
            int rand = Random.Range(0, 3);
            
            if (rand == 2 && previousChunk == "river")
            {
                rand = 1;
            }

            if (rand == 0)
            {
                GenerateVillage();
                gameObject.GetComponent<ObstacleGenerator>().GenerateAllObstacles(0);
            }
            //FOREST
            else if (rand == 1)
            {
                GenerateForest();
                gameObject.GetComponent<ObstacleGenerator>().GenerateAllObstacles(1);
            }
            //RIVER
            else if (rand == 2)
            {
                GenerateRiver();
                gameObject.GetComponent<ObstacleGenerator>().GenerateAllObstacles(2);
            }

            //rand = Random.Range(0, 3);

            // if(chunksIndex == 0){
            //     GenerateVillage();
            //     gameObject.GetComponent<ObstacleGenerator>().GenerateVillageObjects();
            //     chunksIndex = 2;
            // }else if(chunksIndex == 1){
            //     GenerateForest();
            //     gameObject.GetComponent<ObstacleGenerator>().GenerateForestObjects();
            //     chunksIndex = 0;
            // }else if(chunksIndex == 2){
            //     GenerateRiver();
            //     gameObject.GetComponent<ObstacleGenerator>().GenerateRiverObjects();
            //     chunksIndex = 1;
            // }

            
    }

    private void GenerateVillage(){
        if (previousChunk == "forest")
        {
            currentZ += 450f;
        }
        else if (previousChunk == "river")
        {
            currentZ += 850;
        }
        else
        {
            currentZ += 350f;
        }

        GameObject villageClone = Instantiate(villageChunk, new Vector3(-18f, 1f, currentZ), Quaternion.Euler(0, 90, 0));
        chunkObjects.Add(villageClone);

        chunks.Add(0);
        previousChunk = "village";
    }

    private void GenerateForest(){
        if (previousChunk == "river")
        {
            currentZ += 680;
        }
        else
        {
            currentZ += 230;
        }

        GameObject forestClone = Instantiate(forestChunk, new Vector3(-22f, 0f, currentZ), Quaternion.Euler(0, 90, 0));
        chunkObjects.Add(forestClone);

        
        chunks.Add(1);  
        previousChunk = "forest";
    }

    private void GenerateRiver(){

        GameObject riverClone;
        

        if (previousChunk == "forest")
        {
            currentZ += 250;
            riverClone = Instantiate(riverChunk, new Vector3(17.57f, 3f, currentZ), Quaternion.Euler(0, -100.92f, 0));
        }
        else if (previousChunk == "village")
        {
            currentZ += 180;
            riverClone = Instantiate(riverChunk, new Vector3(17.57f, 3f, currentZ), Quaternion.Euler(0, -100.92f, 0));

        }
        else if (previousChunk == "river")
        {
            currentZ += 650;
            riverClone = Instantiate(riverChunkNoBoat, new Vector3(17.57f, 3f, currentZ), Quaternion.Euler(0, -100.92f, 0));
        }
        else
        {
            currentZ = 110f;
            riverClone = Instantiate(riverChunk, new Vector3(17.57f, 3f, currentZ), Quaternion.Euler(0, -100.92f, 0));
        }
        chunkObjects.Add(riverClone);
        chunks.Add(2);
        previousChunk = "river";
    }

    public ArrayList GetChunkOrder()
    {
        return chunks;
    }

    // Update is called once per frame
    void Update()
    {

    }
}