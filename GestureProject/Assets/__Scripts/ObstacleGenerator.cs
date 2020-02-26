using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private static int numberOfChunks;
    private const int chunkLenghtVillage = 420;
    private const int chunkLenghtRiver = 520;
    private const int chunkLenghtForest = 500;
    private int start = 0;
    private int end = 0;
    
    // Dictionary which holds all game objects in memory rather than loading them all the time.
    private Dictionary<string, GameObject> gameObstacles = new Dictionary<string, GameObject>();

    private GameObject container;

    void Start()
    {
        // Reset on each game load.
        numberOfChunks = 1;

        LoadAllGameObjects();

        container = GameObject.Find("GeneratedObjects");

        // Testing, will be called randomly from chunk generator.
        GenerateVillageObjects();
        GenerateRiverObjects();
        GenerateForestObjects();
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

        numberOfChunks++;
    }
    
    public void GenerateRiverObjects()
    {
        UpdateStartEndValues(chunkLenghtRiver);

        for (int i = start; i < end; i += 55)
        {
            Instantiate(gameObstacles["rock"], new Vector3(Random.Range(-2.5f, 2.5f), 1.2f, i), Quaternion.identity).transform.parent = container.transform;
        }

        SpawnCoins(start, end);
        SpawnPotions(start, end);

        numberOfChunks++;
    }

    public void GenerateForestObjects()
    {
        UpdateStartEndValues(chunkLenghtForest);

        for (int i = start + 30; i < end; i += 27)
        {
            Instantiate(gameObstacles["logs"], new Vector3(Random.Range(-2.5f, 2.5f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        for (int i = start + 30; i < end; i += 150)
        {
            Instantiate(gameObstacles["TriggerObstacle"], new Vector3(0.5f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        SpawnCoins(start, end);
        SpawnPotions(start, end);

        numberOfChunks++;
    }
    #endregion

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
        start = (numberOfChunks - 1) * chunkLenght;
        end = numberOfChunks * chunkLenght;
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
