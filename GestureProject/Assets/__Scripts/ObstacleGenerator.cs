using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private static int numberOfChunks = 1;
    private const int chunkLenghtVillage = 420;
    private const int chunkLenghtRiver = 520;
    private const int chunkLenghtForest = 500;
    private int start = 0;
    private int end = 0;

    // Single object for now, could use a list later.
    private GameObject fence;
    private GameObject npc;
    private GameObject fallingTree;
    private GameObject logs;
    private GameObject rock;
    private GameObject coin;
    private GameObject revivePotion;

    private GameObject container;

    void Start()
    {
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

        for (int i = start; i < end; i += 60)
        {
            npc = Resources.Load("NPC_Man") as GameObject;
            Instantiate(npc, new Vector3(Random.Range(-3f, 3f), 1f, i), Quaternion.Euler(0, -180, 0)).transform.parent = container.transform;
        }
        
        for (int i = start + 30; i < end; i += 27)
        {
            fence = Resources.Load("fence") as GameObject;
            Instantiate(fence, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        // Testing to show trees.
        //for (int i = start; i < end; i += 150)
        //{
        //    fallingTree = Resources.Load("TriggerObstacle") as GameObject;
        //    Instantiate(fallingTree, new Vector3(0.5f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
        //}

        SpawnCoins(start, end);
        SpawnPotions(start, end);

        numberOfChunks++;
    }
    
    public void GenerateRiverObjects()
    {
        UpdateStartEndValues(chunkLenghtRiver);

        for (int i = start + 30; i < end; i += 55)
        {
            rock = Resources.Load("rock") as GameObject;
            Instantiate(rock, new Vector3(Random.Range(-4f, 4f), 1.2f, i), Quaternion.identity).transform.parent = container.transform;
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
            logs = Resources.Load("logs") as GameObject;
            Instantiate(logs, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        for (int i = start + 30; i < end; i += 150)
        {
            fallingTree = Resources.Load("TriggerObstacle") as GameObject;
            Instantiate(fallingTree, new Vector3(0.5f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
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
        for (int i = start; i < end; i += 150)
        {
            revivePotion = Resources.Load("RevivePotion") as GameObject;
            Instantiate(revivePotion, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }
    }

    private void SpawnCoins(int start, int end)
    {
        for (int i = start ; i < end; i += 50)
        {
            coin = Resources.Load("coin") as GameObject;
            Instantiate(coin, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }
    }
    #endregion
    
    private void UpdateStartEndValues(int chunkLenght)
    {
        // Get start and end values to load objects from, so it aligns with each chunk.
        start = (numberOfChunks - 1) * chunkLenght;
        end = numberOfChunks * chunkLenght;
    }
}
