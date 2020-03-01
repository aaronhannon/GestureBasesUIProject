using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private static int numberOfChunks;
    private const int chunkLenghtVillage = 420;
    private const int chunkLenghtRiver = 640;
    private const int chunkLenghtForest = 320;
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

    void Start()
    {
        pathway = GameObject.Find("pathway");
        // Reset on each game load.
        numberOfChunks = 1;

        LoadAllGameObjects();

        container = GameObject.Find("GeneratedObjects");

        this.chunks = gameObject.GetComponent<GenerateChunks>().GetChunkOrder();

        // Testing, will be called randomly from chunk generator.
        //GenerateVillageObjects();
        //GenerateRiverObjects();
        //GenerateForestObjects();



        foreach (int item in chunks)
        {

            switch (item)
            {
                case 0:
                    GenerateVillageObjects();
                    break;
                case 1:
                    GenerateForestObjects();
                    break;
                case 2:
                    GenerateRiverObjects();
                    break;
            }

            currentChunk++;
        }

        Debug.Log("Obstacles Generated");
    }

    #region == Chunk Object Generators == 
    public void GenerateVillageObjects()
    {
        // Get start and end values to load objects from, so it aligns with each chunk.
        UpdateStartEndValues(chunkLenghtVillage);

        for (int i = start; i < end; i += 100)
        {
            Instantiate(gameObstacles["NPC_Man"], new Vector3(Random.Range(-2.5f, 2.5f), 1f, i), Quaternion.Euler(0, -180, 0)).transform.parent = container.transform;
        }
        
        for (int i = start + 30; i < end; i += 27)
        {
            Instantiate(gameObstacles["fence"], new Vector3(Random.Range(-2.5f, 2.5f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        // Testing to show trees.
        //for (int i = start; i < end; i += 150)
        //{
        //    Instantiate(gameObstacles["TriggerObstacle"], new Vector3(0.5f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
        //}

        SpawnCoins(start, end);
        SpawnPotions(start, end);
        GeneratePathway(start, end);
        numberOfChunks++;
    }
    
    public void GenerateRiverObjects()
    {
        UpdateStartEndValues(chunkLenghtRiver);

        for (int i = start+50; i < end; i += 55)
        {
            Instantiate(gameObstacles["rock"], new Vector3(Random.Range(-2.5f, 2.5f), 1.2f, i), Quaternion.identity).transform.parent = container.transform;
        }

        SpawnCoins(start, end);
        SpawnPotions(start, end);
        //GeneratePathway(start, end);
        numberOfChunks++;
    }

    public void GenerateForestObjects()
    {
        UpdateStartEndValues(chunkLenghtForest);

        for (int i = start + 30; i < end; i += 50)
        {
            Instantiate(gameObstacles["logs"], new Vector3(Random.Range(-2.5f, 2.5f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        for (int i = start + 30; i < end; i += 150)
        {
            Instantiate(gameObstacles["TriggerObstacle"], new Vector3(0.5f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        SpawnCoins(start, end);
        SpawnPotions(start, end);
        GeneratePathway(start, end);

        numberOfChunks++;
    }
    #endregion

    public void GeneratePathway(int start,int end)
    {
        int[] chunksArray = (int[])chunks.ToArray(typeof(int));
        int nextChunk;
        int currentChunkValue = chunksArray[currentChunk];
        Debug.Log("Chunk Value: " + currentChunkValue);
        try
        {
            nextChunk = chunksArray[currentChunk + 1];
        }
        catch (System.IndexOutOfRangeException)
        {

            nextChunk = 0;
        }


        if (nextChunk == 2)
        {
            Debug.Log("River next");
            if (chunksArray[currentChunk] == 1)
            {
                Debug.Log("FORST then river");
                for (int i = start + 10; i < end - 50; i += 1)
                {
                    pathway.transform.localScale = new Vector3(Random.Range(1.5f, 3.0f), 1, Random.Range(1.0f, 2.0f));
                    Instantiate(pathway, new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 0.5f), i), Quaternion.identity).transform.parent = container.transform;
                }
            }
            else if (chunksArray[currentChunk] == 0)
            {
                Debug.Log("village then river");
                for (int i = start + 10; i < end - 80; i += 1)
                {
                    pathway.transform.localScale = new Vector3(Random.Range(1.5f, 3.0f), 1, Random.Range(1.0f, 2.0f));
                    Instantiate(pathway, new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 0.5f), i), Quaternion.identity).transform.parent = container.transform;
                }
            }
            else
            {
                for (int i = start; i < end - 10; i += 1)
                {
                    pathway.transform.localScale = new Vector3(Random.Range(1.5f, 3.0f), 1, Random.Range(1.0f, 2.0f));
                    Instantiate(pathway, new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 0.5f), i), Quaternion.identity).transform.parent = container.transform;
                }
            }

        }
        else if (nextChunk == 1)
        {
            Debug.Log("forest Next");
            if (chunksArray[currentChunk] == 2)
            {
                Debug.Log("river then forest");
                for (int i = start - 50; i < end; i += 1)
                {
                    pathway.transform.localScale = new Vector3(Random.Range(1.5f, 3.0f), 1, Random.Range(1.0f, 2.0f));
                    Instantiate(pathway, new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 0.5f), i), Quaternion.identity).transform.parent = container.transform;
                }
            }
            else
            {

                for (int i = start + 10; i < end + 50; i += 1)
                {
                    pathway.transform.localScale = new Vector3(Random.Range(1.5f, 3.0f), 1, Random.Range(1.0f, 2.0f));
                    Instantiate(pathway, new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 0.5f), i), Quaternion.identity).transform.parent = container.transform;
                }
            }
        }
        else
        {
            Debug.Log("River Next");
            for (int i = start + 10; i < end + 50; i += 1)
            {
                pathway.transform.localScale = new Vector3(Random.Range(1.5f, 3.0f), 1, Random.Range(1.0f, 2.0f));
                Instantiate(pathway, new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 0.5f), i), Quaternion.identity).transform.parent = container.transform;
            }
        }

    }


    #region == Single Object Spawners == 
   
    private void SpawnHelmets()
    {

    }

    private void SpawnPotions(int start, int end)
    {
        // Basic implementation for now, will need to be improved and randomized.
        for (int i = start; i < end; i += 170)
        {
            Instantiate(gameObstacles["RevivePotion"], new Vector3(Random.Range(-2.5f, 2.5f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }
    }

    private void SpawnCoins(int start, int end)
    {
        for (int i = start ; i < end; i += 50)
        {
            Instantiate(gameObstacles["coin"], new Vector3(Random.Range(-2.5f, 2.5f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }
    }
    #endregion
    
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
        //start = (numberOfChunks - 1) * chunkLenght;
        //end = numberOfChunks * chunkLenght;
    }

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
