using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//Class which generates obstacles in game
public class ObstacleGenerator : MonoBehaviour
{
    private static int numberOfChunks;
    private const int chunkLenghtVillage = 400;
    private const int chunkLenghtRiver = 680;
    private const int chunkLenghtForest = 300;
    private int start = 0;
    private int end = 0;
    private bool firstChunk = true;
    private int previousChunkLength = 0;
    private ArrayList chunks;
    private GameObject pathway;
    private int currentChunk = 0;
    private int previousChunkValue;
    // Dictionary which holds all game objects in memory rather than loading them all the time.
    private Dictionary<string, GameObject> gameObstacles = new Dictionary<string, GameObject>();

    private GameObject container;
    //set up 3 spawnpoints for lane positions
    private float[] spawnpoints = { 2.5f, 0f, -2.5f };
    void Start()
    {
        pathway = GameObject.Find("pathway");
        // Reset on each game load.
        numberOfChunks = 1;

        LoadAllGameObjects();

        //load all generated objects
        container = GameObject.Find("GeneratedObjects");

        //load chunks
        chunks = gameObject.GetComponent<GenerateChunks>().GetChunkOrder();
        
        foreach (int item in chunks)
        {
            switch (item)
            {
                case 0:
                    //generate village
                    GenerateAllObstacles(0);
                    break;
                case 1:
                    //generate forest
                    GenerateAllObstacles(1);
                    break;
                case 2:
                    //generate river
                    GenerateAllObstacles(2);
                    break;
            }
        }
    }

    //generate obsticles based on chunk value given
    public void GenerateAllObstacles(int chunkValue){
        if(chunkValue == 0){
            //update start and end values for village chunk
            UpdateStartEndValues(chunkLenghtVillage);
        }else if(chunkValue == 1){
            //update start and end values for forest chunk
            UpdateStartEndValues(chunkLenghtForest);
        }else if(chunkValue == 2){
            //update start and end values for river chunk
            UpdateStartEndValues(chunkLenghtRiver);
        }
        
        int randomIndex;
        //chunkValue == 0 is a village, in villages spawn npc's
        if (chunkValue == 0)
        {
            for (int i = start + 80; i < end; i += 100)
            {
                randomIndex = Random.Range(0, 3);
                // Instaniate and set start point of NPC so they only run to the start of the village chunk.
                GameObject npc = Instantiate(gameObstacles["NPC_Man"], new Vector3(spawnpoints[randomIndex], 1f, i), Quaternion.Euler(0, -180, 0));
                npc.transform.parent = container.transform;

                Debug.Log("Position" + start);
                npc.GetComponent<MoveNPC>().StartPosition = new Vector3(0, 1f, start);
            }
        }

        //generate fences
        for (int i = start; i < end - 30; i += 27)
        {
            randomIndex = Random.Range(0, 3);
            Instantiate(gameObstacles["fence"], new Vector3(spawnpoints[randomIndex], 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        //generate rocks
        for (int i = start+50; i < end-30; i += 35)
        {
            randomIndex = Random.Range(0, 3);
            Instantiate(gameObstacles["rock"], new Vector3(spawnpoints[randomIndex], 1.2f, i), Quaternion.identity).transform.parent = container.transform;
        }

        //generate logs
        for (int i = start + 30; i < end - 20; i += 50)
        {
            randomIndex = Random.Range(0, 3);
            Instantiate(gameObstacles["logs"], new Vector3(spawnpoints[randomIndex], 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        // Spawns trees.
        for (int i = start; i < end - 30; i += 100)
        {
            int rand = Random.Range(0, 2);

            // Load either left or right falling trees
            if (rand == 0)
            {
                Instantiate(gameObstacles["TreeRight"], new Vector3(0.5f, 2.7f, i), Quaternion.identity).transform.parent = container.transform;
            }
            else
            {
                Instantiate(gameObstacles["TreeLeft"], new Vector3(0f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
            }
        }

        //spawn in collectables and pathways
        SpawnCoins(start, end);
        SpawnPotions(start, end);
        GeneratePathway(start, end);
        SpawnHelmets(start,end);
        //increase chunks
        currentChunk++;
        numberOfChunks++;
    }

    #region == Chunk Object Generators == 
    public void GenerateVillageObjects()
    {
        // Get start and end values to load objects from, so it aligns with each chunk.
        UpdateStartEndValues(chunkLenghtVillage);

        //generate NPC's
        for (int i = start; i < end; i += 100)
        {
            // Instaniate and set start point of NPC so they only run to the start of the village chunk.
            GameObject npc = Instantiate(gameObstacles["NPC_Man"], new Vector3(Random.Range(-2.5f, 2.5f), 1f, i), Quaternion.Euler(0, -180, 0));
            npc.transform.parent = container.transform;
            npc.GetComponent<MoveNPC>().StartPosition = new Vector3(start, 1f, 0f);
        }
        
        //generate fences
        for (int i = start; i < end - 30; i += 27)
        {
            Instantiate(gameObstacles["fence"], new Vector3(Random.Range(-2.5f, 2.5f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        //spawn in collectables and pathways
        SpawnCoins(start, end);
        SpawnPotions(start, end);
        GeneratePathway(start, end);
        SpawnHelmets(start,end);
        //increase chunks
        currentChunk++;
        numberOfChunks++;
    }
    
    public void GenerateRiverObjects()
    {
        //update start and end values to chunks size
        UpdateStartEndValues(chunkLenghtRiver);

        //generate rocks
        for (int i = start+20; i < end-20; i += 55)
        {
            Instantiate(gameObstacles["rock"], new Vector3(Random.Range(-2.5f, 2.5f), 1.2f, i), Quaternion.identity).transform.parent = container.transform;
        }

        //spawn in collectables and pathways
        SpawnCoins(start, end);
        SpawnPotions(start, end);
        GeneratePathway(start, end);
        SpawnHelmets(start,end);
        //increase chunks
        currentChunk++;
        numberOfChunks++;
    }

    //method to generate forest objects
    public void GenerateForestObjects()
    {
        //update start and end values for chunk
        UpdateStartEndValues(chunkLenghtForest);

        //spawn logs
        for (int i = start; i < end; i += 50)
        {
            Instantiate(gameObstacles["logs"], new Vector3(Random.Range(-2.5f, 2.5f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        // Spawns trees.
        for (int i = start; i < end; i += 100)
        {
            int rand = Random.Range(0, 2);

            // Load either left or right falling trees
            if (rand == 0)
            {
                Instantiate(gameObstacles["TreeRight"], new Vector3(0.5f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
            }
            else
            {
                Instantiate(gameObstacles["TreeLeft"], new Vector3(0f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
            }
        }

        //spawn in collectables and pathways
        SpawnCoins(start, end);
        SpawnPotions(start, end);
        GeneratePathway(start, end);
        SpawnHelmets(start,end);
        //increase chunks
        currentChunk++;
        numberOfChunks++;
    }
    #endregion

    //method to generate pathways
    public void GeneratePathway(int start,int end)
    {
        for (int i = start; i < end; i += 1)
        {
            pathway.transform.localScale = new Vector3(3f, .8f, 1f);
            Instantiate(pathway, new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 0.5f), i), Quaternion.identity).transform.parent = container.transform;
        }

    }

    #region == Single Object Spawners == 
    //method to spawn helmets
    private void SpawnHelmets(int start, int end)
    {
        int randomIndex = Random.Range(0, 3);
        Instantiate(gameObstacles["helmet"], new Vector3(spawnpoints[randomIndex], 1.5f, start+125), Quaternion.identity).transform.parent = container.transform;
    }

    //method to spawn potions
    private void SpawnPotions(int start, int end)
    {
        //select random index between 0,1 and 2
        int randomIndex = Random.Range(0, 3);
        //spawn potion in random lane of 3 chosen above
        Instantiate(gameObstacles["RevivePotion"], new Vector3(spawnpoints[randomIndex], 1.5f, start + 170), Quaternion.identity).transform.parent = container.transform;
    }

    //method to spawn coins
    private void SpawnCoins(int start, int end)
    {
        for (int i = start ; i < end; i += 50)
        {
            //select random index between 0,1 and 2
            int randomIndex = Random.Range(0, 3);
            //spawn coin in random lane of 3 chosen above
            Instantiate(gameObstacles["coin"], new Vector3(spawnpoints[randomIndex], 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }
    }
    #endregion
    
    //method to update the start and end values of a chunk
    private void UpdateStartEndValues(int chunkLenght)
    {
        // Get start and end values to load objects from, so it aligns with each chunk.
        if(firstChunk == true)
        {
            start = 0;
            end = chunkLenght;
            firstChunk = false;
            
        }
        else
        {
            start += previousChunkLength;
            end += chunkLenght;
        }
        previousChunkLength = chunkLenght;
    }

    //method to load all game objects for unity folder
    private void LoadAllGameObjects()
    {
        // Get all file names from Resources folder, and get file information about each.
        // Code adapted from: https://answers.unity.com/questions/16433/get-list-of-all-files-in-a-directory.html
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Obstacles");
        FileInfo[] fileInfo = dir.GetFiles();

        // Loop through each file in the directory/array.
        foreach (FileInfo file in fileInfo)
        {
            // Exlude meta files
            if (!file.Name.Contains(".meta"))
            {
                // Remove file extension from file name, get file from resouces and add to dictionary.
                string[] fileName = file.Name.Split('.');

                GameObject temp = Resources.Load("Obstacles/" + fileName[0]) as GameObject;
                gameObstacles.Add(fileName[0], temp);
            }
        }
    }
}
